using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float? GetHeight(Vector3 position)
    {
        foreach (var terrain in Terrain.activeTerrains)
        {
            var hasPosition = ContainsPosition(terrain, position);
            if (hasPosition)
            {
                return terrain.SampleHeight(position);
            }
        }

        return null;
    }

    private bool ContainsPosition(Terrain terrain, Vector3 position)
    {
        var hasPosition = false;

        var xMin = terrain.GetPosition().x;
        var xMax = xMin + terrain.terrainData.size.x;

        var zMin = terrain.GetPosition().z;
        var zMax = zMin + terrain.terrainData.size.z;

        if (position.x >= xMin && position.x < xMax && position.z >= zMin && position.z < zMax)
        {
            hasPosition = true;
        }

        return hasPosition;
    }
}
