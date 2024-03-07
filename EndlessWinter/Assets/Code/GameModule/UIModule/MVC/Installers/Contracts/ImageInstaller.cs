using GameModule.ProviderModule;
using GameModule.ProviderModule.Sprite;
using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.MVC.View.Image;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameModule.UIModule.MVC.Installers.Contracts
{
	public abstract class ImageViewInstaller<T> : MonoInstaller
		where T : ImageView
	{
		[SerializeField] private Image _imageComponent;
		[SerializeField] private ProviderID _providerID;
		[SerializeField] private ServiceID _serviceID;
		public override void InstallBindings()
		{
			_imageComponent.color = Color.white;
			
			Container.BindInterfacesAndSelfTo<T>()
				.FromSubContainerResolveAll()
				.ByMethod(InstallFacade)
				.AsSingle()
				.NonLazy();
		}

		private void InstallFacade(DiContainer subContainer)
		{
			SpriteProvider textProvider = Container.ResolveId<SpriteProvider>(_providerID);
			IImageService imageService = Container.ResolveId<IImageService>(_serviceID);
			
			subContainer.Bind<ImageModel>().FromNew().AsSingle();
			subContainer.Bind<ImagePresenter>().AsSingle().WithArguments(textProvider);
			subContainer.BindInterfacesAndSelfTo<T>().FromNew().AsSingle().WithArguments(_imageComponent, imageService);
		}
	}
}
