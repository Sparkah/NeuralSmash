using Content.Game.UI.Scripts.Narrate;
using UnityEngine;
using Zenject;

namespace Content.Game.SceneManagement.Installers
{
    public class UINarrativeInstaller : MonoInstaller
    {
        [SerializeField] private UINarrate _uiNarrateManager;

        public override void InstallBindings()
        {
            var uiInstance = Container.Bind<UINarrate>().FromInstance(_uiNarrateManager);
        }
    }
}