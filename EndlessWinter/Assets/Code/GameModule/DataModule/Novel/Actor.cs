using System.Collections.Generic;
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace GameModule.DataModule.Novel
{
	public class Actor
	{
		public ActorType ActorName;
		public Queue<string> StartReplicas;
		public Queue<string> PositiveReplicas;
		public Queue<string> NegativeReplicas;
		public Queue<string> EndReplicas;
		
		public Actor(ActorType __actorName, Queue<string> __startReplicas, Queue<string> __positiveReplicas, Queue<string> __negativeReplicas, Queue<string> __endReplicas)
		{
			EndReplicas = __endReplicas;
			NegativeReplicas = __negativeReplicas;
			PositiveReplicas = __positiveReplicas;
			StartReplicas = __startReplicas;
			ActorName = __actorName;
		}
	}
}
