using UnityEngine.Events;


/// <summary>
/// interface ILoadingScreenView
///
/// The interface describes a functionality of a loading screen's view
/// </summary>

public interface ILoadingScreenView
{
    void Show(UnityAction postActionCallback);

    void Hide(UnityAction postActionCallback);

    float FadeCoefficient { get; set; }

    bool IsEnabled { set; }
}
