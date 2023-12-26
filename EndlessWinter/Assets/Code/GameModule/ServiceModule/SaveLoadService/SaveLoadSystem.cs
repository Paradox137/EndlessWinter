using System;
using UnityEngine;
using Zenject;

namespace GameModule.PlayerModule
{
	public class SaveLoadSystem : IInitializable
	{
		//private DateTime _lastSaveTime;
		
		private PlayerData playerData;
		private const string KEY_SAVE = "save";

		public PlayerData PlayerData
		{
			get => playerData;
			set => playerData = value;
		}

		public void Initialize()
		{
			playerData = Load<PlayerData>();
			Debug.Log(playerData.SavePlace);
		}
		
		private T Load<T>() where T : PlayerData, new()
		{
			string localStringData = PlayerPrefs.GetString(KEY_SAVE);
			
			return string.IsNullOrEmpty(localStringData) ? new() : JsonUtility.FromJson<T>(localStringData);
		}

		public void Save<T>(T data) where T : PlayerData
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
