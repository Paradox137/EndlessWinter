using System.Collections.Generic;
using System.Linq;
using GameModule.DataModule;
using UnityEngine;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "ChapterFlowConfig", menuName = "MyAssets/Game/Settings/ChapterFlowConfig")]
	public class ChapterFlowConfig : ScriptableObject
	{
		public List<DialogueFlowConfig> DialogueFlowConfigs;

		public ChapterFlow GenerateChapterFlowClass()
		{
			List<DialogueFlow> dialogueFlows = new List<DialogueFlow>(DialogueFlowConfigs.Count);
			
			//dialogueFlows.AddRange(DialogueFlowConfigs.Select(__dialogueFlowConfig => __dialogueFlowConfig.GenerateDialogueFlowClass()));

			return new ChapterFlow(dialogueFlows);
		}
	}
}
