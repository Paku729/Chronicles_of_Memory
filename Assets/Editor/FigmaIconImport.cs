using UnityEngine;
using UnityEditor;

// Разовая утилита: разложить импорт иконок из Figma как Sprite (2D and UI).
public static class FigmaIconImport
{
    [MenuItem("Tools/Import Figma Icons")]
    public static void Run()
    {
        AssetDatabase.Refresh();
        string folder = "Assets/Art/icons";
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new string[] { folder });
        int changed = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter imp = (TextureImporter)AssetImporter.GetAtPath(path);
            if (imp.textureType != TextureImporterType.Sprite
                || imp.textureCompression != TextureImporterCompression.Uncompressed)
            {
                imp.textureType = TextureImporterType.Sprite;
                imp.spriteImportMode = SpriteImportMode.Single;
                imp.mipmapEnabled = false;
                imp.alphaIsTransparency = true;
                imp.textureCompression = TextureImporterCompression.Uncompressed;
                imp.SaveAndReimport();
                changed++;
            }
        }
        Debug.Log("[FigmaIconImport] спрайтов настроено: " + changed + " из " + guids.Length);
    }
}