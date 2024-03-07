using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "ChapterFlowConfig", menuName = "MyAssets/Game/Settings/ChapterFlowConfig")]
	public class ChapterFlowConfig : ScriptableObject
	{
		public List<AssetReferenceT<DialogueFlowConfig>> DialogueFlowConfigs;
	}
}
