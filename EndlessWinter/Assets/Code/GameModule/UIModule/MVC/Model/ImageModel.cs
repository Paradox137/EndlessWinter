using UniRx;
using UnityEngine;

namespace GameModule.UIModule.MVC.Model
{
	public class ImageModel
	{
		public ReactiveProperty<Sprite> Sprite { get; set; }

		public ImageModel()
		{
			Sprite = new ReactiveProperty<Sprite>();
		}
	}
}
