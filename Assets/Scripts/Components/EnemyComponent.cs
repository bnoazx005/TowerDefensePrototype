using UnityEngine;


/// <summary>
/// class EnemyComponent
/// The class is a component which all enemies in the game should have
/// </summary>

public class EnemyComponent : MonoBehaviour
{
    public WaypointComponent mCurrWaypoint = null; /// when mCurrWaypoint equals to null the enemy will try to find first waypoint of a path

    public BaseEnemyConfig   mConfigs;

    public float             mHealth;
}
