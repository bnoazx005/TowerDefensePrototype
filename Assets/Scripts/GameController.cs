using UnityEngine;


/// <summary>
/// class GameController
///
/// The class represents a game controller that contains all the
/// infrastructure that is needed for correct work of the game
/// </summary>

public class GameController: MonoBehaviour
{
	protected ISceneLoader mSceneLoader;

	protected IMenuContext mMenuContext;

	protected void Awake()
	{
		mSceneLoader = new SceneLoader(this, FindObjectOfType<LoadingScreenView>());

		mMenuContext = FindObjectOfType<MenuContext>();

		mMenuContext.OnStartGame += _onStartNewGame;
		mMenuContext.OnPauseGame += _onShowGamePause;
	}

	protected void OnDestroy()
	{
		mMenuContext.OnStartGame -= _onStartNewGame;
		mMenuContext.OnPauseGame -= _onShowGamePause;		
	}

	protected void _onStartNewGame()
	{
		mSceneLoader.LoadAsync(2);
	}

	protected void _onShowGamePause(bool isPaused)
	{
		Time.timeScale = isPaused ? 0.0f : 1.0f;
	}
}