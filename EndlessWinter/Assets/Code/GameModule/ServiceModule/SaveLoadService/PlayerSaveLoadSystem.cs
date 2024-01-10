using System;
using System.Collections.Generic;
using System.Linq;
using GameModule.EntityModule;
using GameModule.SettingsModule;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;

namespace GameModule.PlayerModule
{
	// usages name Conversions:
	// public PlayerData PlayerData => _playerData; - if need outside change parametrs data
	// 	public PlayerData GetPlayerData(); - if need just get data
	public class PlayerSaveLoadSystem : IInitializable
	{
		private readonly List<PerkEntity> _entities;
		//private DateTime _lastSaveTime;
		
		private readonly PlayerData _playerData;
		private const string KEY_SAVE = "save8";
		
		public PlayerData PlayerData => _playerData;

		[Inject]
		public PlayerSaveLoadSystem(List<PerkEntity> __perkEntities)
		{
			_entities = __perkEntities;
			
			_playerData = Load();
		}

		public void Initialize()
		{
			Debug.Log("PlayerData Loaded");
			
			Debug.Log(_playerData.IsGameStarted);
			Debug.Log(_playerData.SaveID);
		}
		
		private PlayerData Load()
		{
			string localStringData = PlayerPrefs.GetString(KEY_SAVE);
			
			return string.IsNullOrEmpty(localStringData) ? new PlayerData(_entities) : JsonConvert.DeserializeObject<PlayerData>(localStringData);
		}

		public void Save()
		{
			_playerData.SaveID++;
			
			string stringData = JsonConvert.SerializeObject(_playerData);
			
			PlayerPrefs.SetString(KEY_SAVE, stringData);
			
			Debug.Log(_playerData.IsGameStarted);
			Debug.Log(_playerData.SaveID);

		}

		public PlayerData GetPlayerData()
		{
			return _playerData;
		}
	}
}
