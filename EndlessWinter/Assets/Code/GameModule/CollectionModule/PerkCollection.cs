using System.Collections.Generic;
using System.Linq;
using GameModule.EntityModule;
using GameModule.ExtensionsModule;
using GameModule.PlayerModule;
using UnityEngine;
using Zenject;

namespace GameModule.CollectionModule
{
	public class PerkCollection
	{
		private readonly List<PerkEntity> _perkEntities;
		
		[Inject]
		public PerkCollection(List<PerkEntity> __perkEntities)
		{
			_perkEntities = __perkEntities;
			
			Debug.Log("Collection Inited");
		}
		
		public string GetPerkName(PerkType __characteristic)
		{
			return __characteristic.GetPerkDescription();
		}
		
		public int GetPerkValue(PerkType __characteristic)
		{
			return _perkEntities.FirstOrDefault(p => p.Type == __characteristic).Value;
		}
	}
}
