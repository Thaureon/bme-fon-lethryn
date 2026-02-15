using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Lethryn/Plant")]
public class Plant : ScriptableObject
{
    [SerializeField] public GameObject PlantPrefab;
    [SerializeField] public int MinSpreadAge;
    [SerializeField] public int MaxSpreadAge;
    [SerializeField] public int SpreadCount;
    [SerializeField] public int SpreadDistance;
    [SerializeField] public int MaxAge;
}
