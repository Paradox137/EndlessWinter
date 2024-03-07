using System;
using System.Collections.Generic;
using GameModule.DataModule;
using GameModule.DataModule.Novel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using TextAsset = UnityEngine.TextAsset;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "DialogueFlowConfig", menuName = "MyAssets/Game/Settings/DialogueFlowConfig")]
	public class DialogueFlowConfig : ScriptableObject
	{
		public DialogueCustomDictionary StartFlow;
		public DialogueCustomDictionary PositiveFlow;
		public DialogueCustomDictionary NegativeFlow;
		public DialogueCustomDictionary EndFlow;
		
		public ImageFlowCustomDictionary ImageFlow;
		
		public ChoiceInfluenceCustomDictionary ChoiceInfluence;
		public uint ActorQuestNumber;

		public List<AssetReferenceT<TextAsset>> ActorsTexts;
	}
	
	[Serializable] public struct DialogueCustomDictionary { public DialogueItem[] Items; }
	[Serializable] public struct DialogueItem { public ActorType ActorType; public AssetReferenceT<Sprite> ActorInfo; }
	[Serializable] public struct ImageFlowCustomDictionary { public ImageFlowItem[] Items; }
	[Serializable] public struct ImageFlowItem { [FormerlySerializedAs("ImageTexture")]
		public AssetReferenceT<Sprite> ImageSprite; public uint viewPhase; }
	
	[Serializable] public class ChoiceInfluenceItem { public PerkType Perk; public int Influence; }
	[Serializable] public struct ChoiceInfluenceCustomDictionary
	{
		public string PositiveChoiceText;
		public string NegativeChoiceText;
		public ChoiceInfluenceItem[] PositiveChoice;
		public ChoiceInfluenceItem[] NegativeChoice;
	}
}
