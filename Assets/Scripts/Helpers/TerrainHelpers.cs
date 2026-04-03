using UnityEngine;

namespace Helpers
{
    public static class TerrainHelpers
    {
        public static float GetTerrainHeightAtPosition(this Terrain terrain, float x, float z)
            => terrain.SampleHeight(new Vector3(x, 0f, z));
    }
}