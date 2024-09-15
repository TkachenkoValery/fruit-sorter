using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[System.Serializable]
public struct PlaceholderData
{
    public Collider Collider;
    public Camera Camera;
    public PlaceholderConfig PlaceholderConfig;
}

public class GameSystemsInstaller : MonoInstaller
{
    [Header("Placeholders data")]
    [SerializeField] private List<PlaceholderData> PlaceholdersData = new();

    [Header("Audio")]
    [SerializeField] private AudioSource WhooshSound;
    [SerializeField] private AudioSource ButtonSound;

    [Header("Playing sound on button")]
    [SerializeField] private List<Graphic> ButtonsWithSound = new();

    [Header("In-game UI")]
    [SerializeField] private RectTransform MovableSideUI;
    [SerializeField] private Button SideUIMovingButton;
    [SerializeField] private Button SideUIClosingButton;
    [SerializeField] private Text TextWithInGameScore;
    [SerializeField] private Text TextWithInGameCoins;
    [SerializeField] private Button HomeButtonInGame;
    [SerializeField] private Button RestartButtonInGame;
    [SerializeField] private GameObject AudioEnabledSign;
    [SerializeField] private GameObject AudioDisabledSign;
    [SerializeField] private Button AudioSwitchingButton;

    [Header("Game finish UI")]
    [SerializeField] private RectTransform FinalWindow;
    [SerializeField] private Text TextWithScoreOnPerfectPopup;
    [SerializeField] private Text TextWithCoinsOnPerfectPopup;
    [SerializeField] private Button HomeButtonOnFinalScreen;
    [SerializeField] private Button RestartButtonOnFinalScreen;

    public override void InstallBindings()
    {
        Container.Bind<GameCycleEventHandler>().AsSingle().NonLazy();
        foreach (PlaceholderData placeholderData in PlaceholdersData)
        {
            Container.BindInterfacesAndSelfTo<Placeholder>().AsCached().WithArguments(placeholderData.Collider, placeholderData.Camera, placeholderData.PlaceholderConfig).NonLazy();
        }
        Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle().WithArguments(ButtonSound, WhooshSound).NonLazy();
        foreach(Graphic graphic in ButtonsWithSound)
        {
            Container.BindInterfacesAndSelfTo<ButtonWithSound>().AsCached().WithArguments(graphic);
        }
        Container.BindInterfacesAndSelfTo<LevelCoreMechanicsProvider>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ScoreHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreUIView>().AsSingle().WithArguments(TextWithInGameScore);
        Container.BindInterfacesAndSelfTo<CoinsHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<CoinsUIView>().AsSingle().WithArguments(new ObservableCollection<Text>() { TextWithInGameCoins });
        Container.BindInterfacesAndSelfTo<EarnedCoinsAdder>().AsSingle();
        Container.BindInterfacesAndSelfTo<MovableSideUI>().AsSingle().WithArguments(MovableSideUI);
        Container.BindInterfacesAndSelfTo<SideUIMover>().AsSingle();
        Container.BindInterfacesAndSelfTo<SideUIMovingButton>().AsSingle().WithArguments(SideUIMovingButton);
        Container.BindInterfacesAndSelfTo<SideUICloser>().AsSingle();
        Container.BindInterfacesAndSelfTo<SideUIClosingButton>().AsSingle().WithArguments(SideUIClosingButton);
        Container.BindInterfacesAndSelfTo<MovableFinalWindow>().AsSingle().WithArguments(FinalWindow);
        Container.BindInterfacesAndSelfTo<GameFinishView>().AsSingle().WithArguments(TextWithScoreOnPerfectPopup, TextWithCoinsOnPerfectPopup);
        Container.BindInterfacesAndSelfTo<BackButton>().AsCached().WithArguments(HomeButtonInGame);
        Container.BindInterfacesAndSelfTo<BackButton>().AsCached().WithArguments(HomeButtonOnFinalScreen);
        Container.BindInterfacesAndSelfTo<RestartButton>().AsCached().WithArguments(RestartButtonInGame);
        Container.BindInterfacesAndSelfTo<RestartButton>().AsCached().WithArguments(RestartButtonOnFinalScreen);
        Container.Bind<AudioSwitcher>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioSwitcherView>().AsSingle().WithArguments(AudioEnabledSign, AudioDisabledSign);
        Container.BindInterfacesAndSelfTo<AudioChangingButton>().AsSingle().WithArguments(AudioSwitchingButton);
    }
}