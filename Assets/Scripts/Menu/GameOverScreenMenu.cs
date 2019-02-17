using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class GameOverScreenMenu
/// 
/// The class represents a game over screen
/// </summary>

public class GameOverScreenMenu : BaseMenu
{
    public Button mRetryButton;

    public Button mQuitButton;
    
    public override void Init(IMenuContext menuContext)
    {
        base.Init(menuContext);
        
        mRetryButton?.onClick.AddListener(_onRetryButtonClicked);
        mQuitButton?.onClick.AddListener(_onQuitButtonClicked);
    }

    public override void OnInputEvent()
    {
    }

    protected void _onRetryButtonClicked()
    {
        mMenuContext.SetMenu(mMenuContext.PauseMenu);

        Hide();

        mMenuContext.NotifyOnRetryLevel();
    }

    protected void _onQuitButtonClicked()
    {
        Debug.Log("[Game Over Screen Menu] Game -> World map");

        _onRetryButtonClicked();

       // mMenuContext.NotifyOnQuitToGameMap();
    }

    protected void OnDestroy()
    {
        mRetryButton?.onClick.RemoveListener(_onRetryButtonClicked);
        mQuitButton?.onClick.RemoveListener(_onQuitButtonClicked);
    }
}
