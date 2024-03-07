using SharedModule.CollectionModule;
using UnityEngine;

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
		
		public virtual void LocalCachedShow()
		{
			_windowCanvas.enabled = true;
		}

		protected void Hide()
		{
			_windowCanvas.enabled = false;
            
			OnHide();
		}

		protected abstract void OnShow(object[] args);

		protected abstract void OnHide();
	}
}
