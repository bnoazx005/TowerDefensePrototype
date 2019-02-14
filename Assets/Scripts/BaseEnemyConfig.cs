using UnityEngine;


/// <summary>
/// abstract class BaseEnemyConfig
///
/// The class is used to configure a common properties of a group of enemies
/// If you need to implement specific kind of enemy derive your own class from this one. 
/// </summary>

public abstract class BaseEnemyConfig : ScriptableObject
{
	public float mHealth;

	public float mSpeed;

	public float mDamage;
}