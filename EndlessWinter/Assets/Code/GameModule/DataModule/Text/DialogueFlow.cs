using System;
using System.Collections.Generic;
using GameModule.ConfigsModule;
using GameModule.DataModule.Image;
using GameModule.PlayerModule;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace GameModule.DataModule
{
	public class DialogueFlow
	{
		private Queue<KeyValuePair<ActorType, Sprite>> _startFlow;
		private Queue<KeyValuePair<ActorType, Sprite>> _positiveFlow;
		private Queue<KeyValuePair<ActorType, Sprite>> _negativeFlow;
		private Queue<KeyValuePair<ActorType, Sprite>> _endFlow;

		private uint _currentFlow;
		private uint _actorQuestNumber;
		
		public uint CurrentFlow => _currentFlow;
		
		private ChoiceInfluence _choiceInfluence;
		private Dictionary<uint, Texture> _imageFlow;
		public DialogueFlow(Queue<KeyValuePair<ActorType, Sprite>>__endFlow,  Queue<KeyValuePair<ActorType, Sprite>> __negativeFlow, 
			Queue<KeyValuePair<ActorType, Sprite>> __positiveFlow,  Queue<KeyValuePair<ActorType, Sprite>> __startFlow,
			Dictionary<uint, Texture> __imageFlow, uint __actorQuestNumber, ChoiceInfluence __choiceInfluence)
		{
			_currentFlow = 0;
			_actorQuestNumber = __actorQuestNumber;
			
			_endFlow = __endFlow;
			_negativeFlow = __negativeFlow;
			_positiveFlow = __positiveFlow;
			_startFlow = __startFlow;
			_imageFlow = __imageFlow;

			_choiceInfluence = __choiceInfluence;
		}
		
		public struct ChoiceInfluence
		{
			public List<(PerkType, int)> PositiveInfluence;
			public List<(PerkType, int)> NegativeInfluence;
		}
	}
}
