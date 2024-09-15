using UnityEngine;

[System.Serializable]
public class ItemTemplate
{
    [SerializeField] private GameObject _ItemPrefab;
    public GameObject ItemPrefab { get => _ItemPrefab; }

    [SerializeField] private ItemConfig _Config;
    public ItemConfig Config { get => _Config; }
}