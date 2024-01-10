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

		public Actor(Queue<string> __endReplicas, Queue<string> __negativeReplicas, Queue<string> __positiveReplicas, Queue<string> __startReplicas, ActorType __actorName)
		{
			endReplicas = __endReplicas;
			negativeReplicas = __negativeReplicas;
			positiveReplicas = __positiveReplicas;
			startReplicas = __startReplicas;
			actorName = __actorName;

			Queue<string> y = new Queue<string>();
			y.Enqueue("Tom");
			y.Enqueue("Bob");
			Actor a = new Actor(y, y, y, y, ActorType.Player);
		}
	}
}
