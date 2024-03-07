using UnityEngine;

namespace SharedModule.CustomizeModule
{
	public static class CustomDebug
	{
		private const string Red = "red";
		private const string Magenta = "magenta";
		private const string Green = "green";
		private const string Yellow = "yellow";
		private const string Purple = "purple";
		private const string Black = "black";
		private const string White = "white";
		private const string Cyan = "cyan";

		public static void WriteLine(object __reason, object __message, CustomDebugColors __color)
		{
			Debug.Log(string.Format("<color={2}> {0}: </color> <color=white> {1} </color> ", __reason, __message, __color.ToStringColor()));
		}
		
		public static void WriteLineWarning(object __reason, object __message, CustomDebugColors __color)
		{
			Debug.Log(string.Format("<color=yellow> Warning in Module: </color><color={2}> {0}: </color> <color=white> {1} </color> ", __reason, __message, __color.ToStringColor()));
		}
		private static string ToStringColor(this CustomDebugColors __color)
		{
			return __color switch
			{
				CustomDebugColors.Red => Red,
				CustomDebugColors.Black => Black,
				CustomDebugColors.Magenta => Magenta,
				CustomDebugColors.Yellow => Yellow,
				CustomDebugColors.Green => Green,
				CustomDebugColors.White => White,
				CustomDebugColors.Purple => Purple,
				CustomDebugColors.Cyan => Cyan,
				_ => White
			};
		}
	}
	
	public enum CustomDebugColors
	{
		Red,
		Magenta,
		Green,
		Yellow,
		Purple,
		Black,
		White,
		Cyan
	}
}
