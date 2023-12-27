namespace GameModule.StateMachineModule
{
	public enum NovelGameState
	{
		Startup,
		
		LoadMainMenu,
		LoadNewNovel,
		LoadSavedNovel,
		
		MainMenu,
		
		StartGame,
		EndGame,
	}
}
