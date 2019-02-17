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

	public override void OnBeginScene()
	{
		mGameUIController = new GameUIController(FindObjectOfType<GameUIView>(), FindObjectOfType<GamePersistentData>());

		Debug.Log("Game Level Scene Handler call");
	}
}