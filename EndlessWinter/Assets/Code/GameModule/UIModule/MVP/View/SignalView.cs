using Zenject;

namespace GameModule.UIModule.MVP.View
{
    public abstract class SignalView : IInitializable
    {
        protected readonly SignalBus SignalBus;
        
        public SignalView(SignalBus __signalBus)
        {
            SignalBus = __signalBus;
            
            SetupSignals();
        }

        public void Initialize() => SetupObserves();

        protected abstract void SetupSignals();
        protected abstract void SetupObserves();
    }
}