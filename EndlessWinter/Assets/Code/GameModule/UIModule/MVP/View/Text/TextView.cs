using GameModule.ServiceModule;
using GameModule.UIModule.MVP.Model;
using GameModule.UIModule.MVP.Presenter;
using SharedModule.UIModule.Signals;
using TMPro;
using Zenject;

namespace GameModule.UIModule.MVP.View
{
    public class TextView : SignalView
    {
        protected readonly TextMeshProUGUI _screenText;
        protected readonly TextWriterService _writerService;
        
        protected readonly TextPresenter _presenter;
        protected readonly TextModel _model;
        
        [Inject]
        protected TextView(SignalBus __signalBus, TextPresenter __presenter, TextModel __model, 
            TextWriterService __writerService , TextMeshProUGUI ___screenText)
            : base(__signalBus)
        {
            _presenter = __presenter;
            _model = __model;
            _screenText = ___screenText;
            _writerService = __writerService;
        }

        protected override void SetupSignals()
        {
        }

        protected override void SetupObserves()
        {
        }
    }
}