using UnityEngine;

[CreateAssetMenu(fileName = "MovableRectTransformConfig", menuName = "DataContainers/MovableRectTransformConfig")]
public class MovableRectTransformConfig : ScriptableObject
{
    [SerializeField] private Vector2 _TargetAnchoredPosition;
    public Vector2 TargetAnchoredPosition { get => _TargetAnchoredPosition; }

    [SerializeField] private float _MovingDuration;
    public float MovingDuration { get => _MovingDuration; }
}