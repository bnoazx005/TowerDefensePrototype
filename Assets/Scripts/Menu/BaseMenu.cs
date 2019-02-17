using System;
using UnityEngine;


/// <summary>
/// abstract class BaseMenu
/// 
/// The class is a base class for all submenus in the game
/// </summary>

public class BaseMenu: MonoBehaviour, IMenu
{
    protected IMenuContext mMenuContext;

    protected IMenu        mParentMenu;

    protected Transform    mCachedTransform;
    
    public virtual void Init(IMenuContext menuContext)
    {
        mMenuContext = menuContext ?? throw new ArgumentNullException("menuContext");

        mCachedTransform = GetComponent<Transform>();
    }

    public virtual void Show(object arg = null)
    {
        if (mCachedTransform == null)
        {
            return;
        }

        mCachedTransform.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        if (mCachedTransform == null)
        {
            return;
        }

        mCachedTransform.gameObject.SetActive(false);
    }

    public virtual void OnStartGameButtonClicked() {}

    public virtual void OnInputEvent()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogFormat("[Base Menu] Go to {0} menu", mMenuContext.ParentMenu.Name);

            mMenuContext.SetMenu(mMenuContext.ParentMenu);
        }
    }
    
    public IMenuContext MenuInstance
    {
        set
        {
            mMenuContext = value;
        }
    }

    public string Name
    {
        get
        {
            return mCachedTransform.name;
        }
    }

    public IMenu ParentMenu
    {
        get
        {
            return mParentMenu;
        }

        set
        {
            mParentMenu = value;
        }
    }
}