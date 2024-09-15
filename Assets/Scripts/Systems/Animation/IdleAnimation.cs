using UnityEngine;
using DG.Tweening;
using Zenject;

public class IdleAnimation : IInitializable
{
    private IdleAnimationConfig Config;
    private GameObject MovingObject;

    private int TargetPositionIndex;

    public IdleAnimation(IdleAnimationConfig config, GameObject movingObject)
    {
        Config = config;
        MovingObject = movingObject;
    }

    public void Initialize()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        TargetPositionIndex = TargetPositionIndex == Config.Positions.Count - 1 ? 0 : TargetPositionIndex + 1;
        MovingObject.transform.DOMove(MovingObject.transform.position + (Config.YTopPosition - MovingObject.transform.position.y) * Vector3.up, Config.OneStepDuration).OnComplete(() =>
            {
                Vector3 StartPoint = MovingObject.transform.position;
                Vector3 EndPoint = Config.Positions[TargetPositionIndex];
                EndPoint.y = Config.YTopPosition;
                Vector3 MiddlePoint = (StartPoint + EndPoint) / 2 + Vector3.up * (StartPoint - EndPoint).magnitude / 2;
                Vector3[] Path = new Vector3[] { StartPoint, MiddlePoint, EndPoint };
                MovingObject.transform.DOPath(Path, Config.OneStepDuration, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
                    MovingObject.transform.DOMove(MovingObject.transform.position + (Config.YTopPosition - Config.Positions[TargetPositionIndex].y) * Vector3.down, Config.OneStepDuration).OnComplete(StartMoving)
                );
            }
        );
    }
}
