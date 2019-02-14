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
		Vector3 spawningPosition = enemySpawner.GetComponent<Transform>().position;

		GameObject instantiatedEnemy = null;

		EnemyComponent enemyComponent = null;

		/// time to spawn new enemy
		if (enemySpawner.mCurrSpawningTimer > enemySpawner.mSpawningInterval)
		{
			instantiatedEnemy = GameObject.Instantiate(enemySpawner.mEnemyPrefab, spawningPosition, Quaternion.identity, mEnemiesRootTransform);

			enemyComponent = instantiatedEnemy.GetComponent<EnemyComponent>();

			if (enemyComponent == null)
			{
				return;
			}
			
			/// copy shared values into individual fields
			enemyComponent.mHealth       = enemyComponent.mConfigs.mHealth; 
			enemyComponent.mCurrWaypoint = enemySpawner.mStartWaypoint;

			/// update enemy's UI
			HealthBarView healthBarView = enemyComponent.GetComponentInChildren<HealthBarView>();
			healthBarView.CurrNormalizedHealth = 1.0f;

			/// reset spawning timer
			enemySpawner.mCurrSpawningTimer = 0.0f;
		}

		enemySpawner.mCurrSpawningTimer += deltaTime;
	}
}