using UnityEngine;

public class MapMarker : MonoBehaviour
{
    public RectTransform playerMarker;
    public Transform player;
    public RectTransform mapRect;

    public Vector2 mapWorldMin;
    public Vector2 mapWorldMax;

    private float mapScale = 0.878f;

    private void Update()
    {
        if (!mapRect.gameObject.activeSelf) return;
        UpdatePlayerMarker();
    }

    private void UpdatePlayerMarker()
    {
        // Ignore elevation and move player to bottom of map
        float u = 1f - Mathf.InverseLerp(mapWorldMin.y, mapWorldMax.y, player.position.z);
        float v = Mathf.InverseLerp(mapWorldMin.x, mapWorldMax.x, player.position.x);

        // Padding to account for borders around map
        float padding = (1f - mapScale) / 2f;
        float mappedU = padding + u * mapScale;
        float mappedV = padding + v * mapScale;

        playerMarker.anchoredPosition = new Vector2(
            mappedU * mapRect.rect.width,
            mappedV * mapRect.rect.height
        );
    }
}