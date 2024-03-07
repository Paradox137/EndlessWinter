using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameModule.DataModule.Novel
{
	public class DialogueFlow
	{
		public ushort CurrentFlow { get; set; }
		public Queue<KeyValuePair<ActorType, Sprite>> StartFlow { get; }
		public Queue<KeyValuePair<ActorType, Sprite>> PositiveFlow { get; }
		public Queue<KeyValuePair<ActorType, Sprite>> NegativeFlow { get; }
		public Queue<KeyValuePair<ActorType, Sprite>> EndFlow { get;  }
		public uint ActorQuestNumber { get;  }
		public ChoiceInfluence GamerChoice { get;  }
		public Dictionary<uint, Sprite> ImageFlow { get;  }
		public List<Actor> Actors { get;  }
		public uint MaxFlow { get; }
		public DialogueFlow(Queue<KeyValuePair<ActorType, Sprite>> __endFlow, Queue<KeyValuePair<ActorType, Sprite>> __negativeFlow,
			Queue<KeyValuePair<ActorType, Sprite>> __positiveFlow, Queue<KeyValuePair<ActorType, Sprite>> __startFlow,
			Dictionary<uint, Sprite> __imageFlow, uint __actorQuestNumber, ChoiceInfluence __choiceInfluence, Actor[] __actors, ushort __currentFlow)
		{
			CurrentFlow = __currentFlow;
			ActorQuestNumber = __actorQuestNumber;

			EndFlow = __endFlow;
			NegativeFlow = __negativeFlow;
			PositiveFlow = __positiveFlow;
			StartFlow = __startFlow;
			ImageFlow = __imageFlow;

			GamerChoice = __choiceInfluence;
			Actors = __actors.ToList();
			
			MaxFlow = (uint) (StartFlow.Count + EndFlow.Count);
		}

		public struct ChoiceInfluence
		{
			public string PositiveChoiceText;
			public string NegativeChoiceText;
			public List<(PerkType, int)> PositiveInfluence;
			public List<(PerkType, int)> NegativeInfluence;
		}

	}
}
