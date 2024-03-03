using System.Collections.Generic;
using GameModule.EntityModule;
using UnityEngine;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "MyAssets/Game/Settings/PlayerConfig")]
	public class PlayerDataConfig : ScriptableObject	
	{
		public List<PerkEntity> PerkEntities;
	}
}

