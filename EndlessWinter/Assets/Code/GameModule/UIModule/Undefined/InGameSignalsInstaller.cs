    using SharedModule.UIModule.Signals;
using Zenject;

namespace GameModule.UIModule.MVP.Installers
{
    public class InGameSignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<NextWriteTextSignal>();
            Container.DeclareSignal<NextStaticTextSignal>();
        }
    }
}