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
	public void Initialize()
	{
		/// check whether BootSceneComponent exists or not
		BootSceneComponent bootSceneComponent = GameObject.FindObjectOfType<BootSceneComponent>();

		if (bootSceneComponent == null)
		{
			/// load Boot scene if it's not loaded yet
			SceneManager.sceneLoaded += _onBootSceneLoaded;

			SceneManager.LoadSceneAsync("Boot", LoadSceneMode.Additive);
		}
	}

	protected abstract void _onBootSceneLoaded(Scene scene, LoadSceneMode mode);
}