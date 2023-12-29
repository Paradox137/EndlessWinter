using System.Collections.Generic;
using GameModule.CollectionModule;
using GameModule.PlayerModule;
using SharedModule.UIModule.Window;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.Window
{
	public class CharacterWindow : BaseWindow
	{
		[SerializeField] private PerkView[] _perkViews;

		private PerkCollection _perkCollection;

		[Inject]
		public void Construct(PerkCollection __perkCollection)
		{
			_perkCollection = __perkCollection;
		}
		public override void OnShow(object[] args)
		{
			for (int i = 0; i < _perkViews.Length; i++)
			{
				PerkType perkType = _perkCollection.GetPerkType(i);
				
				_perkViews[i].Name.text = _perkCollection.GetPerkName(perkType);
				
				int currentPerkProgress = _perkCollection.GetPerkValue(perkType);
				_perkViews[i].ProgressValue.text = $"{currentPerkProgress}/100";
				_perkViews[i].ProgressImage.fillAmount = currentPerkProgress / 100f;
			}
		}
		public override void OnHide()
		{
			
		}
	}
}
