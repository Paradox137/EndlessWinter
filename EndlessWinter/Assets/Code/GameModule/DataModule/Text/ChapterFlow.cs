using System.Collections.Generic;
using GameModule.DataModule.Image;

namespace GameModule.DataModule
{
	public class ChapterFlow
	{
		public List<DialogueFlow> _dialogueFlows;
		
		public ChapterFlow(List<DialogueFlow> __dialogueFlows)
		{
			_dialogueFlows = __dialogueFlows;
		}
	}
}
