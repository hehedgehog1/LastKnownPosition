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

        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        byte[] bytes = image.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/MapImage.png", bytes);

        RenderTexture.active = currentRT;

        Debug.Log("Map saved!");
    }
}