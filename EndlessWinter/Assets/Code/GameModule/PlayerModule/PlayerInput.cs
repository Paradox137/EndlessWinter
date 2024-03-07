using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.UIModule.Signals;
using SharedModule.CustomizeModule;
using UnityEngine.UI;
using Zenject;

namespace GameModule.PlayerModule
{
	public class PlayerInput
	{
		private readonly TextWriterService _textWriterService;
		private readonly SignalBus _signalBus;

		[Inject]
		public PlayerInput(TextWriterService __textWriterService, Button __nextButton, SignalBus __signalBus)
		{
			_textWriterService = __textWriterService;
			_signalBus = __signalBus;
			
			__nextButton.onClick.AddListener(HandleInput);
		}
		
		private void HandleInput()
		{
			if (_textWriterService.IsBusy && _textWriterService.Mode == WriteMode.NormalMode)
			{
				CustomDebug.WriteLine("PlayerInput", "Change Writing to Speed Mode", CustomDebugColors.Green);
				_textWriterService.ChangeMode(WriteMode.SpeedMode);
			}
			
			else if (_textWriterService.IsBusy && _textWriterService.Mode == WriteMode.SpeedMode)
			{
				CustomDebug.WriteLine("PlayerInput", "Show All Text Permanent", CustomDebugColors.Green);
				_signalBus.Fire<ShowAllText>();
			}
			
			else if (_textWriterService.IsBusy == false)
			{
				CustomDebug.WriteLine("PlayerInput", "Get Next Novel Data", CustomDebugColors.Green);
				_signalBus.Fire<NextNovelData>();
			}
		}
	}
}
