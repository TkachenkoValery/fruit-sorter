using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadSceneSystemsInstaller : MonoInstaller
{
    [SerializeField] private GameObject RotatedObject;
    [SerializeField] private Image FilledImage;
    [SerializeField] private GameObject MovingObjectAtIdleAnimation;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LoadingAnimation>().AsSingle().WithArguments(RotatedObject, FilledImage);
        Container.BindInterfacesAndSelfTo<IdleAnimation>().AsSingle().WithArguments(MovingObjectAtIdleAnimation);
    }
}