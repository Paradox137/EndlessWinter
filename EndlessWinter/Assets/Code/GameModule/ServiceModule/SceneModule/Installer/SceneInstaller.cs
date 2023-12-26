using SharedModule.ServiceModule.SceneModule;
using Zenject;

namespace GameModule.ServiceModule.SceneModule
{
	public class SceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
			
			Container.Bind<SceneLoader>().AsSingle();
		}
	}
}
