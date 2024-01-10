using GameModule.BusinessLogicModule.PlayerUIActions;
using SharedModule.UIModule.Window;
using UnityEngine;
using UnityEngine.UI;

namespace GameModule.UIModule.Window
{
	public class AssertNewGamePopupWindow : BaseWindow
	{
		[SerializeField] private Button _yesButton;
		[SerializeField] private Button _noButton;

		private MenuAction _newGameAction;
		protected override void OnShow(object[] args)
		{
			_newGameAction = (MenuAction) args[0];

			SubscribeActions();
		}

		protected override void OnHide()
		{
			CleanUp();
		}
		
		private void SubscribeActions()
		{
			_yesButton.onClick.AddListener(CreateNewGame);
			_noButton.onClick.AddListener(ClosePopUp);
		}

		private void CreateNewGame() => _newGameAction.Rise(MenuLogicAction.NewGame);
		private void ClosePopUp() => this.Hide();
		
		private void CleanUp()
		{
			_yesButton.onClick.RemoveListener(CreateNewGame);
			_noButton.onClick.RemoveListener(ClosePopUp);
		}
	}
}
