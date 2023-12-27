using System.Collections.Generic;
using System.Linq;
using GameModule.PlayerModule;
using UnityEngine.PlayerLoop;

namespace GameModule.ExtensionsModule
{
	public static class DataExtensions
	{
		private static Dictionary<PerkType, string> PlayerPerks = new Dictionary<PerkType, string>()
		{		
			{ PerkType.Leadership,     "Лидерство"},
			{ PerkType.Friendship,     "Дружба"},
			{ PerkType.Love,           "Любовь"},
			{ PerkType.Popularity,     "Популярность"},
			{ PerkType.Savvy,          "Cмекалка"},
			{ PerkType.Initiative,     "Инициатива"},
			{ PerkType.SelfDiscipline, "Самодисциплина"},
			{ PerkType.Honesty,        "Честность"},
			{ PerkType.Generosity,     "Щедрость"},
			{ PerkType.Bravery,        "Xрабрость"},
			{ PerkType.Determination,  "Определение"},
			{ PerkType.XXX,            "XXX"},
		};

		public static string GetPerkDescription(this PerkType __characteristic)
		{
			return PlayerPerks[__characteristic];
		}
		public static PerkType GetPerkType(this string __perkName)
		{
			return PlayerPerks.FirstOrDefault(n => n.Value == __perkName).Key;
		}
	}
}