using System;
using System.Collections.Generic;
using GameModule.EntityModule;
using GameModule.ExtensionsModule;
using UnityEngine;

namespace GameModule.PlayerModule
{
	[Serializable]
	public class PlayerData
	{
		private int _saveID;
		private bool _isGameStarted;
		private (ushort, ushort) _savePlace;
		private List<PerkEntity> _perkEntities;
		
		public bool IsGameStarted { get => _isGameStarted; set => _isGameStarted = value; }
		public int SaveID { get => _saveID; set => _saveID = value; }
		public (ushort, ushort) SavePlace { get => _savePlace; set => _savePlace = value; }
		public List<PerkEntity> PerkEntities { get => _perkEntities; set => _perkEntities = value; }

		public PlayerData(List<PerkEntity> __perkEntities)
		{
			Debug.Log("PLAYER DATA CONSTRUCTOR");
			_saveID = 0;
			_isGameStarted = false;
			_savePlace = (0, 0);

			_perkEntities = __perkEntities;
		}
	}
}
