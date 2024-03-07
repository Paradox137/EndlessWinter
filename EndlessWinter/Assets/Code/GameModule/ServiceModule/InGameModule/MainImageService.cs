using UnityEngine;
using UnityEngine.UI;

namespace GameModule.ServiceModule.InGameModule
{
	public class MainImageService : IImageService
	{
		public void Output(Image __image, Sprite __sprite)
		{
			if(__image.enabled == false)
				__image.enabled = true;
			
			__image.sprite = __sprite;
		}
	}
}
