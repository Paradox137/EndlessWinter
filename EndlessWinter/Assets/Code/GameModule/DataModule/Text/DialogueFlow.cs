using System;
using System.Collections.Generic;
using GameModule.DataModule.Image;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameModule.DataModule
{
	public class DialogueFlow
	{
		public Queue<KeyValuePair<ActorType, ActorInfo>> startFlow;
		public Queue<KeyValuePair<ActorType, ActorInfo>> positiveFlow;
		public Queue<KeyValuePair<ActorType, ActorInfo>> negativeFlow;
		public Queue<KeyValuePair<ActorType, ActorInfo>> endFlow;

		public uint currentFlow;
		public uint actorQuestNumber;
		
		public Dictionary<uint, UnityEngine.UIElements.Image> imageFlow;
		public DialogueFlow( Queue<KeyValuePair<ActorType, ActorInfo>>__endFlow,  Queue<KeyValuePair<ActorType, ActorInfo>> __negativeFlow, 
			Queue<KeyValuePair<ActorType, ActorInfo>> __positiveFlow,  Queue<KeyValuePair<ActorType, ActorInfo>> __startFlow, Dictionary<uint, UnityEngine.UIElements.Image> __imageFlow)
		{
			currentFlow = 0;
			
			endFlow = __endFlow;
			negativeFlow = __negativeFlow;
			positiveFlow = __positiveFlow;
			startFlow = __startFlow;
			imageFlow = __imageFlow;
		}

		public struct ActorInfo
		{
			public bool IsQuest;
			public UnityEngine.UIElements.Image Image;
		}
	}
}
