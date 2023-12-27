using System;
using GameModule.UIModule.Window;
using SharedModule.UIModule.Window;
using UnityEngine;

namespace GameModule.BusinessLogicModule
{
	public class MainMenuStartup : MonoBehaviour
	{
		private void Awake()
		{
			WindowsCollection.Get<MainMenuWindow>().Show();
		}
	}
}
