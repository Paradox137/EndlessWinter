using System;
using System.Collections.Generic;
using System.Linq;
using GameModule.EntityModule;
using GameModule.SettingsModule;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameModule.PlayerModule
{
	public class SaveLoadSystem : IInitializable
	{
		private readonly List<PerkEntity> _entities;
		//private DateTime _lastSaveTime;
		
		private PlayerData playerData;
		private const string KEY_SAVE = "save";

		public PlayerData PlayerData
		{
			get => playerData;
			set => playerData = value;
		}

		[Inject]
		public SaveLoadSystem(List<PerkEntity> __perkEntities)
		{
			_entities = __perkEntities;
			
			PlayerData = Load();
		}
		
		public void Initialize()
		{
			Debug.Log("PlayerData Loaded");
		}
		
		private PlayerData Load()
		{
			string localStringData = PlayerPrefs.GetString(KEY_SAVE);
			
			return string.IsNullOrEmpty(localStringData) ? new PlayerData(_entities) : JsonUtility.FromJson<PlayerData>(localStringData);
		}

		public void Save(PlayerData data)
		{
			string stringData = JsonUtility.ToJson(data);
			
			PlayerPrefs.SetString(KEY_SAVE, stringData);
		}

		public PlayerData GetPlayerData()
		{
			return playerData;
		}
	}
}
