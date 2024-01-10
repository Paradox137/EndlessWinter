using System.Collections.Generic;

namespace GameModule.DataModule
{
	public class Actor
	{
		public ActorType actorName;
		
		public Queue<string> startReplicas;
		public Queue<string> positiveReplicas;
		public Queue<string> negativeReplicas;
		public Queue<string> endReplicas;

		public Actor(ActorType __actorName, Queue<string> __startReplicas, Queue<string> __positiveReplicas, Queue<string> __negativeReplicas, Queue<string> __endReplicas)
		{
			endReplicas = __endReplicas;
			negativeReplicas = __negativeReplicas;
			positiveReplicas = __positiveReplicas;
			startReplicas = __startReplicas;
			actorName = __actorName;
		}
	}
}
