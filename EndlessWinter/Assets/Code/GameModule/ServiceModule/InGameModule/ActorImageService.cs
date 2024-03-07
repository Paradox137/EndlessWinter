using UnityEngine;
using UnityEngine.UI;

namespace GameModule.ServiceModule.InGameModule
{
	public class ActorImageService : IImageService
	{
		public void Output(Image __image, Sprite __sprite)
		{
			if (__sprite == null)
				__image.enabled = false;
			else
			{
				__image.sprite = __sprite;
				__image.enabled = true;
			}
		}
	}
}
