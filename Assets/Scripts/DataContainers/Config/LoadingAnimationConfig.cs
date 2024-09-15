using UnityEngine;

[CreateAssetMenu(fileName = "LoadingAnimationConfig", menuName = "DataContainers/LoadingAnimationConfig")]
public class LoadingAnimationConfig : ScriptableObject
{
    [SerializeField] private float _RotatingSpeed = 0.5f;
    public float RotatingSpeed { get => _RotatingSpeed; }

    [SerializeField] private float _MinimalFillingSpeed = 0.33333333f;
    public float MinimalFillingSpeed { get => _MinimalFillingSpeed; }

    [SerializeField] private float _MaximalFillingSpeed = 0.5f;
    public float MaximalFillingSpeed { get => _MaximalFillingSpeed; }
}