using GameModule.UIModule.Window;
using SharedModule.UIModule.Window;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class MainMenuState : NovelState
	{
		[Inject]
		
		public MainMenuState()
		{
			
		}

		public override void Enter()
		{
			WindowsCollection.Get<MainMenuWindow>().Show();
		}
	}
}
