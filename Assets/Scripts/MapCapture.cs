using UnityEngine;
using System.IO;

public class MapCapture : MonoBehaviour
{
    public Camera mapCamera;
    public RenderTexture renderTexture;

    void Start()
    {
        Capture();
    }

    public void Capture()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        mapCamera.Render();

        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false, true);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

        // Convert linear colors to sRGB to fix dark PNG
        Color[] pixels = image.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = pixels[i].gamma;
        image.SetPixels(pixels);

        image.Apply();

        string filePath = Application.dataPath + "/Art/Map/MapImage.png";
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        RenderTexture.active = currentRT;

        Debug.Log("Map saved!");
    }
}