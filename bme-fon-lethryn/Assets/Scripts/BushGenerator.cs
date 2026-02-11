using UnityEngine;

public class BushGenerator : MonoBehaviour
{
    public GameObject BushHolder;
    public Bush DefaultBush;
    public int InitialBushCount;

    public TerrainManager TerrainManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (var i = 0; i < InitialBushCount; i++)
        {
            var x = Random.Range(0.0f, 5000.0f);
            var z = Random.Range(0.0f, 5000.0f);

            var pos = new Vector3(x, 0.0f, z);
            var y = TerrainManager.GetHeight(pos);
            if (y == null)
            {
                continue;
            }

            var newBush = Instantiate(DefaultBush.BushPrefab, BushHolder.transform);

            newBush.name = $"Bush{i}";


            pos.y = y.Value;

            newBush.transform.position = pos;
            newBush.transform.localScale *= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
