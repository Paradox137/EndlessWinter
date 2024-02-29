using System.Collections;
using System.Collections.Generic;
using GameModule.ConfigsModule;
using UnityEngine;

namespace GameModule.DataModule
{
	public static class DataExtensions
	{
		private static Queue<KeyValuePair<ActorType, ActorInfo>> _variableQueue;
		private static Dictionary<uint, Texture> _variableDictionary;
		
		public static Queue<KeyValuePair<ActorType, ActorInfo>> ToQueue(this DialogueCustomDictionary __dialogueCustomQueue)
		{
			_variableQueue = new Queue<KeyValuePair<ActorType, ActorInfo>>(__dialogueCustomQueue.Items.Length);

			foreach (DialogueItem item in __dialogueCustomQueue.Items)
			{
				_variableQueue.Enqueue(new KeyValuePair<ActorType, ActorInfo>(item.ActorType, item.ActorInfo));
			}

			return _variableQueue;
		}
		
		public static Dictionary<uint, Texture> ToDictionary(this ImageFlowCustomDictionary __dialogueCustomQueue)
		{
			_variableDictionary = new Dictionary<uint, Texture>(__dialogueCustomQueue.Items.Length);
			
			foreach (ImageFlowItem item in __dialogueCustomQueue.Items)
			{
				_variableDictionary.Add(item.Flow, item.ImageTexture);
			}

			return _variableDictionary;
		}
	}
}
