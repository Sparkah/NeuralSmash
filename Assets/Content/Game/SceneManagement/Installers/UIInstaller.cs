using Content.Game.UI.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Game.SceneManagement.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;

        public override void InstallBindings()
        {
            var uiInstance = Container.Bind<UIManager>().FromInstance(_uiManager);
        }
    }
}