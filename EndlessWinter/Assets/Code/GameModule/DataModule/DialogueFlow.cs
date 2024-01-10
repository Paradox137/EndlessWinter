using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameModule.DataModule
{
	public class DialogueFlow
	{
		public Queue<KeyValuePair<ActorType, uint>> startFlow;
		public  Queue<KeyValuePair<ActorType, uint>> positiveFlow;
		public  Queue<KeyValuePair<ActorType, uint>> negativeFlow;
		public  Queue<KeyValuePair<ActorType, uint>> endFlow;

		public uint currentFlow;

		public DialogueFlow( Queue<KeyValuePair<ActorType, uint>>__endFlow,  Queue<KeyValuePair<ActorType, uint>> __negativeFlow, 
			Queue<KeyValuePair<ActorType, uint>> __positiveFlow,  Queue<KeyValuePair<ActorType, uint>> __startFlow)
		{
			currentFlow = 0;
			
			endFlow = __endFlow;
			negativeFlow = __negativeFlow;
			positiveFlow = __positiveFlow;
			startFlow = __startFlow;

			Queue<KeyValuePair<ActorType, uint>> r = new  Queue<KeyValuePair<ActorType, uint>>();
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Player, 2));
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Semen, 1));
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Veronika, 3));
			DialogueFlow d = new DialogueFlow(r, r, r, r);
		}
	}
}
