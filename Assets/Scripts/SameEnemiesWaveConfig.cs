using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class SameEnemiesWaveConfig
///
/// The class represents an enemies wave where all enemies use same prefab
/// </summary>

[CreateAssetMenu(fileName = "SameEnemiesWave_Config", menuName = "Enemies/Same Enemies Wave Config", order = 1)]
public class SameEnemiesWaveConfig : WaveConfig
{
	[SerializeField]
	protected uint       mEnemiesCount;

	[SerializeField]
	protected GameObject mEnemyPrefab;

	public override GameObject this[int index]
	{
		get
		{
			return mEnemyPrefab;
		}
	}

	public override uint EnemiesCount => mEnemiesCount;
}