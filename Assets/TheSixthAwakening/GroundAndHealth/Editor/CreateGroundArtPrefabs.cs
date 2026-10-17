using System.IO;
using UnityEditor;
using UnityEngine;
namespace SixthAwakening.Editor {
 public static class CreateGroundArtPrefabs {
  private const string Root="Assets/TheSixthAwakening/GroundAndHealth";
  [MenuItem("The Sixth Awakening/Create Ground Art Prefabs")]
  public static void Build(){
   int created=0;
   foreach(string guid in AssetDatabase.FindAssets("t:Sprite",new[]{Root+"/Art/Ground"})){
    string path=AssetDatabase.GUIDToAssetPath(guid);Sprite sprite=AssetDatabase.LoadAssetAtPath<Sprite>(path);if(sprite==null)continue;
    string setting=Path.GetFileName(Path.GetDirectoryName(path)),name=Path.GetFileNameWithoutExtension(path);
    string dir=Root+"/Prefabs/Ground/"+setting;if(!Directory.Exists(dir)){Directory.CreateDirectory(dir);AssetDatabase.Refresh();}
    string prefabPath=dir+"/"+name+".prefab";if(File.Exists(prefabPath))continue;
    var go=new GameObject(name+" (art)");
    var renderer=go.AddComponent<SpriteRenderer>();renderer.sprite=sprite;
    renderer.flipX=setting=="03-hall-of-stolen-voices"&&name=="step-up";
    PrefabUtility.SaveAsPrefabAsset(go,prefabPath);Object.DestroyImmediate(go);created++;
   }
   AssetDatabase.Refresh();Debug.Log("Created "+created+" ground art prefabs. Add and tune collision surfaces separately; decorative art outlines are not collision.");
  }
 }
}
