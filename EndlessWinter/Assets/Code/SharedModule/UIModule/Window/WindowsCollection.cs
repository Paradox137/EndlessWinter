using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SharedModule.UIModule.Window
{
	public class WindowsCollection : IDisposable
	{
		[Inject]
		private static List<BaseWindow> _windows = new List<BaseWindow>();
		
		public static void Add<TWindow>(TWindow __window) where TWindow : BaseWindow
		{
			_windows.Add(__window);
		}

		public static BaseWindow Get<TWindow>() where TWindow : BaseWindow
		{
			Type window = typeof(TWindow);
			
			return _windows.Find((w => w.GetType() == window));
		}
		
		public void Dispose()
		{
			_windows = null;
		}
	}
}
