using System;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    public RectTransform playerMarker;
    public Transform player;
    public RectTransform mapRect;
    public Vector2 mapWorldSize;
    public float offsetX = 5f;
    public float offsetY = -3f;


    void Update()
    {
        if (!mapRect.gameObject.activeSelf) return;
        UpdatePlayerMarker();
        
    }

    private void UpdatePlayerMarker()
    {
        Vector3 playerPos = player.position;

        float x = (1 - (player.position.z / mapWorldSize.y)) * mapRect.rect.width;
        float y = (player.position.x / mapWorldSize.x) * mapRect.rect.height;

        Vector2 offset = new Vector2(offsetX, offsetY); // tweak for map offset

        playerMarker.anchoredPosition = new Vector2(x, y) + offset;
    }
}