using System;
using System.Collections.Generic;
using GameModule.UIModule.Window;
using UnityEngine;
using Zenject;

namespace SharedModule.UIModule.Window
{
	public interface IWindow
	{
		void Show(params object[] args);
		void Hide();
		void OnShow(object[] args);
		void OnHide();
	}
	public abstract class BaseWindow : MonoBehaviour
	{
		protected Canvas _windowCanvas;
		
		protected virtual void Awake()
		{
			_windowCanvas = gameObject.GetComponent<Canvas>();
            
			_windowCanvas.enabled = false;
			
			WindowsCollection.Add(this);
		}
		
		public void Show(params object[] args)
		{
			OnShow(args);
            
			_windowCanvas.enabled = true;
		}
		
		public void Show()
		{
			_windowCanvas.enabled = true;
		}
		
		public void Hide()
		{
			_windowCanvas.enabled = false;
            
			OnHide();
		}

		public abstract void OnShow(object[] args);
		public abstract void OnHide();
	}
}
