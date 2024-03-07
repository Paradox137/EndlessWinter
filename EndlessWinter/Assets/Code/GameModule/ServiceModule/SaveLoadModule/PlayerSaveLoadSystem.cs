using System.Collections.Generic;
using GameModule.EntityModule;
using GameModule.PlayerModule;
using Newtonsoft.Json;
using SharedModule.CustomizeModule;
using UnityEngine;
using Zenject;

namespace GameModule.ServiceModule.SaveLoadModule
{
	// usages name Conversions:
	// public PlayerData PlayerData => _playerData; - if need outside change parametrs data
	// 	public PlayerData GetPlayerData(); - if need just get data
	public class PlayerSaveLoadSystem : IInitializable
	{
		private readonly List<PerkEntity> _entities;
		//private DateTime _lastSaveTime;
		
		
		private readonly PlayerData _playerData;
		private const string KEY_SAVE = "DATA_SAVE";
		
		//public PlayerData PlayerData => _playerData;

		[Inject]
		public PlayerSaveLoadSystem(List<PerkEntity> __perkEntities)
		{
			_entities = __perkEntities;
			
			_playerData = Load();
		}

		public void Initialize()
		{
			CustomDebug.WriteLine("PlayerData", $"Initialized with SaveID : {_playerData.SaveID}", CustomDebugColors.Purple);
		}
		
		private PlayerData Load()
		{
			string localStringData = PlayerPrefs.GetString(KEY_SAVE);
			
			return string.IsNullOrEmpty(localStringData) ? new PlayerData(_entities) : JsonConvert.DeserializeObject<PlayerData>(localStringData);
		}
		
		public void ResetData()
		{
			_playerData.SaveID = 0;
			_playerData.SavePlace = (0, 0, 0);
			_playerData.PerkEntities = _entities;
		}
		
		public void Save()
		{
			_playerData.SaveID++;
			
			string stringData = JsonConvert.SerializeObject(_playerData);
			
			PlayerPrefs.SetString(KEY_SAVE, stringData);
		}

		public PlayerData GetPlayerData()
		{
			return _playerData;
		}
	}
}
