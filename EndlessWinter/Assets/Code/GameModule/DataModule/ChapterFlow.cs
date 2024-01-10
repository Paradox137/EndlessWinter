using System.Collections.Generic;

namespace GameModule.DataModule
{
	public class ChapterFlow
	{
		public List<DialogueFlow> _dialogueFlows;
		
		public ChapterFlow(List<DialogueFlow> __dialogueFlows)
		{
			_dialogueFlows = __dialogueFlows;
			
			Queue<KeyValuePair<ActorType, uint>> r = new  Queue<KeyValuePair<ActorType, uint>>();
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Player, 2));
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Semen, 1));
			r.Enqueue(new KeyValuePair<ActorType, uint>(ActorType.Veronika, 3));
			DialogueFlow d1 = new DialogueFlow(r, r, r, r);
			DialogueFlow d2 = new DialogueFlow(r, r, r, r);
			DialogueFlow d3 = new DialogueFlow(r, r, r, r);


			ChapterFlow chapterFlow = new ChapterFlow(new List<DialogueFlow>()
			{
				d1,
				d2,
				d3
			});
		}
	}
}
