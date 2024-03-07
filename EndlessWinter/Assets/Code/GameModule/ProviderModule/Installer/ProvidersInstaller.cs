using GameModule.ProviderModule.Sprite;
using GameModule.ProviderModule.Text;
using Zenject;

namespace GameModule.ProviderModule.Installer
{
	public class ProvidersInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<TextProvider>().WithId(ProviderID.ActorNameText).FromNew().AsCached();
			Container.Bind<TextProvider>().WithId(ProviderID.ActorNovelText).FromNew().AsCached();
			Container.Bind<SpriteProvider>().WithId(ProviderID.ActorSprite).FromNew().AsCached();
			Container.Bind<SpriteProvider>().WithId(ProviderID.MainSprite).FromNew().AsCached();
			
		}
	}
}
