using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LoadSceneInstaller", menuName = "Installers/ScriptableObjectInstallers/LoadSceneConfigInstaller")]
public class LoadSceneConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private LoadingAnimationConfig LoadingAnimationConfig;

    public override void InstallBindings()
    {
        Container.Bind<LoadingAnimationConfig>().FromInstance(LoadingAnimationConfig).AsSingle();
    }
}