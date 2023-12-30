using GameModule.BusinessLogicModule.PlayerUIActions;
using SharedModule.UIModule.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameModule.UIModule.Window
{
	public class MainMenuWindow : BaseWindow
	{
		[SerializeField] private Button _characterMenuButton;
		
		//todo: изменить после попапа
		[SerializeField] private Button _newGameButton;
		[SerializeField] private Button _continueGameButton;

		private MenuAction _menuAction;

		[Inject]
		public void Construct(MenuAction __menuAction)
		{
			_menuAction = __menuAction;
		}
		protected override void Awake()
		{
			base.Awake();
			
			SubscribeActions();
		}
		
		public override void OnShow(object[] args)
		{
			bool gameExists = (bool) args[0];
			
			Debug.Log("GameExists = " + gameExists);
			
			_continueGameButton.interactable = gameExists;
		}
		public override void OnHide()
		{
			
		}
		
		private void SubscribeActions()
		{
			_characterMenuButton.onClick.AddListener(OnCharacterButtonClick);
			
			_newGameButton.onClick.AddListener(OnNewGameClick);
			
			_continueGameButton.onClick.AddListener(OnContinueGameClick);
		}

		private void OnCharacterButtonClick() { this.Hide(); WindowsCollection.Get<CharacterWindow>().Show(); }
		private void OnNewGameClick() { this.Hide(); _menuAction.Rise(MenuLogicAction.NewGame); }
		private void OnContinueGameClick() { this.Hide(); _menuAction.Rise(MenuLogicAction.ContinueGame); }
	}
}
