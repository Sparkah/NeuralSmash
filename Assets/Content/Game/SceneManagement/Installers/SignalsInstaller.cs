using Zenject;

namespace Content.Game.SceneManagement.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            /*Подключение сигналов*/
            SignalBusInstaller.Install(Container);
            /*сигналы*/
            InitialSignals();
        }

        private void InitialSignals()
        {
            Container.DeclareSignal<LevelUpSignal>();
            Container.DeclareSignal<CloseUINarrativeSignal>();
        }
    }
}