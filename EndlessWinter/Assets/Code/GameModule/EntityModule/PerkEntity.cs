using System;
using GameModule.DataModule;
using UnityEngine;

namespace GameModule.EntityModule
{
	[Serializable]
	public struct PerkEntity
	{
		public PerkType Type;
		
		[Range(0, 100)]
		public int Value;
	}
}
