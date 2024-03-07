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
    public class ActorTextView : TextView
    {
        [Inject]
        protected ActorTextView(SignalBus __signalBus, TextPresenter __presenter, TextModel __model,
            TextWriterService __writerService, TextMeshProUGUI __screenText)
            : base(__signalBus, __presenter, __model, __writerService, __screenText)
        { }
        
        protected override void SetupSignals()
        {
            base.SetupSignals();
            
            SignalBus.Subscribe<NextActorTextSignal>(_ => { Presenter.OnNext(); });
        }
        
        protected override void SetupObserves()
        {
            Model.Text
                .ObserveEveryValueChanged(text => text.Value)
                .Subscribe(text =>
                {
                    WriterService.OutputActorNameText(ScreenText, text);
                }).AddTo(ScreenText.gameObject);
        }
    }
}