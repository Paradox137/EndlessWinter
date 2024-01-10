using GameModule.CollectionModule;
using GameModule.PlayerModule;
using GameModule.SettingsModule;
using UnityEngine;
using Zenject;

namespace GameModule
{
	public class GlobalInstaller_TODO_ : MonoInstaller
	{
		[SerializeField] private PlayerDataSettings _playerSettings;
		public override void InstallBindings()
		{
			PlayerSaveLoadSystem saveLoadSystem = new PlayerSaveLoadSystem(_playerSettings.PerkEntities);
			Container.BindInterfacesAndSelfTo<PlayerSaveLoadSystem>().FromInstance(saveLoadSystem).AsSingle();


			Container.Bind<PerkCollection>().AsSingle().WithArguments(saveLoadSystem.GetPlayerData().PerkEntities).NonLazy();
		}
	}
}
