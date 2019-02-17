using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// interface IMenuContext
///
/// The interface describes a functionality of a menu's context
/// </summary>

public interface IMenuContext
{
	event UnityAction<bool> OnSoundOptionValueChanged;

	event UnityAction OnStartGame;

	event UnityAction<bool> OnPauseGame;

	event UnityAction OnRetryLevel;

	/// <summary>
	/// The method initializes an internal state of a menu context
	/// </summary>

	void Init();

	/// <summary>
	/// The method is used to set up current active menu
	/// </summary>
	/// <param name="menu"></param>
	/// <param name="arg">An additional information that can be passed into a menu</param>

	void SetMenu(IMenu menu, object arg = null);

	/// <summary>
	/// The method is used to specify a parent menu of a context
	/// </summary>
	/// <param name="menu">A reference to parent menu</param>

	void SetParentMenu(IMenu menu);

	void Hide();

	void OnStartGameButtonClicked();

	void NotifyOnSoundOptionValueChanged(bool value);

	void NotifyOnStartGame();

	void NotifyOnPauseGame(bool isPaused);

	void NotifyOnRetryLevel();

	IMenu MainMenu { get; }

	IMenu PauseMenu { get; }

	IMenu GameOverScreenMenu { get; }

	IMenu VictoryScreenMenu { get; }

	IMenu ParentMenu { get; }
}


/// <summary>
/// interface IMenu
///
/// The interface describes a functionality of a single menu's page
/// </summary>

public interface IMenu
{
	void Init(IMenuContext menuContext);

	void Show(object arg = null);

	void Hide();

	void OnStartGameButtonClicked();

	void OnInputEvent();

	IMenuContext MenuInstance { set; }

	string Name { get; }

	IMenu ParentMenu { get; set; }
}