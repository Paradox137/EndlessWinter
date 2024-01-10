namespace GameModule.StateMachineModule
{
	public enum NovelGameState
	{
		Startup,
		
		LoadMainMenu,
		LoadNewGame,
		LoadSavedNovel,
		
		MainMenu,
		
		InGame,
		EndGame,
	}
}
