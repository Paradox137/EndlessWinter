using System.Collections.Generic;
using GameModule.EntityModule;
using GameModule.PlayerModule;
using UnityEngine;
using Zenject;

namespace GameModule.SettingsModule
{
	[CreateAssetMenu(fileName = "PlayerSettings", menuName = "MyAssets/Game/Settings/PlayerSettings")]
	public class PlayerDataSettings : ScriptableObject	
	{
		public List<PerkEntity> PerkEntities;
	}
}

