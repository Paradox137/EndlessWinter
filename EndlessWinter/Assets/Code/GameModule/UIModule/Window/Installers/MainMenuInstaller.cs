﻿using SharedModule.UIModule.Window;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.Window.Installers
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private CharacterWindow _characterWindow;
		[SerializeField] private MainMenuWindow _mainMenuWindow;
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<WindowsCollection>().AsTransient();

			Container.BindInterfacesAndSelfTo<CharacterWindow>().FromInstance(_characterWindow).AsSingle();
			Container.BindInterfacesAndSelfTo<MainMenuWindow>().FromInstance(_mainMenuWindow).AsSingle();
		}
	}
}
