using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


/// <summary>
/// class TurretsLogicSystem
///
/// The class is a system's implementation that processes all placed turrets that are currently active
/// in the game session
/// </summary>

public class TurretsLogicSystem : ComponentSystem
{
	protected struct TInstantiationCommand
	{
		public Vector3       mGunPosition;
		public Vector3       mEnemyTargetPosition;
		public BaseGunConfig mGunConfigs;
	}

	protected Transform mBulletsRootTransform;

	protected List<TInstantiationCommand> mInstantiationBuffer; /// used to deffered instantiations of bullets

	protected struct TTurretGroup
	{
		public GunComponent mGun;
	}

	protected struct TEnemyGroup
	{
		public EnemyComponent mEnemy;
	}

	protected override void OnStartRunning()
	{
		mBulletsRootTransform = new GameObject("BulletsList_Root").GetComponent<Transform>();

		mInstantiationBuffer = new List<TInstantiationCommand>();
	}

	protected override void OnUpdate()
	{
		mInstantiationBuffer.Clear();

		foreach (var entity in GetEntities<TTurretGroup>())
		{
			_processSingleTurret(entity.mGun, GetEntities<TEnemyGroup>());
		}

		/// deffered instantiation of prebafs
		foreach (var command in mInstantiationBuffer)
		{
			_makeShot(command);
		}
	}

	protected void _processSingleTurret(GunComponent gun, ComponentGroupArray<TEnemyGroup> enemies)
	{
		Transform gunTransform = gun.GetComponent<Transform>();

		BaseGunConfig gunConfigs = gun.mConfigs;

		/// this method can return null if there are no enemies within an attack zone
		EnemyComponent nearestEnemy = _getNearestEnemy(gunTransform, gunConfigs.mRadius, enemies);

		/// skip other logic if there are no enemies near the turret
		if (nearestEnemy == null)
		{
			return;
		}

		Transform enemyTransform = nearestEnemy.GetComponent<Transform>();

		/// rotate the turret towards a target
		gunTransform.rotation = QuaternionUtils.LookRotationXZ(enemyTransform.position - gunTransform.position);

		/// shooting logic
		if (gun.mElapsedReloadingTime > gunConfigs.mReloadInterval)
		{
			/// create a deffered request for instantiation of a new bullet
			mInstantiationBuffer.Add(new TInstantiationCommand { mGunPosition = gun.mBulletSpawTransform.position, mGunConfigs = gunConfigs, mEnemyTargetPosition = enemyTransform.position });

			gun.mElapsedReloadingTime = 0.0f; // starts to wait for an end of a reloading cycle

			return;
		}

		/// wait for a gun is being reloading
		gun.mElapsedReloadingTime += Time.deltaTime;
	}

	protected EnemyComponent _getNearestEnemy(Transform gunTransform, float gunRadius, ComponentGroupArray<TEnemyGroup> enemies)
	{
		EnemyComponent currEnemy    = null;
		EnemyComponent nearestEnemy = null;

		float minDistance  = float.MaxValue;	
		float currDistance = 0.0f;

		Vector3 gunPosition = gunTransform.position;

		Transform enemyTransform = null;

		for (int i = 0; i < enemies.Length; ++i)
		{
			currEnemy = enemies[i].mEnemy;

			enemyTransform = currEnemy.GetComponent<Transform>();

			currDistance = Vector3.Distance(gunPosition, enemyTransform.position);

			if (currDistance < minDistance)
			{
				minDistance = currDistance;

				nearestEnemy = currEnemy;
			}
		}

		// if the nearest enemy is located out of attach zone of the gun return null
		if (minDistance > gunRadius)
		{
			return null;
		}

		return nearestEnemy;
	}

	protected void _makeShot(TInstantiationCommand command)
	{
		BaseGunConfig gunConfigs = command.mGunConfigs;

		/// create a new instance of a bullet prefab
		GameObject bulletInstance = GameObject.Instantiate(gunConfigs.mBulletPrefab, command.mGunPosition, Quaternion.identity, mBulletsRootTransform);

		BulletComponent bulletComponent = bulletInstance.GetComponent<BulletComponent>();

		bulletComponent.mDamage         = gunConfigs.mDamage;
		bulletComponent.mSpeed          = gunConfigs.mBulletSpeed;
		bulletComponent.mTargetPosition = command.mEnemyTargetPosition;
	}
}