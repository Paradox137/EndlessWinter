using SharedModule.CollectionModule;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.Window.Installers
{
	public class GameMenuInstaller : MonoInstaller
	{
		[SerializeField] private QuestChoicePopupWindow _choicePopupWindow;
		[SerializeField] private GameEndWindow _gameEndWindow;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<WindowsCollection>().FromNew().AsCached().NonLazy();

			Container.BindInterfacesAndSelfTo<QuestChoicePopupWindow>().FromInstance(_choicePopupWindow).AsSingle();
			Container.BindInterfacesAndSelfTo<GameEndWindow>().FromInstance(_gameEndWindow).AsSingle();
		}
	}
}
	