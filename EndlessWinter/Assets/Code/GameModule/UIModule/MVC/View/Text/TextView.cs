using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.Signals;
using TMPro;
using Zenject;

namespace GameModule.UIModule.MVC.View.Text
{
    public abstract class TextView : SignalView
    {
        protected readonly TextMeshProUGUI ScreenText;
        protected readonly TextWriterService WriterService;
        
        protected readonly TextPresenter Presenter;
        protected readonly TextModel Model;
        [Inject]
        protected TextView(SignalBus __signalBus, TextPresenter __presenter, TextModel __model, 
            TextWriterService __writerService , TextMeshProUGUI __screenText)
            : base(__signalBus)
        {
            Presenter = __presenter;
            Model = __model;
            ScreenText = __screenText;
            WriterService = __writerService;
        }

        protected override void SetupSignals()
        {
            SignalBus.Subscribe<GameInitSignal>(_ => { Presenter.OnNext(); });
        }

        protected override void SetupObserves()
        {
        }
    }
}