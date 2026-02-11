using UnityEngine;

[CreateAssetMenu(fileName = "Bush", menuName = "Lethryn/Bush")]
public class Bush : ScriptableObject
{
    [SerializeField] public GameObject BushPrefab;
    [SerializeField] public bool Exists;
    [SerializeField] public Vector3 Position;
    [SerializeField] public Vector3 Scale;
}
