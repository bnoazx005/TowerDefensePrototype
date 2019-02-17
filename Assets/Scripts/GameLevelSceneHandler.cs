using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// class GameLevelSceneHandler
///
/// The class is a scene handler that should be placed at all game scenes
/// </summary>

public class GameLevelSceneHandler: BaseSceneHandler
{
	protected GameUIController mGameUIController;

	protected override void _onBootSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		mGameUIController = new GameUIController(FindObjectOfType<GameUIView>(), FindObjectOfType<GamePersistentData>());
	}
}