using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCoreMechanicsProviderConfig", menuName = "DataContainers/LevelCoreMechanicsProviderConfig")]
public class LevelCoreMechanicsProviderConfig : ScriptableObject
{
    [SerializeField] private int _MinimumPlaceholders = 3;
    public int MinimumPlaceholders { get => _MinimumPlaceholders; }

    [SerializeField] private int _MaximumPlaceholders = 6;
    public int MaximumPlaceholders { get => _MaximumPlaceholders; }

    [SerializeField] private List<ItemTemplate> _PossibleItems = new();
    public List<ItemTemplate> PossibleItems { get => _PossibleItems; }

    [SerializeField] private int _MaximumItemsInPlaceholder = 5;
    public int MaximumItemsInPlaceholder { get => _MaximumItemsInPlaceholder; }

    [SerializeField] private int _MinimumItemsToBeSpawnedPerPlaceholder = 2;
    public int MinimumItemsToBeSpawnedPerPlaceholder { get => _MinimumItemsToBeSpawnedPerPlaceholder; }

    [SerializeField] private int _MaximumItemsToBeSpawnedPerPlaceholder = 4;
    public int MaximumItemsToBeSpawnedPerPlaceholder { get => _MaximumItemsToBeSpawnedPerPlaceholder; }

    [SerializeField] private float _YPositionOfNewItems = 180;
    public float YPositionOfNewItems { get => _YPositionOfNewItems; }

    [SerializeField] private float _StartSpeedOfNewItem = 20;
    public float StartSpeedOfNewItem { get => _StartSpeedOfNewItem; }

    [SerializeField] private float _DurationOfMovingUp = 0.5f;
    public float DurationOfMovingUp { get => _DurationOfMovingUp; }

    [SerializeField] private float _LowestYPositionInGlass = 63.4f;
    public float LowestYPositionInGlass { get => _LowestYPositionInGlass; }
}