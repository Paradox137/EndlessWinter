using System;
using System.Collections.Generic;
using System.ComponentModel;
using GameModule.EntityModule;

namespace GameModule.PlayerModule
{
	[Serializable]
	public class PlayerData
	{
		/// <summary>
		/// chapter, dialogue, flowPosition
		/// </summary>
		private (ushort, ushort, ushort) _savePlace;
		
		private int _saveID;
		private bool _isGameStarted;
		private List<PerkEntity> _perkEntities;
		
		public bool IsGameExists { get => _isGameStarted; set => _isGameStarted = value; }
		public int SaveID { get => _saveID; set => _saveID = value; }
		
		[Description("chapter, dialogue, flowPosition")]
		public (ushort ChapterSavedPart, ushort DialogueSavedPart, ushort CurrentFlow) SavePlace { get => _savePlace; set => _savePlace = value; }
		public List<PerkEntity> PerkEntities { get => _perkEntities; set => _perkEntities = value; }
		
		public PlayerData(List<PerkEntity> __perkEntities)
		{
			_saveID = 0;
			_isGameStarted = false;
			_savePlace = (0, 0, 0);

			_perkEntities = __perkEntities;
		}
	}
}
