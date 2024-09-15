using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IdleAnimationInstaller", menuName = "Installers/ScriptableObjectInstallers/IdleAnimationInstaller")]
public class IdleAnimationInstaller : ScriptableObjectInstaller
{
    [SerializeField] private IdleAnimationConfig IdleAnimationConfig;

    public override void InstallBindings()
    {
        Container.Bind<IdleAnimationConfig>().FromInstance(IdleAnimationConfig).AsSingle();
    }
}