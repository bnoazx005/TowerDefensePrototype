using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// class MainMenuSceneHandler
///
/// The class is a scene handler that should be placed at a scene which shows the main menu 
/// </summary>

public class MainMenuSceneHandler: BaseSceneHandler
{
	protected override void _onBootSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		IMenuContext menuContext = FindObjectOfType<MenuContext>();

		menuContext.Init();
		menuContext.SetMenu(menuContext.MainMenu);
	}
}