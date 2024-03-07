using System.Collections.Generic;

namespace GameModule.DataModule.Novel
{
	public class ChapterFlow
	{
		public List<DialogueFlow> Dialogues { get; set; }
		public ushort DialoguePart { get; set; }
		public ushort ChapterNumber { get; set; }
		public ChapterFlow(List<DialogueFlow> __dialogueFlows, ushort __currentDialoguePart, ushort __chapterNumber)
		{
			Dialogues = __dialogueFlows;
			
			DialoguePart = __currentDialoguePart;

			ChapterNumber = __chapterNumber;
		}
	}
}
