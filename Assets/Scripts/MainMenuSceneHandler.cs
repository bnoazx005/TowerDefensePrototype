using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// class MainMenuSceneHandler
///
/// The class is a scene handler that should be placed at a scene which shows the main menu 
/// </summary>

public class MainMenuSceneHandler: BaseSceneHandler
{
	public override void OnBeginScene()
	{
		IMenuContext menuContext = FindObjectOfType<MenuContext>();
		
		menuContext.SetMenu(menuContext.MainMenu);
	}
}