using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace SixthAwakening.Editor {
 public static class CreateHealthPrefabs {
  private const string Root="Assets/TheSixthAwakening/GroundAndHealth";
  [MenuItem("The Sixth Awakening/Create Health UI and Actor Templates")]
  public static void Build(){
   Directory.CreateDirectory(Root+"/Art/UI");Directory.CreateDirectory(Root+"/Prefabs");
   string whitePath=Root+"/Art/UI/white.png";
   if(!File.Exists(whitePath)){var t=new Texture2D(4,4,TextureFormat.RGBA32,false);var colors=new Color[16];for(int i=0;i<16;i++)colors[i]=Color.white;t.SetPixels(colors);t.Apply();File.WriteAllBytes(whitePath,t.EncodeToPNG());Object.DestroyImmediate(t);}
   AssetDatabase.Refresh();
   var importer=(TextureImporter)AssetImporter.GetAtPath(whitePath);importer.textureType=TextureImporterType.Sprite;importer.spriteImportMode=SpriteImportMode.Single;importer.SaveAndReimport();
   Sprite white=AssetDatabase.LoadAssetAtPath<Sprite>(whitePath);
   Make("Kael",100,.35f,440,72,new Color(.39f,.55f,.64f),white);
   Make("Mira",100,.35f,440,72,new Color(.64f,.28f,.35f),white);
   Make("Enemy",40,0,240,54,new Color(.62f,.54f,.42f),white);
   Make("Boss",800,.08f,820,78,new Color(.72f,.55f,.28f),white);
   AssetDatabase.SaveAssets();
   Debug.Log("Health templates created. Put UI prefabs under a Canvas, attach/configure HealthComponent on actors, and bind each HealthBarView target. HP values are balancing placeholders.");
  }
  private static Image Rect(string name,Transform parent,Vector2 min,Vector2 max,Vector2 offsetMin,Vector2 offsetMax,Color color,Sprite white){
   var go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));go.transform.SetParent(parent,false);
   var rt=(RectTransform)go.transform;rt.anchorMin=min;rt.anchorMax=max;rt.offsetMin=offsetMin;rt.offsetMax=offsetMax;
   var image=go.GetComponent<Image>();image.sprite=white;image.color=color;image.raycastTarget=false;return image;
  }
  private static void Make(string label,int hp,float immunity,float width,float height,Color accent,Sprite white){
   string uiPath=Root+"/Prefabs/"+label+"HealthUI.prefab",actorPath=Root+"/Prefabs/"+label+"HealthActor.prefab";
   if(File.Exists(uiPath)||File.Exists(actorPath)){Debug.LogWarning("Skipped existing "+label+" templates to preserve edits.");return;}
   var root=new GameObject(label+" Health UI",typeof(RectTransform));
   ((RectTransform)root.transform).sizeDelta=new Vector2(width,height);
   Rect("Border",root.transform,Vector2.zero,Vector2.one,Vector2.zero,Vector2.zero,accent,white);
   Rect("Panel",root.transform,Vector2.zero,Vector2.one,new Vector2(2,2),new Vector2(-2,-2),new Color(.07f,.08f,.1f,.97f),white);
   var track=Rect("Health Track",root.transform,new Vector2(0,0),new Vector2(1,0),new Vector2(14,12),new Vector2(-14,28),new Color(.19f,.12f,.14f),white);
   var fill=Rect("Health Fill",track.transform,Vector2.zero,Vector2.one,Vector2.zero,Vector2.zero,new Color(.67f,.12f,.17f),white);
   fill.type=Image.Type.Filled;fill.fillMethod=Image.FillMethod.Horizontal;fill.fillOrigin=0;fill.fillAmount=1;
   for(int i=1;i<4;i++)Rect("Quarter Marker "+i,track.transform,new Vector2(i*.25f,0),new Vector2(i*.25f,1),new Vector2(-1,0),new Vector2(1,0),new Color(.06f,.07f,.09f,.65f),white);
   var textObject=new GameObject("HP and State",typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));textObject.transform.SetParent(root.transform,false);
   var tr=(RectTransform)textObject.transform;tr.anchorMin=new Vector2(0,0);tr.anchorMax=Vector2.one;tr.offsetMin=new Vector2(14,30);tr.offsetMax=new Vector2(-14,-5);
   var text=textObject.GetComponent<Text>();
#if UNITY_2022_2_OR_NEWER
   text.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
#else
   text.font=Resources.GetBuiltinResource<Font>("Arial.ttf");
#endif
   text.fontSize=label=="Enemy"?12:16;text.color=new Color(.93f,.89f,.81f);text.alignment=TextAnchor.MiddleLeft;text.raycastTarget=false;text.text=label+"  "+hp+" / "+hp+"  · HEALTHY";
   var view=root.AddComponent<HealthBarView>();var so=new SerializedObject(view);so.FindProperty("fill").objectReferenceValue=fill;so.FindProperty("valueLabel").objectReferenceValue=text;so.FindProperty("displayName").stringValue=label;so.ApplyModifiedPropertiesWithoutUndo();
   PrefabUtility.SaveAsPrefabAsset(root,uiPath);Object.DestroyImmediate(root);
   var actor=new GameObject(label+" Health Actor");var health=actor.AddComponent<HealthComponent>();var hs=new SerializedObject(health);hs.FindProperty("maximumHealth").intValue=hp;hs.FindProperty("invulnerabilitySeconds").floatValue=immunity;hs.ApplyModifiedPropertiesWithoutUndo();
   PrefabUtility.SaveAsPrefabAsset(actor,actorPath);Object.DestroyImmediate(actor);
  }
 }
}
