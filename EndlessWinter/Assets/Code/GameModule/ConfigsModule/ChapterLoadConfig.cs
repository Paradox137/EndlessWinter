using System.Collections.Generic;
using UnityEngine;

namespace GameModule.ConfigsModule
{	
	[CreateAssetMenu(fileName = "ChapterConfig", menuName = "MyAssets/Game/Settings/ChapterConfig")]
	public class ChapterLoadConfig : ScriptableObject
	{
		public List<ChapterFlowConfig> _ChapterFlowConfigs;
	}
}
