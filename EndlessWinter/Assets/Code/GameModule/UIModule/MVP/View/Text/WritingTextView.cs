using System.Threading;
using GameModule.ServiceModule;
using GameModule.UIModule.MVP.Model;
using GameModule.UIModule.MVP.Presenter;
using SharedModule.UIModule.Signals;
using TMPro;
using UniRx;
using Zenject;

namespace GameModule.UIModule.MVP.View
{
    public class WritingTextView : TextView
    {
        [Inject]
        protected WritingTextView(SignalBus __signalBus, TextPresenter __presenter, TextModel __model, 
            TextWriterService __writerService, TextMeshProUGUI ___screenText)
            : base(__signalBus, __presenter, __model, __writerService, ___screenText)
        { }

        protected override void SetupSignals()
        {
            SignalBus.Subscribe<NextWriteTextSignal>(n =>
            {
                _presenter.OnNext();
            });
        }
        
        protected override void SetupObserves()
        {
            _model.Text
                .ObserveEveryValueChanged(text => text.Value)
                .FirstOrDefault(text => text != null)
                .Subscribe(text =>
                {
                    _writerService.WriteTextLetterByLetter(_screenText, text, new CancellationToken());
                });
        }
    }
}