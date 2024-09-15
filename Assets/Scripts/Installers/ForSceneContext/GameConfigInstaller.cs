using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/ScriptableObjectInstallers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private LevelCoreMechanicsProviderConfig LevelCoreMechanicsProviderConfig;
    [SerializeField] private ScoreHandlerConfig ScoreHandlerConfig;
    [SerializeField] private MovableFinalWindowConfig MovableFinalWindowConfig;
    [SerializeField] private MovableSideUIConfig MovableSideUIConfig;

    public override void InstallBindings()
    {
        Container.Bind<LevelCoreMechanicsProviderConfig>().FromInstance(LevelCoreMechanicsProviderConfig).AsSingle();
        Container.Bind<ScoreHandlerConfig>().FromInstance(ScoreHandlerConfig).AsSingle();
        Container.Bind<MovableFinalWindowConfig>().FromInstance(MovableFinalWindowConfig).AsSingle();
        Container.Bind<MovableSideUIConfig>().FromInstance(MovableSideUIConfig).AsSingle();
    }
}