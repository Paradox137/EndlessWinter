using System;

namespace GameModule.BusinessLogicModule.PlayerUIActions
{
	public class InGameAction
	{
		public event Action<InGameLogicItem> Action;

		public void Rise(InGameLogicItem __item)
		{
			Action?.Invoke(__item);
		}
	}
}
