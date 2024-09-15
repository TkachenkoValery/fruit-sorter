using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAnimationConfig", menuName = "DataContainers/IdleAnimationConfig")]
public class IdleAnimationConfig : ScriptableObject
{
    [SerializeField] private List<Vector3> _Positions = new();
    public List<Vector3> Positions { get => _Positions; }

    [SerializeField] private float _YTopPosition = 156;
    public float YTopPosition { get => _YTopPosition; }

    [SerializeField] private float _OneStepDuration = 1;
    public float OneStepDuration { get => _OneStepDuration; }
}