using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// abstract class BaseSceneHandler
///
/// The class is an utility one that is used to load Boot scene, which
/// containt GameController, Camera and other facility objects
/// </summary>

public abstract class BaseSceneHandler: MonoBehaviour
{
	public abstract void OnBeginScene();

	protected void Awake()
	{
		/// check whether BootSceneComponent exists or not
		BootSceneComponent bootSceneComponent = GameObject.FindObjectOfType<BootSceneComponent>();

		if (bootSceneComponent == null)
		{
			/// load Boot scene if it's not loaded yet
			SceneManager.LoadSceneAsync("Boot", LoadSceneMode.Additive);

			SceneManager.sceneLoaded += _onBootSceneLoaded;

			return;
		}

		OnBeginScene();
	}

	protected void _onBootSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == gameObject.scene.buildIndex)
		{
			return;
		}

		SceneManager.sceneLoaded -= _onBootSceneLoaded;

		OnBeginScene();
	}
}