using UnityEngine;


/// <summary>
/// class EnemySpawnerComponent
///
/// The class is a component that is responsible for spawning
/// new enemies
/// </summary>

public class EnemySpawnerComponent : MonoBehaviour
{
    public GameObject        mEnemyPrefab;

    public float             mSpawningInterval;

    public float             mCurrSpawningTimer;

    public WaypointComponent mStartWaypoint;
}
