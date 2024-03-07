using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.Signals;
using Zenject;

namespace GameModule.UIModule.MVC.View.Image
{
	public class ActorImageView : ImageView
	{

		public ActorImageView(SignalBus __signalBus, UnityEngine.UI.Image __image, ImagePresenter __presenter, ImageModel __model) 
			: base(__signalBus, __image, __presenter, __model) { }
		
		
		protected override void SetupSignals()
		{
			base.SetupSignals();
			
			SignalBus.Subscribe<NextActorImageSignal>(_ =>
			{
				Presenter.OnNext();
			});
			
		}
	}
}
