using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuSystemsInstaller : MonoInstaller
{
    [SerializeField] private AudioSource ButtonSound;
    [SerializeField] private AudioSource WhooshSound;
    [SerializeField] private GameObject AudioEnabledSign;
    [SerializeField] private GameObject AudioDisabledSign;
    [SerializeField] private Button AudioSwitchingButton;
    [SerializeField] private Button NextSceneLoadingButton;
    [SerializeField] private Button ApplicationQuittingButton;
    [SerializeField] private RectTransform MovableSideUI;
    [SerializeField] private Button SideUIMovingButton;
    [SerializeField] private List<Graphic> ButtonsWithSound = new();
    [SerializeField] private GameObject MovingObjectAtIdleAnimation;
    [SerializeField] private Text TextWithCoinsAmount;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle().WithArguments(ButtonSound, WhooshSound);
        Container.Bind<AudioSwitcher>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioSwitcherView>().AsSingle().WithArguments(AudioEnabledSign, AudioDisabledSign);
        Container.BindInterfacesAndSelfTo<AudioChangingButton>().AsCached().WithArguments(AudioSwitchingButton);
        Container.BindInterfacesAndSelfTo<NextSceneLoadingButton>().AsCached().WithArguments(NextSceneLoadingButton);
        Container.BindInterfacesAndSelfTo<ApplicationQuittingButton>().AsCached().WithArguments(ApplicationQuittingButton);
        foreach (Graphic graphic in ButtonsWithSound)
        {
            Container.BindInterfacesAndSelfTo<ButtonWithSound>().AsCached().WithArguments(graphic);
        }
        Container.BindInterfacesAndSelfTo<IdleAnimation>().AsSingle().WithArguments(MovingObjectAtIdleAnimation);
        Container.BindInterfacesAndSelfTo<CoinsHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<CoinsUIView>().AsSingle().WithArguments(new ObservableCollection<Text>() { TextWithCoinsAmount });
        Container.BindInterfacesAndSelfTo<MovableSideUI>().AsSingle().WithArguments(MovableSideUI);
        Container.BindInterfacesAndSelfTo<SideUIMover>().AsSingle();
        Container.BindInterfacesAndSelfTo<SideUIMovingButton>().AsSingle().WithArguments(SideUIMovingButton);
    }
}