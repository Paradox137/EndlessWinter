using System;
using System.Collections.Generic;
using GameModule.ConfigsModule;
using GameModule.DataModule.Image;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace GameModule.DataModule
{
	public class DialogueFlow
	{
		private Queue<KeyValuePair<ActorType, ActorInfo>> _startFlow;
		private Queue<KeyValuePair<ActorType, ActorInfo>> _positiveFlow;
		private Queue<KeyValuePair<ActorType, ActorInfo>> _negativeFlow;
		private Queue<KeyValuePair<ActorType, ActorInfo>> _endFlow;

		private uint _currentFlow;
		private uint _actorQuestNumber;
		
		public uint CurrentFlow => _currentFlow;
		
		private Dictionary<uint, Texture> _imageFlow;
		public DialogueFlow(Queue<KeyValuePair<ActorType, ActorInfo>>__endFlow,  Queue<KeyValuePair<ActorType, ActorInfo>> __negativeFlow, 
			Queue<KeyValuePair<ActorType, ActorInfo>> __positiveFlow,  Queue<KeyValuePair<ActorType, ActorInfo>> __startFlow,
			Dictionary<uint, Texture> __imageFlow, uint __actorQuestNumber)
		{
			_currentFlow = 0;
			_actorQuestNumber = __actorQuestNumber;
			
			_endFlow = __endFlow;
			_negativeFlow = __negativeFlow;
			_positiveFlow = __positiveFlow;
			_startFlow = __startFlow;
			_imageFlow = __imageFlow;
		}
	}
}
