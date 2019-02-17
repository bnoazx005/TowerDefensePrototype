using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// class MainMenuSceneHandler
///
/// The class is a scene handler that should be placed at a scene which shows the main menu 
/// </summary>

public class MainMenuSceneHandler: BaseSceneHandler
{
	protected ISceneLoader mSceneLoader;

	public override void OnBeginScene()
	{
		IMenuContext menuContext = FindObjectOfType<MenuContext>();

		menuContext.Init();
		menuContext.SetMenu(menuContext.MainMenu);
	}
}