using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.Signals;
using Zenject;

namespace GameModule.UIModule.MVC.View.Image
{
	public class MainImageView : ImageView
	{
		[Inject]
		public MainImageView(SignalBus __signalBus, UnityEngine.UI.Image __image, ImagePresenter __presenter, ImageModel __model) 
			: base(__signalBus, __image, __presenter, __model)
		{
			
		}

		protected override void SetupSignals()
		{
			base.SetupSignals();
			
			SignalBus.Subscribe<NextMainImageSignal>(_ =>
			{
				Presenter.OnNext();
			});
			
		}
	}
}
