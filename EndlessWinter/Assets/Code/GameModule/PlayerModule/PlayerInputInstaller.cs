using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameModule.PlayerModule
{
	public class PlayerInputInstaller : MonoInstaller
	{
		[SerializeField] private Button _nextButton;
		public override void InstallBindings()
		{
			Container.Bind<PlayerInput>().FromNew().AsSingle().WithArguments(_nextButton).NonLazy();
		}
	}
}
