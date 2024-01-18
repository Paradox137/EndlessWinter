using System.Collections.Generic;
using GameModule.DataModule.Image;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameModule.DataModule
{
	public class DialogueFlow
	{
		public Queue<KeyValuePair<ActorType, ActorImage>> startFlow;
		public Queue<KeyValuePair<ActorType, ActorImage>> positiveFlow;
		public Queue<KeyValuePair<ActorType, ActorImage>> negativeFlow;
		public Queue<KeyValuePair<ActorType, ActorImage>> endFlow;

		public uint currentFlow;
		
		public Dictionary<uint, MainImage> imageFlow;
		public DialogueFlow( Queue<KeyValuePair<ActorType, ActorImage>>__endFlow,  Queue<KeyValuePair<ActorType, ActorImage>> __negativeFlow, 
			Queue<KeyValuePair<ActorType, ActorImage>> __positiveFlow,  Queue<KeyValuePair<ActorType, ActorImage>> __startFlow, Dictionary<uint, MainImage> __imageFlow)
		{
			currentFlow = 0;
			
			endFlow = __endFlow;
			negativeFlow = __negativeFlow;
			positiveFlow = __positiveFlow;
			startFlow = __startFlow;
			imageFlow = __imageFlow;
		}
	}
}
