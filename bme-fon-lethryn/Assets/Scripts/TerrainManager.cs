using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    private List<Terrain> terrains = new List<Terrain>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrains = Terrain.activeTerrains.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float? GetHeight(Vector3 position)
    {
        foreach (var terrain in terrains)
        {
            var hasPosition = ContainsPosition(terrain, position);
            if (hasPosition)
            {
                var newX = position.x % terrain.terrainData.size.x;
                var newZ = position.z % terrain.terrainData.size.z;
                return terrain.SampleHeight(new Vector3(newX, 0.0f, newZ));
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
