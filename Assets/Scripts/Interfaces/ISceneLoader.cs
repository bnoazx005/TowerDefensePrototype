using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


/// <summary>
/// interface ISceneLoader
/// 
/// The interface describes a functionality of a scene loader
/// </summary>

public interface ISceneLoader
{
    event UnityAction OnLoaded;

    event UnityAction OnBeforeLoading;
    
    /// <summary>
    /// The method additively loads a single scene with a givel identifier
    /// </summary>
    /// <param name="sceneId">Scene's identifier</param>

    void LoadAsync(int sceneId);

    /// <summary>
    /// The method unloads all loaded scenes
    /// </summary>

    IEnumerator UnloadAllScenes();
}
