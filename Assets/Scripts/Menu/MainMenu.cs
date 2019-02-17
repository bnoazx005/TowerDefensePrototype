using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class MainMenu
///
/// The class represents a logic of a main menu
/// </summary>

public class MainMenu: BaseMenu
{
    public Button mStartGameButton;
    
    public override void Init(IMenuContext menuContext)
    {
        base.Init(menuContext);

        mStartGameButton?.onClick.AddListener(NotifyOnStartGameButtonClicked);
    }

    private void OnDestroy()
    {
        mStartGameButton?.onClick.RemoveListener(NotifyOnStartGameButtonClicked);
    }

    public override void NotifyOnStartGameButtonClicked()
    {
        Debug.Log("[Main Menu] Main menu -> Game");

        mMenuContext.SetMenu(mMenuContext.PauseMenu);
        mMenuContext.Hide();

        mMenuContext.NotifyOnStartGame();
    }
}