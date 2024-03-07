using GameModule.BusinessLogicModule.PlayerUIActions;
using SharedModule.CollectionModule;
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

		private MenuAction _newGameAction;
		private bool _gameExists;
		
		[Inject]
		public void Construct(MenuAction __menuAction)
		{
			_newGameAction = __menuAction;
		}
		protected override void Awake()
		{
			base.Awake();
		}

		public override void LocalCachedShow()
		{
			SubscribeActions(_gameExists);
			
			base.LocalCachedShow();
		}

		protected override void OnShow(object[] args)
		{
			_gameExists = (bool) args[0];

			_continueGameButton.interactable = _gameExists;
			
			SubscribeActions(_gameExists);
		}

		protected override void OnHide()
		{
			CleanUp();
		}
		
		private void SubscribeActions(bool __gameExists)
		{
			_characterMenuButton.onClick.AddListener(ShowCharacterWindow);
			
			if(__gameExists)
				_newGameButton.onClick.AddListener(ShowAssertNewGamePopUp);
			else
				_newGameButton.onClick.AddListener(CreateNewGame);
			
			_continueGameButton.onClick.AddListener(ContinueGame);
		}
		
		private void ShowAssertNewGamePopUp() => WindowsCollection.Get<AssertNewGamePopupWindow>().Show(_newGameAction);
		private void ShowCharacterWindow() { this.Hide(); WindowsCollection.Get<CharacterWindow>().Show(); }
		private void CreateNewGame() => _newGameAction.Rise(MenuLogicAction.NewGame);
		private void ContinueGame() => _newGameAction.Rise(MenuLogicAction.ContinueGame);
		
		private void CleanUp()
		{
			_characterMenuButton.onClick.RemoveListener(ShowCharacterWindow);
			_continueGameButton.onClick.RemoveListener(ContinueGame);
			_newGameButton.onClick.RemoveAllListeners();
		}
	}
}
