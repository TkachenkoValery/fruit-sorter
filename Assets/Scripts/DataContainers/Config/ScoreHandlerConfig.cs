using UnityEngine;

[CreateAssetMenu(fileName = "ScoreHandlerConfig", menuName = "DataContainers/ScoreHandlerConfig")]
public class ScoreHandlerConfig : ScriptableObject
{
    [SerializeField] private int _StartScore = 999;
    public int StartScore { get => _StartScore; }

    [SerializeField] private float _DecreasingSpeedInSeconds = 0.1f;
    public float DecreasingSpeedInSeconds { get => _DecreasingSpeedInSeconds; }
}