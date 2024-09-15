using UnityEngine;

[CreateAssetMenu(fileName = "PlaceholderConfig", menuName = "DataContainers/PlaceholderConfig")]
public class PlaceholderConfig : ScriptableObject
{
    [SerializeField] private int _ID;
    public int ID { get => _ID; }
}
