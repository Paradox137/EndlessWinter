using Zenject;

namespace GameModule.ProviderModule.Sprite
{
	public class SpriteProvider : IItemProvider<UnityEngine.Sprite>
	{
		private UnityEngine.Sprite _sprite;
		
		public UnityEngine.Sprite Sprite
		{
			get => _sprite;
			set => _sprite = value;
		}

		[Inject]
		public SpriteProvider()
		{
            
		}
		
		public UnityEngine.Sprite GetItem()
		{
			return _sprite;
		}

	}
}
