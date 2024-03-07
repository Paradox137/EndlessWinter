using System;
using System.Collections.Generic;
using System.Linq;
using Dre0Dru.AddressableAssets.Loaders;
using GameModule.ConfigsModule;
using GameModule.DataModule.Novel;
using UnityEngine;

namespace GameModule.DataModule
{
	public static class DataExtensions
	{
		private static readonly Dictionary<PerkType, string> PlayerPerks = new Dictionary<PerkType, string>()
		{		
			{ PerkType.Leadership,     "Лидерство"},
			{ PerkType.Friendship,     "Дружба"},
			{ PerkType.Love,           "Любовь"},
			{ PerkType.Popularity,     "Популярность"},
			{ PerkType.Savvy,          "Cмекалка"},
			{ PerkType.Initiative,     "Инициатива"},
			{ PerkType.SelfDiscipline, "Самодисциплина"},
			{ PerkType.Honesty,        "Честность"},
			{ PerkType.Generosity,     "Щедрость"},
			{ PerkType.Bravery,        "Xрабрость"},
			{ PerkType.Determination,  "Решимость"},
			{ PerkType.Coolness,       "Крутость"},
		};
		
		private static readonly Dictionary<ActorType, string> ActorNames = new Dictionary<ActorType, string>()
		{		
			{ ActorType.Gamer,                ""},
			{ ActorType.AnnaVladimitovna,     "Анна Владимировна"},
			{ ActorType.Kat9,                 "Катя"},
			{ ActorType.Nast9,                "Настя"},
			{ ActorType.Ton9,                 "Тоня"},
			{ ActorType.Veronika,             "Вероника"},
			{ ActorType.Alisa,                "Алиса"},
			{ ActorType.San9,                 "Саня"},
			{ ActorType.Andrey,               "Андрей"},
		};
		public static Queue<KeyValuePair<ActorType, Sprite>> ToQueue(this DialogueCustomDictionary __dialogueCustomQueue,
			AssetsReferenceLoader<Sprite> __spriteLoader)
		{
			Queue<KeyValuePair<ActorType, Sprite>> variableQueue = new Queue<KeyValuePair<ActorType, Sprite>>(__dialogueCustomQueue.Items.Length);

			foreach (DialogueItem item in __dialogueCustomQueue.Items)
			{
				if (!string.IsNullOrEmpty(item.ActorInfo.AssetGUID))
				{
					KeyValuePair<ActorType, Sprite> keyValuePair = new KeyValuePair<ActorType, Sprite>(item.ActorType, __spriteLoader.GetAsset(item.ActorInfo));
					variableQueue.Enqueue(keyValuePair);
				}
				else
				{
					variableQueue.Enqueue(new KeyValuePair<ActorType, Sprite>(item.ActorType, null));
				}
				
			}
			
			return variableQueue;
		}

		public static Dictionary<uint, Sprite> ToDictionary(this ImageFlowCustomDictionary __dialogueCustomQueue,
			AssetsReferenceLoader<Sprite> __spriteLoader)
		{
			Dictionary<uint, Sprite> variableDictionary = new Dictionary<uint, Sprite>(__dialogueCustomQueue.Items.Length);

			foreach (ImageFlowItem item in __dialogueCustomQueue.Items)
			{
				variableDictionary.Add(item.viewPhase, __spriteLoader.GetAsset(item.ImageSprite));
			}

			return variableDictionary;
		}
		public static DialogueFlow.ChoiceInfluence ToChoiceInfluence(this ChoiceInfluenceCustomDictionary __dictionary)
		{
			List<(PerkType, int)> positiveInfluence = __dictionary.PositiveChoice.Select(__item => (__item.Perk, __item.Influence)).ToList() ?? throw new ArgumentNullException("__dictionary.PositiveChoice.Select(__item => (__item.Perk, __item.Influence)).ToList()");
			List<(PerkType, int)> negativeInfluence = __dictionary.NegativeChoice.Select(__item => (__item.Perk, __item.Influence)).ToList();

			DialogueFlow.ChoiceInfluence choiceInfluence = new DialogueFlow.ChoiceInfluence
			{
				PositiveChoiceText = __dictionary.PositiveChoiceText,
				NegativeChoiceText = __dictionary.NegativeChoiceText,
				NegativeInfluence = negativeInfluence,
				PositiveInfluence = positiveInfluence
			};

			return choiceInfluence;
		}

		public static void DequeueMulty<T>(this Queue<T> __queue, int __chunkSize) 
		{
			for (int i = 0; i < __chunkSize && __queue.Count > 0; i++)
			{
				__queue.Dequeue();
			}
		}
		public static string GetActorName(this ActorType __characteristic)
		{
			return ActorNames[__characteristic];
		}
		public static string GetPerkDescription(this PerkType __characteristic)
		{
			return PlayerPerks[__characteristic];
		}
		public static PerkType GetPerkType(this string __perkName)
		{
			return PlayerPerks.FirstOrDefault(__n => __n.Value == __perkName).Key;
		}
		
		public static ActorType GetActorType(this string __perkName)
		{
			return ActorNames.FirstOrDefault(__n => __n.Value == __perkName).Key;
		}
	}
}
