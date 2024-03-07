using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.Signals;
using UniRx;
using Zenject;

namespace GameModule.UIModule.MVC.View.Image
{
	public abstract class ImageView : SignalView
	{
		[Inject]
		protected readonly IImageService ImageService;      
			
		protected readonly UnityEngine.UI.Image Image;
		protected readonly ImagePresenter Presenter;
		protected readonly ImageModel Model;
		
		[Inject]
		public ImageView(SignalBus __signalBus, UnityEngine.UI.Image __image, ImagePresenter __presenter, ImageModel __model) : base(__signalBus)
		{
			Image = __image;
			Presenter = __presenter;
			Model = __model;
		}
		protected override void SetupSignals()
		{
			SignalBus.Subscribe<GameInitSignal>(_ =>
			{
				Presenter.OnNext();
			});
		}
		protected override void SetupObserves()
		{
			Model.Sprite
				.ObserveEveryValueChanged(sprite => sprite.Value)
				.Subscribe(sprite =>
				{
					ImageService.Output(Image, sprite);
				}).AddTo(Image.gameObject);
		}
	}
}
