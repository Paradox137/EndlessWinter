using System;

namespace GameModule.BusinessLogicModule.PlayerUIActions
{
	public class MenuAction
	{
		public event Action<MenuLogicAction> Action;

		public void Rise(MenuLogicAction item)
		{
			Action?.Invoke(item);
		}
	}
}
