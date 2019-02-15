using System;
using UnityEngine;


/// <summary>
/// class GameUIController
///
/// The class provides a logic for in-game UI and updates it
/// using available data
/// </summary>

public class GameUIController: MonoBehaviour
{
	protected GameUIView         mView;

	protected GamePersistentData mPersistentData;

	protected void Awake()
	{
		mView = GameObject.FindObjectOfType<GameUIView>();

		mPersistentData = GameObject.FindObjectOfType<GamePersistentData>();

		EventBus.OnBaseHealthChanged += _onHealthChanged;
		EventBus.OnEnemyDestroyed    += _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing   += _onNewWaveIsComming;
	}

	protected void _onHealthChanged(float value)
	{
		mView.HealthValue = value;
	}

	protected void _onEnemyDestroyed(uint reward)
	{
		mPersistentData.mCurrScore += reward;

		mView.ScoreValue = mPersistentData.mCurrScore;
	}

	protected void _onNewWaveIsComming(int waveIndex)
	{
		mView.WavesProgress = waveIndex;
	}
}