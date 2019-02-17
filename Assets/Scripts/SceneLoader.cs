using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
/// class SceneLoader
/// 
/// The class is a plain C# class that can load game scenes
/// </summary>

public class SceneLoader : ISceneLoader
{
    public event UnityAction OnLoaded;

    public event UnityAction OnBeforeLoading;

    protected MonoBehaviour      mCoroutineCtx;

    protected List<int>          mLoadedScenesList;

    protected int                mCurrActiveScene;

    protected ILoadingScreenView mLoadingScreenView;

    /// <summary>
    /// The main constructor of a class
    /// </summary>
    /// <param name="coroutineCtx">The given object won't be changed just is used to create coroutines</param>

    public SceneLoader(MonoBehaviour coroutineCtx, ILoadingScreenView loadingScreenView)
    {
        if (!coroutineCtx)
        {
            throw new ArgumentNullException("coroutineCtx");
        }

        mCoroutineCtx = coroutineCtx ?? throw new ArgumentNullException("loadingScreenView");

        mLoadingScreenView = loadingScreenView ?? throw new ArgumentNullException("loadingScreenView");

        mLoadingScreenView.IsEnabled = false;

        mLoadedScenesList = new List<int>();

        mLoadedScenesList.Add(SceneManager.GetActiveScene().buildIndex);

        mCurrActiveScene = -1;
    }

    /// <summary>
    /// The method additively loads a single scene with a givel identifier
    /// </summary>
    /// <param name="sceneId">Scene's identifier</param>

    public void LoadAsync(int sceneId)
    {
        if (sceneId < 0)
        {
            throw new ArgumentOutOfRangeException("sceneId");
        }

        OnBeforeLoading?.Invoke();

        mLoadingScreenView.Show(() =>
        {
            mCoroutineCtx.StartCoroutine(_loadSingleScene(sceneId));
        });
    }

    /// <summary>
    /// The method unloads all loaded scenes
    /// </summary>

    public IEnumerator UnloadAllScenes()
    {
        for (int i = 0; i < mLoadedScenesList.Count; ++i)
        {
            if (mCurrActiveScene == i)
            {
                yield return null;

                continue;
            }

            Debug.LogFormat("[Scene Loader] Unload scene {0}", mLoadedScenesList[i]);

            yield return SceneManager.UnloadSceneAsync(mLoadedScenesList[i]);
        }

        mCurrActiveScene = -1;

        mLoadedScenesList.Clear();
    }

    protected IEnumerator _loadSingleScene(int sceneId)
    {
        if (mLoadedScenesList.Contains(sceneId))
        {
            yield break;
        }

        mCurrActiveScene = -1;

        yield return mCoroutineCtx.StartCoroutine(UnloadAllScenes());

        yield return SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);

        mLoadedScenesList.Add(sceneId);

        mCurrActiveScene = sceneId;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneId));

        mLoadingScreenView.Hide(() => { });

        yield return null;
    }

    protected void _notifyOnSceneLoaded()
    {
        if (OnLoaded != null)
        {
            OnLoaded();
        }
    }
}