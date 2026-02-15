using UnityEngine;

public class BushGenerator : MonoBehaviour
{
    public GameObject BushHolder;
    public Plant DefaultBush;
    public int InitialBushCount;
    public int BushCount = 0;

    public TerrainManager TerrainManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (var i = 0; i < InitialBushCount; i++)
        {
            var bush = new Bush
            {
                Name = $"Bush{i}",
                Age = 1,
                Status = "Alive",
                Exists = false,
                MaxAge = DefaultBush.MaxAge,
                SpreadAge = Random.Range(DefaultBush.MinSpreadAge, DefaultBush.MaxSpreadAge),
                SpreadCount = DefaultBush.SpreadCount,
                SpreadDistance = DefaultBush.SpreadDistance
            };

            GenerateBush(bush);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AgePlants()
    {
        foreach (Transform plant in BushHolder.transform)
        {
            var bushComp = plant.gameObject.GetComponent<BushPlant>();
            if (bushComp.CanGrow())
            {
                bushComp.Grow();
            }
            if (bushComp.CanSpread())
            {
                bushComp.Spread();
            }
            if (bushComp.ShouldDie())
            {
                BushCount--;
                bushComp.Die();
            }
        }
    }

    public void GenerateBush(Bush bush)
    {
        var newBush = Instantiate(DefaultBush.PlantPrefab, BushHolder.transform);
        newBush.name = bush.Name;

        newBush.AddComponent<BushPlant>();

        var bushComp = newBush.GetComponent<BushPlant>();

        bushComp.Bush = bush;
        bushComp.Generator = this;

        if (bush.Exists)
        {
            newBush.transform.position = bushComp.Bush.Position;
            newBush.transform.localScale = bushComp.Bush.Scale;
        }
        else
        {
            var position = GeneratePosition();
            bushComp.Bush.Position = position;
            newBush.transform.position = position;
            bushComp.Bush.Scale = newBush.transform.localScale;
            bushComp.Bush.Exists = true;
        }

        BushCount++;
    }

    private Vector3 GeneratePosition()
    {
        var pos = new Vector3(0.0f, 0.0f, 0.0f);
        float? y = null;
        while (y == null)
        {
            pos = new Vector3(0.0f, 0.0f, 0.0f);
            var x = Random.Range(-1000.0f, 7000.0f);
            var z = Random.Range(-4000.0f, 5000.0f);
            pos.x = x;
            pos.z = z;

            y = TerrainManager.GetHeight(pos);

            if (y == null)
            {
                continue;
            }

            pos.y = y.Value;
        }

        return pos;
    }

    private Vector3? GeneratePosition(Vector3 position)
    {
        var y = TerrainManager.GetHeight(position);

        if (y == null)
        {
            return null;
        }

        position.y = y.Value;

        return position;
    }

    public void CreateNewBush(string bushName, Vector3 position)
    {
        var newPosition = GeneratePosition(position);
        if (newPosition == null)
        {
            return;
        }

        var bush = new Bush
        {
            Name = bushName,
            Age = 1,
            Status = "Alive",
            Exists = true,
            MaxAge = DefaultBush.MaxAge,
            SpreadAge = Random.Range(DefaultBush.MinSpreadAge, DefaultBush.MaxSpreadAge),
            SpreadCount = DefaultBush.SpreadCount,
            SpreadDistance = DefaultBush.SpreadDistance,
            Position = newPosition.Value,
            Scale = Vector3.one
        };
        GenerateBush(bush);
    }
}
