using UnityEngine;


/// <summary>
/// abstract class BaseGunConfig
///
/// The class is used to configure a common properties of different guns
/// that are available within the game
/// </summary>

public abstract class BaseGunConfig : ScriptableObject
{
	public float      mDamage;

	public float      mRadius;

	public float      mBulletSpeed;

	public float      mReloadInterval;

	public GameObject mBulletPrefab;
}