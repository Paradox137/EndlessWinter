using GameModule.CollectionModule;
using GameModule.ConfigsModule;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.UIModule.Signals;
using UnityEngine;
using Zenject;

namespace GameModule.PlayerModule
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDataConfig _playerSettings;
		public override void InstallBindings()
		{
			InstallSignalBus();

			PlayerSaveLoadSystem saveLoadSystem = new PlayerSaveLoadSystem(_playerSettings.PerkEntities);
			Container.BindInterfacesAndSelfTo<PlayerSaveLoadSystem>().FromInstance(saveLoadSystem).AsSingle();
			
			Container.Bind<PerkCollection>().AsSingle().WithArguments(saveLoadSystem.GetPlayerData().PerkEntities).NonLazy();
		}
		private void InstallSignalBus()
		{
			SignalBusInstaller.Install(Container);

			Container.DeclareSignal<NextWriteTextSignal>();
			Container.DeclareSignal<NextActorTextSignal>();
			Container.DeclareSignal<GameInitSignal>();
			Container.DeclareSignal<ShowAllText>();
			Container.DeclareSignal<NextNovelData>();
			Container.DeclareSignal<NextMainImageSignal>();
			Container.DeclareSignal<NextActorImageSignal>();
		}
	}
}
