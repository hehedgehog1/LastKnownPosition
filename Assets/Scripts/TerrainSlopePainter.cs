using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainSlopePainter3Layers : MonoBehaviour
{
    [Header("Terrain Layers")]
    public TerrainLayer grassLayer;
    public TerrainLayer soilLayer;
    public TerrainLayer rockLayer;

    [Header("Slope Settings")]
    public float grassMaxSlope = 20f;
    public float soilMaxSlope = 35f;

    private Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        terrain.terrainData.terrainLayers = new TerrainLayer[] { grassLayer, soilLayer, rockLayer };

        PaintBySlope();
    }

    void PaintBySlope()
    {
        int w = terrainData.alphamapWidth;
        int h = terrainData.alphamapHeight;
        float[,,] alphaMaps = new float[h, w, 3]; // 3 layers

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                float normX = (float)x / (w - 1);
                float normY = (float)y / (h - 1);

                float slope = terrainData.GetSteepness(normX, normY);

                // blend layers based on slope
                float grassWeight = Mathf.Clamp01((grassMaxSlope - slope) / 5f); 
                float soilWeight = Mathf.Clamp01((slope - grassMaxSlope) / (soilMaxSlope - grassMaxSlope));
                soilWeight = Mathf.Clamp01(soilWeight * (1 - Mathf.Clamp01((slope - soilMaxSlope) / 5f))); // fades out as it approaches rock
                float rockWeight = slope > soilMaxSlope ? 1f : 0f;

                float total = grassWeight + soilWeight + rockWeight;
                alphaMaps[y, x, 0] = grassWeight / total;
                alphaMaps[y, x, 1] = soilWeight / total;
                alphaMaps[y, x, 2] = rockWeight / total;
            }
        }

        terrainData.SetAlphamaps(0, 0, alphaMaps);
    }
}