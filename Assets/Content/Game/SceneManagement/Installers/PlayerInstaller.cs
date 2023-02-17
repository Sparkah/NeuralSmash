using Content.Game.Player.Scripts;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;

    public override void InstallBindings()
    {
        var playerInstance = Container.Bind<PlayerController>().FromInstance(_playerController);
    }
}
