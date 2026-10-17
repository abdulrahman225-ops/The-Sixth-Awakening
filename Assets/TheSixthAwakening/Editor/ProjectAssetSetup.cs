using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SixthAwakening.Editor {
 public sealed class ProjectArtImporter : AssetPostprocessor {
  void OnPreprocessTexture() {
   if(!assetPath.StartsWith("Assets/TheSixthAwakening/Art/") || !assetPath.EndsWith(".png")) return;
   var t=(TextureImporter)assetImporter;
   t.textureType=TextureImporterType.Sprite;t.spriteImportMode=SpriteImportMode.Single;
   t.spritePixelsPerUnit=128;t.alphaIsTransparency=true;t.mipmapEnabled=false;
   t.wrapMode=TextureWrapMode.Clamp;t.filterMode=FilterMode.Bilinear;
   t.textureCompression=TextureImporterCompression.Uncompressed;t.maxTextureSize=4096;
   if(assetPath.Contains("/bosses/sprites/") && !assetPath.Contains("-atlas") && !assetPath.Contains("-source")) {
    var settings=new TextureImporterSettings();t.ReadTextureSettings(settings);
    settings.spriteAlignment=(int)SpriteAlignment.Custom;settings.spritePivot=new Vector2(.5f,.125f);t.SetTextureSettings(settings);
   }
   if(assetPath.EndsWith("-animations-draft-v1.png")) {
    t.spriteImportMode=SpriteImportMode.Multiple;
    var cells=new SpriteMetaData[36];
    string[] actions={"idle","walk","run","jump","attack","death"};
    for(int row=0;row<6;row++)for(int col=0;col<6;col++)cells[row*6+col]=new SpriteMetaData {
     name=actions[row]+"_"+col.ToString("00"),rect=new Rect(col*209,(5-row)*209,209,209),
     alignment=(int)SpriteAlignment.Custom,pivot=new Vector2(.5f,.125f)
    };
    // This project targets Unity 2021 LTS, whose importer exposes this slicing API.
    t.spritesheet=cells;
   }
  }
 }

 [InitializeOnLoad]
 public static class ProjectAssetSetup {
  const string Root="Assets/TheSixthAwakening";
  const string Output=Root+"/Generated";
  [Serializable] public class Prop {public string id; public bool loop;public string anchor;public int fps;}
  [Serializable] public class Setting {public string name;public Prop[] props;}
  [Serializable] public class Settings {public Setting[] items;}
  static ProjectAssetSetup(){EditorApplication.delayCall+=FirstImport;}
  static void FirstImport(){
   if(EditorApplication.isCompiling||EditorApplication.isUpdating){EditorApplication.delayCall+=FirstImport;return;}
   if(!File.Exists(Output+"/setup-v1.txt"))Build();
  }
  [MenuItem("The Sixth Awakening/Prepare All Assets")]
  public static void Build(){
   CreateGroundArtPrefabs.Build();CreateHealthPrefabs.Build();
   string[] actions={"idle","walk","run","jump","attack","death"};
   foreach(string name in new[]{"kael","mira","veyr"}){
    var sprites=AssetDatabase.LoadAllAssetsAtPath(Root+"/Art/characters/sprites/"+name+"-animations-draft-v1.png").OfType<Sprite>().ToArray();
    MakeActor(name,actions.Select(a=>MakeClip("Characters/"+name,a,sprites.Where(s=>s.name.StartsWith(a+"_")).OrderBy(s=>s.name).ToArray(),a=="attack"?12:8,a=="idle"||a=="walk"||a=="run")).ToArray(),name=="veyr"?800:100);
   }
   foreach(string dir in Directory.GetDirectories(Root+"/Art/characters/bosses/sprites")){
    string name=Path.GetFileName(dir);
    MakeActor(name,actions.Select(a=>MakeClip("Characters/"+name,a,Directory.GetFiles(dir,a+"_*.png").OrderBy(p=>p).Select(p=>AssetDatabase.LoadAssetAtPath<Sprite>(p.Replace('\\','/'))).ToArray(),8,a=="idle"||a=="walk"||a=="run")).ToArray(),500);
   }
   string json=File.ReadAllText(Root+"/Art/environment/props.json");
   foreach(var setting in JsonUtility.FromJson<Settings>("{\"items\":"+json+"}").items)foreach(var prop in setting.props){
    string dir=Root+"/Art/environment/"+setting.name+"/"+prop.id;
    var paths=Directory.GetFiles(dir,prop.id+"_*.png").OrderBy(p=>p).Select(p=>p.Replace('\\','/')).ToArray();
    foreach(string path in paths){
     var importer=(TextureImporter)AssetImporter.GetAtPath(path);var s=new TextureImporterSettings();importer.ReadTextureSettings(s);
     Vector2 pivot=new Vector2(.5f,prop.anchor=="top"?.875f:prop.anchor=="bottom"?.125f:.5f);
     if(s.spriteAlignment!=(int)SpriteAlignment.Custom||s.spritePivot!=pivot){s.spriteAlignment=(int)SpriteAlignment.Custom;s.spritePivot=pivot;importer.SetTextureSettings(s);importer.SaveAndReimport();}
    }
    var clip=MakeClip("Environment/"+setting.name,prop.id,paths.Select(p=>AssetDatabase.LoadAssetAtPath<Sprite>(p)).ToArray(),prop.fps,prop.loop);
    MakeActor("Environment/"+setting.name+"/"+prop.id,new[]{clip},0);
   }
   Folder(Output);File.WriteAllText(Output+"/setup-v1.txt","Prepared animation clips, controllers and visual prefabs. See README for prototype limitations.");
   AssetDatabase.Refresh();AssetDatabase.SaveAssets();Debug.Log("The Sixth Awakening assets prepared successfully.");
  }
  static void Folder(string path){if(!Directory.Exists(path)){Directory.CreateDirectory(path);AssetDatabase.Refresh();}}
  static AnimationClip MakeClip(string folder,string name,Sprite[] frames,int fps,bool loop){
   if(frames.Length==0||frames.Any(s=>s==null))throw new InvalidOperationException("Missing frames: "+folder+"/"+name);
   string dir=Output+"/Animations/"+folder;Folder(dir);string path=dir+"/"+name+".anim";
   var existing=AssetDatabase.LoadAssetAtPath<AnimationClip>(path);if(existing!=null)return existing;
   var clip=new AnimationClip {name=name,frameRate=fps};
   var keys=new ObjectReferenceKeyframe[frames.Length+1];
   for(int i=0;i<frames.Length;i++)keys[i]=new ObjectReferenceKeyframe{time=(float)i/fps,value=frames[i]};
   keys[frames.Length]=new ObjectReferenceKeyframe{time=(float)frames.Length/fps,value=frames[frames.Length-1]};
   AnimationUtility.SetObjectReferenceCurve(clip,EditorCurveBinding.PPtrCurve("",typeof(SpriteRenderer),"m_Sprite"),keys);
   var settings=AnimationUtility.GetAnimationClipSettings(clip);settings.loopTime=loop;AnimationUtility.SetAnimationClipSettings(clip,settings);
   AssetDatabase.CreateAsset(clip,path);return clip;
  }
  static void MakeActor(string name,AnimationClip[] clips,int health){
   string path=Output+"/Prefabs/"+name;Folder(Path.GetDirectoryName(path));
   if(File.Exists(path+".prefab"))return;
   var controller=AssetDatabase.LoadAssetAtPath<AnimatorController>(path+".controller");
   if(controller==null){controller=AnimatorController.CreateAnimatorControllerAtPath(path+".controller");foreach(var clip in clips){var state=controller.layers[0].stateMachine.AddState(clip.name);state.motion=clip;}}
   var go=new GameObject(Path.GetFileName(name));var renderer=go.AddComponent<SpriteRenderer>();
   var binding=EditorCurveBinding.PPtrCurve("",typeof(SpriteRenderer),"m_Sprite");renderer.sprite=(Sprite)AnimationUtility.GetObjectReferenceCurve(clips[0],binding)[0].value;
   go.AddComponent<Animator>().runtimeAnimatorController=controller;
   if(health>0){var h=go.AddComponent<HealthComponent>();var serialized=new SerializedObject(h);serialized.FindProperty("maximumHealth").intValue=health;serialized.ApplyModifiedPropertiesWithoutUndo();}
   PrefabUtility.SaveAsPrefabAsset(go,path+".prefab");UnityEngine.Object.DestroyImmediate(go);
  }
 }
}
