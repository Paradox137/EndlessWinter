using GameModule.ProviderModule;
using GameModule.UIModule.MVC.Model;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.MVC.Presenter
{
	public class ImagePresenter
	{
		private readonly ImageModel _model;
		private readonly IItemProvider<Sprite> _provider;

		[Inject]
		public ImagePresenter(ImageModel __model, IItemProvider<Sprite> __provider)
		{
			_model = __model;
			_provider = __provider;
		}
        
		public void OnNext() => _model.Sprite.Value = _provider.GetItem();
	}
}
