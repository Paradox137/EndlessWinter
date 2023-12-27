using SharedModule.UIModule.Window;
using UnityEngine;
using UnityEngine.UI;

namespace GameModule.UIModule.Window
{
	public class MainMenuWindow : BaseWindow
	{
		[SerializeField] private Button _characterMenuButton;

		protected override void Awake()
		{
			base.Awake();
			
			SubscribeActions();
		}


		public override void OnShow(object[] args)
		{
			
		}
		public override void OnHide()
		{
			
		}
		
		private void SubscribeActions()
		{
			_characterMenuButton.onClick.AddListener(OnCharacterButtonClick);
		}

		private void OnCharacterButtonClick()
		{
			this.Hide();
			
			WindowsCollection.Get<CharacterWindow>().Show();
		}
	}
}
