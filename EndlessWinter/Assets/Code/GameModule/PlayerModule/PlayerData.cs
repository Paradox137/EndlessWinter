using System.Collections.Generic;

namespace GameModule.PlayerModule
{
	public class PlayerData
	{
		private int _saveID;
		private bool _isGameStarted;
		private (ushort, ushort) _savePlace;
		private PlayerCharacteristics _playerCharacteristics;

		public PlayerCharacteristics Characteristics { get => _playerCharacteristics; set => _playerCharacteristics = value; }
		public bool IsGameStarted { get => _isGameStarted; set => _isGameStarted = value; }
		public int SaveID { get => _saveID; set => _saveID = value; }
		public (ushort, ushort) SavePlace { get => _savePlace; set => _savePlace = value; }

		public PlayerData()
		{
			_saveID = 0;
			_isGameStarted = false;
			_savePlace = (0, 0);
			
			_playerCharacteristics.Leadership = 50;
			_playerCharacteristics.Friendship = 50;
			_playerCharacteristics.Love = 50;
			_playerCharacteristics.Popularity = 50;
			_playerCharacteristics.Savvy = 50;
			_playerCharacteristics.Initiative = 50;
			_playerCharacteristics.SelfDiscipline = 50;
			_playerCharacteristics.Honesty = 50;
			_playerCharacteristics.Generosity = 50;
			_playerCharacteristics.Bravery = 50;
			_playerCharacteristics.Determination = 50;
		}
	}
}
