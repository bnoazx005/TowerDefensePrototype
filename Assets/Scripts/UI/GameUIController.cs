using System;
using UnityEngine;


/// <summary>
/// class GameUIController
///
/// The class provides a logic for in-game UI and updates it
/// using available data
/// </summary>

public class GameUIController
{
	protected GameUIView         mView;

	protected GamePersistentData mPersistentData;

	public GameUIController(GameUIView view, GamePersistentData data)
	{
		mView           = view ?? throw new ArgumentNullException("view");
		mPersistentData = data ?? throw new ArgumentNullException("data");

		EventBus.OnBaseHealthChanged   += _onHealthChanged;
		EventBus.OnEnemyDestroyed      += _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing     += _onNewWaveIsComming;
		EventBus.OnNewTurretWasCreated += _onNewTurretWasCreated;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);
	}

	~GameUIController()
	{		
		EventBus.OnBaseHealthChanged   -= _onHealthChanged;
		EventBus.OnEnemyDestroyed      -= _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing     -= _onNewWaveIsComming;
		EventBus.OnNewTurretWasCreated -= _onNewTurretWasCreated;
	}

	protected void _onHealthChanged(float value)
	{
		mView.HealthValue = value;
	}

	protected void _onEnemyDestroyed(uint reward)
	{
		mPersistentData.mCurrScore += reward;

		mView.ScoreValue = mPersistentData.mCurrScore;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);
	}

	protected void _onNewWaveIsComming(int waveIndex)
	{
		mView.WavesProgress = waveIndex;
	}

	protected void _onNewTurretWasCreated(uint turretPrice)
	{
		mPersistentData.mCurrScore = Math.Max(0, mPersistentData.mCurrScore - turretPrice);

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);		
	}

	protected void _updateAvailableTurretsUIList(TurretsCollection turrets)
	{
		/// TODO: REPLACE THIS UGLY CODE WITH A PROPER ONE
		mView.TurretUIEntityView.IsEnabled = mPersistentData.mCurrScore >= turrets[Convert.ToInt32(mView.TurretUIEntityView.mTurretEntityId)].GetComponentInChildren<GunComponent>().mConfigs.mPrice;		
	}
}