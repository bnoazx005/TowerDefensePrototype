using UnityEngine;
using Unity.Entities;


/// <summary>
/// class EnemySpawningSystem
///
/// The class is a system's implementation that processes enemy spawners
/// </summary>

public class EnemySpawningSystem : ComponentSystem
{
	protected Transform mEnemiesRootTransform;

	protected struct TEnemySpawnersGroup
	{
		public EnemySpawnerComponent mEnemySpawner;
	}

	protected override void OnStartRunning()
	{
		mEnemiesRootTransform = new GameObject("EnemiesList_Root").GetComponent<Transform>();
	}

	protected override void OnUpdate()
	{
		float deltaTime = Time.deltaTime;

		foreach (var entity in GetEntities<TEnemySpawnersGroup>())
		{
			_processSingleEnemySpawner(entity.mEnemySpawner, deltaTime);
		}	
	}

	protected void _processSingleEnemySpawner(EnemySpawnerComponent enemySpawner, float deltaTime)
	{
		int currWaveIndex  = enemySpawner.mCurrWaveIndex;
		int currEnemyIndex = enemySpawner.mCurrEnemyIndex;

		if (!_waitForNextWave(enemySpawner, deltaTime))
		{
			return;
		}

		if (currWaveIndex >= enemySpawner.mWavesArray.Count)
		{
			EventBus.NotifyOnLevelFinished();

			return;
		}

		Vector3 spawningPosition = enemySpawner.CachedTransform.position;

		GameObject currEnemyPrefab = null;

		WaveConfig currWaveConfig = null;

		/// time to spawn new enemy
		if (enemySpawner.mCurrSpawningTimer > enemySpawner.mSpawningInterval || currEnemyIndex == 0)
		{
			currWaveConfig = enemySpawner.mWavesArray[currWaveIndex];

			/// new waves should be started
			if (currEnemyIndex >= currWaveConfig.EnemiesCount)
			{
				currWaveIndex  = ++enemySpawner.mCurrWaveIndex;
				currEnemyIndex = 0;

				enemySpawner.mCurrEnemyIndex = 0;

				enemySpawner.mIsWaitingForNextWave = true;

				/// TODO: inform player about a new wave is incoming

				return;
			}

			currEnemyPrefab = enemySpawner.mWavesArray[currWaveIndex][currEnemyIndex];

			enemySpawner.mCurrEnemyIndex = currEnemyIndex + 1;

			_spawnNewEnemy(currEnemyPrefab, spawningPosition, enemySpawner.mStartWaypoint);

			/// reset spawning timer
			enemySpawner.mCurrSpawningTimer = 0.0f;
		}

		enemySpawner.mCurrSpawningTimer += deltaTime;
	}

	protected void _spawnNewEnemy(GameObject enemyPrefab, Vector3 position, WaypointComponent startWaypoint)
	{
		GameObject instantiatedEnemy = GameObject.Instantiate(enemyPrefab, position, Quaternion.identity, mEnemiesRootTransform);
		
		EnemyComponent enemyComponent = instantiatedEnemy.GetComponent<EnemyComponent>();

		if (enemyComponent == null)
		{
			return;
		}
		
		/// copy shared values into individual fields
		enemyComponent.mHealth       = enemyComponent.mConfigs.mHealth; 
		enemyComponent.mCurrWaypoint = startWaypoint;

		/// update enemy's UI
		HealthBarView healthBarView = enemyComponent.GetComponentInChildren<HealthBarView>();
		healthBarView.CurrNormalizedHealth = 1.0f;
	}

	protected bool _waitForNextWave(EnemySpawnerComponent enemySpawner, float deltaTime)
	{
		/// don't start timer because a previous wave is still coming
		if (!enemySpawner.mIsWaitingForNextWave)
		{
			return true;
		}

		if (enemySpawner.mCurrPerWaveRestTimer < enemySpawner.mPerWaveRestInterval)
		{
			enemySpawner.mCurrPerWaveRestTimer += deltaTime;

			return false;
		}

		EventBus.NotifyOnNewWaveIsComing(enemySpawner.mCurrWaveIndex + 1); // a first displaying index will be 1 instead of 0

		enemySpawner.mIsWaitingForNextWave = false;

		enemySpawner.mCurrPerWaveRestTimer = 0.0f;

		return true;
	}
}