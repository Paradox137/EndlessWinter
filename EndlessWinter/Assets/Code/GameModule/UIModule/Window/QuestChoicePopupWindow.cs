using System;
using GameModule.DataModule.Novel;
using GameModule.ServiceModule;
using GameModule.ServiceModule.InGameModule;
using SharedModule.UIModule.Window;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameModule.UIModule.Window
{
	public class QuestChoicePopupWindow : BaseWindow
	{
		[SerializeField] private TextMeshProUGUI _positiveText;
		[SerializeField] private Button _positiveButton;
		[SerializeField] private Button _negativeButton;
		[SerializeField] private TextMeshProUGUI _negativeText;
		
		private DialogueFlow.ChoiceInfluence _choiceInfluence;
		
		protected override void OnShow(object[] __args)
		{
			DialogueFlow.ChoiceInfluence novelFlowController = (DialogueFlow.ChoiceInfluence) __args[0];

			Action<ControlMode> callback = (Action<ControlMode>) __args[1];
			
			_positiveText.text = novelFlowController.PositiveChoiceText;
			_negativeText.text = novelFlowController.NegativeChoiceText;
			
			SubscribeActions(callback);
		}
		private void SubscribeActions(Action<ControlMode> __callback)
		{
			_positiveButton.onClick.AddListener(this.Hide);
			_positiveButton.onClick.AddListener(() => __callback.Invoke(ControlMode.Positive));

			_negativeButton.onClick.AddListener(this.Hide);
			_negativeButton.onClick.AddListener(() => __callback.Invoke(ControlMode.Negative));
		}
		
		protected override void OnHide()
		{
			Cleanup();
		}
		
		private void Cleanup()
		{
			_positiveButton.onClick.RemoveAllListeners();
			_negativeButton.onClick.RemoveAllListeners();
		}
	}
}
