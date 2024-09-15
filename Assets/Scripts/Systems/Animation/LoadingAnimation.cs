using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingAnimation : IInitializable
{
    private LoadingAnimationConfig LoadingAnimationConfig;
    private NextSceneLoader PerformerOfActionAfterFillingImage;
    private GameObject RotatedObject;
    private Image FilledImage;

    public LoadingAnimation(LoadingAnimationConfig loadingAnimationConfig, NextSceneLoader performerOfActionAfterFillingImage, GameObject rotatedObject, Image filledImage)
    {
        LoadingAnimationConfig = loadingAnimationConfig;
        PerformerOfActionAfterFillingImage = performerOfActionAfterFillingImage;
        RotatedObject = rotatedObject;
        FilledImage = filledImage;
    }

    public void Initialize()
    {
        RotatedObject.transform.DORotate(360 * Vector3.forward, 1 / LoadingAnimationConfig.RotatingSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        float AnimationDuration = 1 / Random.Range(LoadingAnimationConfig.MinimalFillingSpeed, LoadingAnimationConfig.MaximalFillingSpeed);
        FilledImage.DOFillAmount(1f, AnimationDuration).SetEase(Ease.Linear).OnComplete(PerformerOfActionAfterFillingImage.PerformAction);
    }
}