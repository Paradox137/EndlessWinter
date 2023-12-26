using Zenject;

namespace GameModule.PlayerModule.Installer
{
	public class SaveLoadInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<SaveLoadSystem>().AsSingle();
		}
	}
}
