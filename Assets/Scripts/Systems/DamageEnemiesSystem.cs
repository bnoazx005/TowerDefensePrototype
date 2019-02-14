using UnityEngine;
using Unity.Entities;


/// <summary>
/// class DamageEnemiesSystem
///
/// The class is a system's implementation that processes taking damage by all enemies
/// </summary>

public class DamageEnemiesSystem : ComponentSystem
{
	protected struct TBulletGroup
	{
		public BulletComponent mBullet;
	}

	protected override void OnUpdate()
	{
		foreach (var entity in GetEntities<TBulletGroup>())
		{
			if (_processSingleBullet(entity.mBullet))
			{
				break;
			}
		}	
	}

	protected bool _processSingleBullet(BulletComponent bullet)
	{
		EnemyComponent damagedEnemy = bullet.mDamagedEnemy;

		/// skip the bullet because it didn't hit any enemy
		if (damagedEnemy == null)
		{
			return false;
		}

		HealthBarView healthBarView = damagedEnemy.GetComponentInChildren<HealthBarView>();

		float newHealth = Mathf.Max(0.0f, damagedEnemy.mHealth - bullet.mDamage);

		healthBarView.CurrNormalizedHealth = newHealth / damagedEnemy.mConfigs.mHealth; /// compute normalized health's value 
		damagedEnemy.mHealth               = newHealth;

		/// destroy enemy's view
		if (damagedEnemy.mHealth < 1e-3f)
		{
			GameObject.Destroy(damagedEnemy.gameObject);
		}

		/// TODO: return bullet to an object pool of bullets
		GameObject.Destroy(bullet.gameObject);

		return true;
	}
}