using System.Collections.Generic;
using DG.Tweening;
using GameModule.DataModule;
using TMPro;
using UnityEngine;

namespace GameModule.ServiceModule.InGameModule
{
	public class UIPerkViewerService
	{
		private readonly RectTransform _panelRect;
		private readonly GameObject _perksParent;
		private readonly GameObject _perkChild;
		
		public UIPerkViewerService(RectTransform __panelRect, GameObject __perksParent, GameObject __perkChild, NovelFlowController __novelFlowController)
		{
			_panelRect = __panelRect;
			_perksParent = __perksParent;
			_perkChild = __perkChild;

			__novelFlowController.onPlayerChoiced += ShowUpgradesPerks;
		}
		private void ShowUpgradesPerks(List<(PerkType, int)> __perks)
		{
			Debug.Log("here");
			
			GameObject holder = Object.Instantiate(_perksParent, _panelRect.gameObject.transform, false);
			holder.GetComponent<RectTransform>().localPosition = new Vector3(1200, 0, 0);

			for (int i = 0; i < __perks.Count; i++)
			{
				GameObject perkGO = Object.Instantiate(_perkChild, holder.gameObject.transform, false);
				perkGO.GetComponentInChildren<TextMeshProUGUI>().text =
					$"Характеристика: \"{__perks[i].Item1.GetPerkDescription()}\"> увеличена на +{__perks[i].Item2}";
			}
			
			holder.GetComponent<RectTransform>().DOLocalMove(new Vector3(1200, 1500, 0), 8f).onComplete += OnComplete; 
			
			void OnComplete()
			{
				Object.Destroy(holder);
			}
		}
	}
}
