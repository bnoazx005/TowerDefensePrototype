using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// class LoadingScreenView
/// 
/// The class is a loading screen's view which is used during scenes loading
/// </summary>

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreenView : MonoBehaviour, ILoadingScreenView
{
    public float          mDuration;

    protected CanvasGroup mCanvasGroup;

    public void Show(UnityAction postActionCallback)
    {
        CanvasGroup.alpha = 0.0f;

        gameObject.SetActive(true);
        
        StartCoroutine(AnimationUtils.Animate(mDuration, (time) =>
        {
            CanvasGroup.alpha = time / mDuration;
        }, postActionCallback));
    }

    public void Hide(UnityAction postActionCallback)
    {
        CanvasGroup.alpha = 1.0f;

        gameObject.SetActive(true);
        
        StartCoroutine(AnimationUtils.Animate(mDuration, (time) =>
        {
            CanvasGroup.alpha = 1.0f - time / mDuration;
        }, 
        () =>
        {
            postActionCallback?.Invoke();

            gameObject.SetActive(false);
        }));
    }
    
    public float FadeCoefficient { get => CanvasGroup.alpha; set => CanvasGroup.alpha = value; }

    public bool IsEnabled { set => gameObject?.SetActive(value); }

    public CanvasGroup CanvasGroup 
    {
        get
        {
            /// caching the component in the script
            if (mCanvasGroup == null)
            {
                mCanvasGroup = GetComponent<CanvasGroup>();
            }

            return mCanvasGroup;
        }
    }
}
