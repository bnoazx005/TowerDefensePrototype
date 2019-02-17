using UnityEngine;
using Unity.Entities;


/// <summary>
/// class MoveEnemySystem
///
/// The class is a system's implementation that processes all enemies in the game and moves them along
/// a navigation path
/// </summary>

public class MoveEnemySystem : ComponentSystem
{
	protected struct TEnemyGroup
	{
		public EnemyComponent mEnemy;
	}

	protected override void OnUpdate()
	{
		EnemyComponent currEnemy = null;

		BaseEnemyConfig currEnemyConfig = null;

		WaypointComponent currWaypoint = null;

		float currSpeed = 0.0f;

		float deltaTime = Time.deltaTime;

		Transform enemyTransform    = null;
		Transform waypointTransform = null;

		Vector3 dir;

		float distance = 0.0f;

		foreach (var entity in GetEntities<TEnemyGroup>())
		{
			currEnemy       = entity.mEnemy;
			currEnemyConfig = currEnemy.mConfigs;
			currWaypoint    = currEnemy.mCurrWaypoint;

			currSpeed = currEnemyConfig.mSpeed * deltaTime;

			enemyTransform    = currEnemy.GetComponent<Transform>();
			waypointTransform = currWaypoint.GetComponent<Transform>();

			/// until the enemy doesn't reach the waypoint update its position
			dir = waypointTransform.position - enemyTransform.position;

			distance = dir.magnitude;

			dir.Normalize();

			/// rotate an enemy along its move direction
			enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, QuaternionUtils.LookRotationXZ(dir), currEnemyConfig.mRotationSpeed * deltaTime);

			if (distance > 0.1f) /// epsilon
			{
				enemyTransform.position += dir * currSpeed;

				continue;
			}

			if (currWaypoint.mNextWaypoint != null)
			{
				currEnemy.mCurrWaypoint = currWaypoint.mNextWaypoint;
			}
		}
	}
}