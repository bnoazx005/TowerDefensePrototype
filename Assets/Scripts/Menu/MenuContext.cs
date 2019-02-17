using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// class MenuContext
///
/// The class is an implementation of a menu context that processes
/// all existing sub-menus in the game
/// </summary>

public class MenuContext: MonoBehaviour, IMenuContext
{
    public event UnityAction<bool> OnSoundOptionValueChanged;

    public event UnityAction       OnStartGame;

    public event UnityAction<bool> OnPauseGame;

    public event UnityAction       OnQuitToGameMap;

    public event UnityAction       OnRetryLevel;

    public BaseMenu mMainMenu;

    public BaseMenu mPauseMenu;

    public BaseMenu mGameOverMenu;

    protected IMenu mCurrActiveMenu;

    protected IMenu mCurrParentMenu;

    public virtual void Init()
    {
        mMainMenu?.Init(this);
        mPauseMenu?.Init(this);
        mGameOverMenu?.Init(this);
        
        Hide();

        mCurrActiveMenu = mMainMenu;
    }

    /// <summary>
    /// The method is used to set up current active menu
    /// </summary>
    /// <param name="menu"></param>

    public void SetMenu(IMenu menu, object arg = null)
    {
        if (menu == null || mCurrActiveMenu == null)
        {
            return;
        }

        //if the new one menu is a pause menu or a main menu we should set it as a root object
        if (menu == mPauseMenu || menu == mMainMenu)
        {
            SetParentMenu(menu);
        }

        mCurrActiveMenu.Hide();

        mCurrActiveMenu = menu;

        mCurrActiveMenu.Show(arg);
    }

    /// <summary>
    /// The method is used to specify a parent menu of a context
    /// </summary>
    /// <param name="menu">A reference to parent menu</param>

    public void SetParentMenu(IMenu menu)
    {
        mCurrParentMenu = menu;
    }
    
    public void Hide()
    {
        MainMenu?.Hide();
        mPauseMenu?.Hide();
        mGameOverMenu?.Hide();
    }

    public void OnStartGameButtonClicked()
    {
        mCurrActiveMenu.OnStartGameButtonClicked();
    }
    
    public void NotifyOnSoundOptionValueChanged(bool value)
    {
        OnSoundOptionValueChanged?.Invoke(value);
    }

    public void NotifyOnStartGame()
    {
        OnStartGame?.Invoke();
    }

    public void NotifyOnPauseGame(bool isPaused)
    {
        OnPauseGame?.Invoke(isPaused);
    }

    public void NotifyOnRetryLevel()
    {
        OnRetryLevel?.Invoke();
    }

    protected void Update()
    {
        mCurrActiveMenu?.OnInputEvent();
    }

    public IMenu MainMenu
    {
        get
        {
            return mMainMenu;
        }
    }

    public IMenu PauseMenu
    {
        get
        {
            return mPauseMenu;
        }
    }

    public IMenu GameOverScreenMenu
    {
        get
        {
            return mGameOverMenu;
        }
    }

    public IMenu ParentMenu
    {
        get
        {
            return mCurrParentMenu;
        }
    }
}
