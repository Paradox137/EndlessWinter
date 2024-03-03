using System;
using System.Collections.Generic;
using GameModule.DataModule;
using GameModule.PlayerModule;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

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
	}

	[Serializable] public struct DialogueCustomDictionary { public DialogueItem[] Items; }
	[Serializable] public struct ImageFlowCustomDictionary { public ImageFlowItem[] Items; }
	[Serializable] public struct ChoiceInfluenceCustomDictionary { public ChoiceInfluenceItem[] PositiveChoice; public ChoiceInfluenceItem[] NegativeChoice; }
	
	[Serializable] public class ChoiceInfluenceItem { public PerkType Perk; public int Influence; }
	[Serializable] public struct ImageFlowItem { public AssetReferenceT<SpriteAsset> ImageTexture; }
	[Serializable] public struct DialogueItem { public ActorType ActorType; public AssetReferenceT<SpriteAsset> ActorInfo; }
}
