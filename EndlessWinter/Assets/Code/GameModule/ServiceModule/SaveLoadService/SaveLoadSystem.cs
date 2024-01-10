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
	public class SaveLoadSystem : IInitializable
	{
		private readonly List<PerkEntity> _entities;
		//private DateTime _lastSaveTime;
		
		private PlayerData playerData;
		private const string KEY_SAVE = "save6";
		

		[Inject]
		public SaveLoadSystem(List<PerkEntity> __perkEntities)
		{
			_entities = __perkEntities;
			
			playerData = Load();
		}
		
		public void Initialize()
		{
			Debug.Log("PlayerData Loaded");
			
			Debug.Log(playerData.IsGameStarted);
			Debug.Log(playerData.SaveID);
		}
		
		private PlayerData Load()
		{
			string localStringData = PlayerPrefs.GetString(KEY_SAVE);
			
			return string.IsNullOrEmpty(localStringData) ? new PlayerData(_entities) : JsonConvert.DeserializeObject<PlayerData>(localStringData);
		}

		public void Save(PlayerData __playerData)
		{
			__playerData.SaveID++;
			
			string stringData = JsonConvert.SerializeObject(__playerData);
			
			PlayerPrefs.SetString(KEY_SAVE, stringData);
			
			Debug.Log(playerData.IsGameStarted);
			Debug.Log(playerData.SaveID);

		}

		public PlayerData GetPlayerData()
		{
			return playerData;
		}
	}
}
