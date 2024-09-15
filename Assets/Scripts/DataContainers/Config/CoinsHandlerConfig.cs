using UnityEngine;

[CreateAssetMenu(fileName = "CoinsHandlerConfig", menuName = "DataContainers/CoinsHandlerConfig")]
public class CoinsHandlerConfig : ScriptableObject
{
    [SerializeField] private string _KeyForPlayerPrefs = "Coins";
    public string KeyForPlayerPrefs { get => _KeyForPlayerPrefs; }
}