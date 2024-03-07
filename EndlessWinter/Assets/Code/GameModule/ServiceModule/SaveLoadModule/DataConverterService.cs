using Dre0Dru.AddressableAssets.Loaders;
using GameModule.ConfigsModule;
using GameModule.DataModule;
using GameModule.DataModule.Novel;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameModule.ServiceModule.SaveLoadModule
{
	public class DataConverterService
	{
		private Actor[] _variableActors;
		private DialogueFlow[] _variableDialogueFlows;
		
		private readonly AssetsReferenceLoader<TextAsset> _textLoader;
		private readonly AssetsReferenceLoader<Sprite> _spriteLoader;
		
		public DataConverterService(ref AssetsReferenceLoader<Sprite> __spriteLoader, ref AssetsReferenceLoader<TextAsset> __textLoader)
		{
			_textLoader = __textLoader;
			_spriteLoader = __spriteLoader;
		}
		
		public DialogueFlow[] GenerateDialogueFlowClass(DialogueFlowConfig[] __dialogueConfigs, ushort __currentFlow, ushort __savedPart)
		{
			_variableDialogueFlows = new DialogueFlow[__dialogueConfigs.Length];

			for (int i = 0; i < __dialogueConfigs.Length; i++)
			{
				DialogueFlow dialogueFlow = new DialogueFlow
				(
					__dialogueConfigs[i].EndFlow.ToQueue(_spriteLoader),
					__dialogueConfigs[i].NegativeFlow.ToQueue(_spriteLoader),
					__dialogueConfigs[i].PositiveFlow.ToQueue(_spriteLoader),
					__dialogueConfigs[i].StartFlow.ToQueue(_spriteLoader),
					__dialogueConfigs[i].ImageFlow.ToDictionary(_spriteLoader),
					__dialogueConfigs[i].ActorQuestNumber,
					__dialogueConfigs[i].ChoiceInfluence.ToChoiceInfluence(),
					GenerateActorClasses(__dialogueConfigs[i]),
					0
				);
				
				if (i == __savedPart)
					dialogueFlow.CurrentFlow = __currentFlow;
				
				_variableDialogueFlows[i] = dialogueFlow;
			}

			return _variableDialogueFlows;
		}

		private Actor[] GenerateActorClasses(DialogueFlowConfig __dialogueConfig)
		{
			_variableActors = new Actor[__dialogueConfig.ActorsTexts.Count];

			for (int i = 0; i < __dialogueConfig.ActorsTexts.Count; i++)
			{
				AssetReferenceT<TextAsset> textRef = __dialogueConfig.ActorsTexts[i];
				TextAsset text = _textLoader.GetAsset(textRef);

				Actor actor = JsonConvert.DeserializeObject<Actor>(text.text);

				_variableActors[i] = actor;
			}

			return _variableActors;
		}
	}
}
