using UnityEngine;
using Unity.Entities;


/// <summary>
/// class ProcessBulletsSystem
///
/// The class is a system's implementation that processes all bullets that are fired by turrets
/// </summary>

[UpdateAfter(typeof(TurretsLogicSystem))]
public class ProcessBulletsSystem : ComponentSystem
{
	protected struct TBulletGroup
	{
		public BulletComponent mBullet;
	}

	protected override void OnUpdate()
	{
		BulletComponent currBullet = null;

		float deltaTime = Time.deltaTime;

		foreach (var entity in GetEntities<TBulletGroup>())
		{
			/// if the method returns true then the bullet was destroyed skip update of other to get correct array of bullets
			if (_processSingleBullet(entity.mBullet, deltaTime))
			{
				break;
			}
		}
	}

	protected bool _processSingleBullet(BulletComponent bullet, float deltaTime)
	{
		Transform bulletTransform = bullet.CachedTransform;

		Vector3 dir = bullet.mTargetPosition - bulletTransform.position;

		float distance = dir.magnitude;

		/// move a bullet towards a goal
		if (distance > 0.1f)
		{
			bulletTransform.position += dir.normalized * (deltaTime * bullet.mSpeed);

			return false;
		}

		// destroy bullet or better push it back to an object pool
		//GameObject.Destroy(bulletTransform.gameObject);
		DestroyedComponent destroyedBullet = bulletTransform.GetComponent<DestroyedComponent>();
		destroyedBullet.mShouldBeDestroyed = true;

		return true;
	}
}