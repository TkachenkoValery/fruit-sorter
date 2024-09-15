using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CrossSceneSystemsInstaller", menuName = "Installers/ScriptableObjectInstallers/CrossSceneSystemsInstaller")]
public class CrossSceneSystemsInstaller : ScriptableObjectInstaller
{
    [SerializeField] private AudioEnabledStateHandler AudioEnabledStateHandler;
    [SerializeField] private CoinsHandlerConfig CoinsHandlerConfig;

    public override void InstallBindings()
    {
        Container.Bind<AudioEnabledStateHandler>().FromInstance(AudioEnabledStateHandler).AsSingle();
        Container.Bind<CoinsHandlerConfig>().FromInstance(CoinsHandlerConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<PreviousSceneLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameRestarter>().AsSingle();
        Container.BindInterfacesAndSelfTo<NextSceneLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<ApplicationQuitter>().AsSingle();
    }
}