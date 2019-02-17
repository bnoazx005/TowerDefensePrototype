using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class PauseMenu
///
/// The class represents a logic of a pause menu
/// </summary>

public class PauseMenu: BaseMenu
{
    public Button  mResumeButton;

    public Button  mQuitButton;

    protected bool mIsShowing;

    public override void Init(IMenuContext menuContext)
    {
        base.Init(menuContext);

        mIsShowing = false;

        mResumeButton?.onClick.AddListener(_cancelPauseMenu);
        mQuitButton?.onClick.AddListener(_onQuitButtonClicked);
    }

    public override void Show(object arg = null)
    {
        base.Show(arg);

        mIsShowing = true;
    }

    public override void Hide()
    {
        base.Hide();

        mIsShowing = false;
    }

    public override void OnInputEvent()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }

        if (mIsShowing)
        {
#if DEBUG
            Debug.Log("[Pause Menu] Pause menu -> Exit");
#endif

            _cancelPauseMenu();

            return;
        }

#if DEBUG
        Debug.Log("[Pause Menu] Game -> Pause menu");
#endif
        
        Show();

        mMenuContext.NotifyOnPauseGame(mIsShowing);
    }

    protected void _cancelPauseMenu()
    {
        Hide();

        mMenuContext.NotifyOnPauseGame(mIsShowing);
    }

    protected void _onQuitButtonClicked()
    {
        Debug.Log("[Pause Menu] Game -> Main menu");

        _cancelPauseMenu();

//SET menu
    }

    protected void OnDestroy()
    {
        mResumeButton?.onClick.RemoveListener(_cancelPauseMenu);
        mQuitButton?.onClick.RemoveListener(_onQuitButtonClicked);
    }
}
