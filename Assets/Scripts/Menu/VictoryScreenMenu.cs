using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class VictoryScreenMenu
/// 
/// The class represents a victory screen
/// </summary>

public class VictoryScreenMenu : BaseMenu
{
    public Button mRetryButton;

    public Button mNextButton;
    
    public override void Init(IMenuContext menuContext)
    {
        base.Init(menuContext);
        
        mRetryButton?.onClick.AddListener(_onRetryButtonClicked);
        mNextButton?.onClick.AddListener(_onNextButtonClicked);
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

    protected void _onNextButtonClicked()
    {
        Debug.Log("[Game Over Screen Menu] Game -> Pause");

        _onRetryButtonClicked();

       // mMenuContext.NotifyOnNextToGameMap();
    }

    protected void OnDestroy()
    {
        mRetryButton?.onClick.RemoveListener(_onRetryButtonClicked);
        mNextButton?.onClick.RemoveListener(_onNextButtonClicked);
    }
}
