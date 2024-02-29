using System;
using System.Collections.Generic;
using GameModule.DataModule;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "DialogueFlowConfig", menuName = "MyAssets/Game/Settings/DialogueFlowConfig")]
	public class DialogueFlowConfig : ScriptableObject
	{
		public DialogueCustomDictionary StartFlow;
		public DialogueCustomDictionary PositiveFlow;
		public DialogueCustomDictionary NegativeFlow;
		public DialogueCustomDictionary EndFlow;
		
		public uint actorQuestNumber;
		public ImageFlowCustomDictionary ImageFlow;

		public DialogueFlow GenerateDialogueFlowClass()
		{
			return new DialogueFlow(
				__endFlow: EndFlow.ToQueue(),
				__negativeFlow: NegativeFlow.ToQueue(),
				__positiveFlow: PositiveFlow.ToQueue(),
				__startFlow: StartFlow.ToQueue(),
				__imageFlow: ImageFlow.ToDictionary(),
				__actorQuestNumber: actorQuestNumber);
		}
	}
	
	[Serializable]
	public struct DialogueCustomDictionary
	{
		public DialogueItem[] Items;
	}
	
	[Serializable]
	public struct DialogueItem
	{
		public ActorType ActorType;
		public ActorInfo ActorInfo;
	}
	
	[Serializable]
	public struct ActorInfo
	{
		public Texture ImageTexture;
	}
	
	[Serializable]
	public struct ImageFlowCustomDictionary
	{
		public ImageFlowItem[] Items;
	}
	
	[Serializable]
	public struct ImageFlowItem
	{
		public uint Flow;
		public Texture ImageTexture;
	}
}
