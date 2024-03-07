using System;
using System.Collections.Generic;
using SharedModule.UIModule.Window;
using Zenject;

namespace SharedModule.CollectionModule
{
	public class WindowsCollection : IDisposable
	{
		private static List<BaseWindow> _windows;
		
		[Inject]
		public void Construct()
		{
			_windows = new List<BaseWindow>();
		}
		
		public static void Add<TWindow>(TWindow __window) where TWindow : BaseWindow
		{
			//_windows ??= new List<BaseWindow>();
			
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
