using System;
using GameModule.DataModule.Novel;
using UnityEngine;

namespace GameModule.ConfigsModule
{
	[CreateAssetMenu(fileName = "TextViewConfig", menuName = "MyAssets/Game/Settings/TextViewConfig")]
	public class TextViewConfig : ScriptableObject
	{
		public ActorsTexts ActorsTexts;
	}
	[Serializable] public struct ActorsTexts { public Color MainTextColor; public NameTextItem[] NamesItems; }
	[Serializable] public struct NameTextItem { public ActorType ActorType; public Color NameColor; }
}
