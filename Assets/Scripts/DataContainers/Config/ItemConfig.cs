using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "DataContainers/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private int _ID;
    public int ID { get => _ID; }

    [SerializeField] private Vector3 _Offset;
    public Vector3 Offset { get => _Offset; }

    [SerializeField] private float _Height = 15;
    public float Height { get => _Height; }
}