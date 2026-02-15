using UnityEngine;

public class BushPlant : MonoBehaviour
{
    public Bush Bush;

    public BushGenerator Generator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanGrow()
    {
        return Bush.Age < Bush.MaxAge;
    }

    public void Grow()
    {
        Bush.Age++;
        Bush.Scale *= 1.1f;
        gameObject.transform.localScale = Bush.Scale;
    }

    public bool ShouldDie()
    {
        var deathProbability = Mathf.Pow(Bush.Age / (float)Bush.MaxAge, 4.0f);
        var chanceToDie = Random.Range(0.0f, 1.0f);
        return deathProbability >= chanceToDie;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool CanSpread()
    {
        return Bush.Age == Bush.SpreadAge;
    }

    public void Spread()
    {
        var spreadAmount = Random.Range(0, Bush.SpreadCount + 1);

        for (var i = 0; i < spreadAmount; i++)
        {
            var newX = Random.Range(-Bush.SpreadDistance, Bush.SpreadDistance);
            var newZ = Random.Range(-Bush.SpreadDistance, Bush.SpreadDistance);

            var position = gameObject.transform.position;
            position.x += newX;
            position.z += newZ;

            Generator.CreateNewBush($"{gameObject.name}-{i}", position);
        }
    }
}
