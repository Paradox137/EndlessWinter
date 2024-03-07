using SharedModule.CollectionModule;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.Window.Installers
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private CharacterWindow _characterWindow;
		[SerializeField] private MainMenuWindow _mainMenuWindow;
		[SerializeField] private AssertNewGamePopupWindow _assertNewGamePopupWindow;
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<WindowsCollection>().AsCached();

			Container.BindInterfacesAndSelfTo<CharacterWindow>().FromInstance(_characterWindow).AsSingle();
			Container.BindInterfacesAndSelfTo<MainMenuWindow>().FromInstance(_mainMenuWindow).AsSingle();
			Container.BindInterfacesAndSelfTo<AssertNewGamePopupWindow>().FromInstance(_assertNewGamePopupWindow).AsSingle();
		}
	}
}
