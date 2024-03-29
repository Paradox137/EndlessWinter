﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameModule.UIModule.Window
{
	public class PerkView : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _name;
		[SerializeField] private Image _progressImage;
		[SerializeField] private TextMeshProUGUI _progressValue;
		public TextMeshProUGUI ProgressValue => _progressValue;
		public Image ProgressImage => _progressImage;
		public TextMeshProUGUI Name => _name;
		public Image Icon => _icon;
	}
}
