using System.Collections.Generic;
using GameModule.CollectionModule;
using GameModule.PlayerModule;
using SharedModule.UIModule.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameModule.UIModule.Window
{
	public class CharacterWindow : BaseWindow
	{
		[SerializeField] private PerkView[] _perkViews;
		[SerializeField] private Button _exitButton;
		
		private PerkCollection _perkCollection;

		[Inject]
		public void Construct(PerkCollection __perkCollection)
		{
			_perkCollection = __perkCollection;
		}

		protected override void Awake()
		{
			base.Awake();
			
		}

		protected override void OnShow(object[] args)
		{
			for (int i = 0; i < _perkViews.Length; i++)
			{
				PerkType perkType = _perkCollection.GetPerkType(i);
				
				_perkViews[i].Name.text = _perkCollection.GetPerkName(perkType);
				
				int currentPerkProgress = _perkCollection.GetPerkValue(perkType);
				_perkViews[i].ProgressValue.text = $"{currentPerkProgress}/100";
				_perkViews[i].ProgressImage.fillAmount = currentPerkProgress / 100f;
			}
			
			SubscribeActions();
		}
		protected override void OnHide()
		{
			Cleanup();
		}

		private void SubscribeActions()
		{
			_exitButton.onClick.AddListener(OnExitButtonClick);
		}
		
		private void OnExitButtonClick()
		{
			this.Hide();
			
			WindowsCollection.Get<MainMenuWindow>().LocalCachedShow();
		}
		
		private void Cleanup()
		{
			_exitButton.onClick.RemoveListener(OnExitButtonClick);
		}

	}
}
