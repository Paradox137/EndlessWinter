using System.Threading;
using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.Signals;
using TMPro;
using UniRx;
using Zenject;

namespace GameModule.UIModule.MVC.View.Text
{
    public class WritingTextView : TextView
    {
        private CancellationTokenSource _cancellationTokenSource;
        [Inject]
        protected WritingTextView(SignalBus __signalBus, TextPresenter __presenter, TextModel __model,
            TextWriterService __writerService, TextMeshProUGUI __screenText)
            : base(__signalBus, __presenter, __model, __writerService, __screenText)
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override void SetupSignals()
        {
            base.SetupSignals();

            SignalBus.Subscribe<NextWriteTextSignal>(_ => { Presenter.OnNext(); });
            SignalBus.Subscribe<ShowAllText>(_ => CancelWriting());
        }
        
        protected override void SetupObserves()
        {
            Model.Text
                .ObserveEveryValueChanged(text => text.Value)
                .Subscribe(text =>
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    WriterService.WriteTextLetterByLetter(ScreenText, text, _cancellationTokenSource.Token);
                }).AddTo(ScreenText.gameObject);
        }
        
        private void CancelWriting()
        {
            _cancellationTokenSource.Cancel();
            WriterService.OutputActorText(ScreenText, Model.Text.Value);
        }
    }
}