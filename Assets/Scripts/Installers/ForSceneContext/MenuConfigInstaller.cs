using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MenuConfigInstaller", menuName = "Installers/ScriptableObjectInstallers/MenuConfigInstaller")]
public class MenuConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private MovableSideUIConfig MovableSideUIConfig;

    public override void InstallBindings()
    {
        Container.Bind<MovableSideUIConfig>().FromInstance(MovableSideUIConfig).AsSingle();
    }
}