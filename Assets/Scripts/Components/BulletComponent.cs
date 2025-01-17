﻿using UnityEngine;


/// <summary>
/// class BulletComponent
///
/// The class represents a component which is a bullet
/// </summary>


public class BulletComponent : BaseComponent
{
	public float          mDamage;

	public float          mSpeed;

	public Vector3        mTargetPosition;

	public EnemyComponent mDamagedEnemy; // null if no enemy was hit, component's reference in other way
}
