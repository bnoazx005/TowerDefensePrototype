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

		mView.ScoreValue = mPersistentData.mCurrScore;

		mView.OnPauseGameButtonClicked      += _onPauseGameTime;
		mView.OnNormalTimeGameButtonClicked += _onSetNormalGameTime;
		mView.OnSpeedUpGameButtonClicked    += _onSpeedUpGameTime;
	}

	~GameUIController()
	{		
		EventBus.OnBaseHealthChanged   -= _onHealthChanged;
		EventBus.OnEnemyDestroyed      -= _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing     -= _onNewWaveIsComming;
		EventBus.OnNewTurretWasCreated -= _onNewTurretWasCreated;

		mView.OnPauseGameButtonClicked      -= _onPauseGameTime;
		mView.OnNormalTimeGameButtonClicked -= _onSetNormalGameTime;
		mView.OnSpeedUpGameButtonClicked    -= _onSpeedUpGameTime;
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

		mView.ScoreValue = mPersistentData.mCurrScore;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);		
	}

	protected void _updateAvailableTurretsUIList(TurretsCollection turrets)
	{
		TurretUIEntityView[] turretsUISlots = mView.TurretUIEntityViewArray;

		TurretUIEntityView currTurretUISlot = null;

		uint currScore = mPersistentData.mCurrScore;

		GameObject currTurretGO = null;

		int entityId = -1;

		uint currGunPrice = 0;

		for (int i = 0; i < turretsUISlots.Length; ++i)
		{
			currTurretUISlot = turretsUISlots[i];

			entityId = Convert.ToInt32(currTurretUISlot.mTurretEntityId);

			currTurretGO = turrets[entityId];

			currGunPrice = currTurretGO.GetComponentInChildren<GunComponent>().mConfigs.mPrice;

			currTurretUISlot.IsEnabled    = (currTurretGO != null) && (currScore >= currGunPrice);
			currTurretUISlot.PreviewImage = turrets.GetPreviewImage(entityId);
			currTurretUISlot.Price        = currGunPrice;
		}		
	}

	protected void _onPauseGameTime()
	{
		Time.timeScale = 0.0f;
	}

	protected void _onSetNormalGameTime()
	{
		Time.timeScale = 1.0f;		
	}

	protected void _onSpeedUpGameTime()
	{
		Time.timeScale = 3.0f;		
	}
}