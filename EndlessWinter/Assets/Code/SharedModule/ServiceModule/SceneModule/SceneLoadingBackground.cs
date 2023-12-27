using UnityEngine;
using UnityEngine.Serialization;

namespace SharedModule.ServiceModule.SceneModule
{
	[CreateAssetMenu(fileName = "SceneLoaderBackground", menuName = "MyAssets/Shared/Scenes/SceneLoaderBackground")]
	public class SceneLoadingBackground : SceneLoadingSettings
	{
		public SceneReference TransitionScene;
	}
}
