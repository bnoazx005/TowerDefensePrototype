using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class EnemySpawnerComponent
///
/// The class is a component that is responsible for spawning
/// new enemies
/// </summary>

public class EnemySpawnerComponent : BaseComponent
{
    public float             mSpawningInterval;

    public float             mCurrSpawningTimer;

    public bool              mIsWaitingForNextWave = true;

    public float             mPerWaveRestInterval;

    public float             mCurrPerWaveRestTimer;

    public WaypointComponent mStartWaypoint;

    public int               mCurrWaveIndex;

    public int               mCurrEnemyIndex;

    public List<WaveConfig>  mWavesArray;
}
