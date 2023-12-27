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
			SaveLoadSystem saveLoadSystem = new SaveLoadSystem(_playerSettings.PerkEntities);
			Container.BindInterfacesAndSelfTo<SaveLoadSystem>().FromInstance(saveLoadSystem).AsSingle();


			Container.Bind<PerkCollection>().AsSingle().WithArguments(saveLoadSystem.PlayerData.PerkEntities).NonLazy();
		}
	}
}
