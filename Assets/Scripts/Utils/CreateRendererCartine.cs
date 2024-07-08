using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateRendererCartine : MonoBehaviour
{
  public RenderTexture Renderer;
  private void Start()
  {
    if (!Renderer)
    {
      Renderer = GetComponent<Camera>().targetTexture;
    }
  }
#if UNITY_EDITOR
   
  [EasyButtons.Button]
  public void TestSave()
  {
    SaveRenderTextureToPNG("Newtest", Renderer);
  }
  private void SaveRenderTextureToPNG(string textureName, RenderTexture renderTexture, Action<TextureImporter> importAction = null)
  {
    string path = EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
    if(path.Length != 0)
    {
      var newTex = new Texture2D(renderTexture.width, renderTexture.height);
      RenderTexture.active = renderTexture;
      newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
      newTex.Apply();

      byte[] pngData = newTex.EncodeToPNG();
      if(pngData != null)
      {
        File.WriteAllBytes(path, pngData);
        AssetDatabase.Refresh();
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if(importAction != null)
          importAction(importer);
      }

      Debug.Log(path);
    }
  }
#endif
}
