using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class MovableRectTransform : IInitializable
{
    private RectTransform RectTransformToBeMoved;
    private MovableRectTransformConfig Config;

    private bool IsOpenedNow = false;
    private Vector2 OriginalAnchoredPosition;

    public MovableRectTransform(RectTransform rectTransformToBeMoved, MovableRectTransformConfig config)
    {
        RectTransformToBeMoved = rectTransformToBeMoved;
        Config = config;
    }

    public void Initialize()
    {
        OriginalAnchoredPosition = RectTransformToBeMoved.anchoredPosition;
    }

    public void Move()
    {
        RectTransformToBeMoved.DOAnchorPos(IsOpenedNow ? OriginalAnchoredPosition : Config.TargetAnchoredPosition, Config.MovingDuration);
        IsOpenedNow = !IsOpenedNow;
    }

    private void MoveUnlessConditionIsTrue(Func<bool> condition)
    {
        if (condition.Invoke())
            return;
        Move();
    }

    public void Open()
    {
        MoveUnlessConditionIsTrue(() => IsOpenedNow);
    }

    public void Close()
    {
        MoveUnlessConditionIsTrue(() => !IsOpenedNow);
    }
}