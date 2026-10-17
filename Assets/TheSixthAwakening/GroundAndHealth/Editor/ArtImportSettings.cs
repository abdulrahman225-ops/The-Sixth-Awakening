using UnityEditor;
using UnityEngine;
namespace SixthAwakening.Editor {
 // Applies only inside this pack; does not modify unrelated project art.
 public sealed class ArtImportSettings : AssetPostprocessor {
  private void OnPreprocessTexture(){
   if(!assetPath.StartsWith("Assets/TheSixthAwakening/GroundAndHealth/Art/"))return;
   var importer=(TextureImporter)assetImporter;
   importer.textureType=TextureImporterType.Sprite;
   importer.spriteImportMode=SpriteImportMode.Single;
   importer.spritePixelsPerUnit=128;
   importer.alphaIsTransparency=true;
   importer.mipmapEnabled=false;
   importer.wrapMode=TextureWrapMode.Clamp;
   importer.filterMode=FilterMode.Bilinear;
   importer.textureCompression=TextureImporterCompression.Uncompressed;
   importer.maxTextureSize=4096;
  }
 }
}
