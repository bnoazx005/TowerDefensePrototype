using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class GenericWaveConfig
///
/// The class represents an enemies wave where all enemies can use their own prefabs
/// </summary>

[CreateAssetMenu(fileName = "GenericEnemiesWave_Config", menuName = "Enemies/Generic Enemies Wave Config", order = 1)]
public class GenericWaveConfig : WaveConfig
{
	[SerializeField]
	protected List<GameObject> mEnemiesPrefabs = new List<GameObject>();

	public override GameObject this[int index]
	{
		get
		{
			if (index >= mEnemiesPrefabs.Count)
			{
				throw new IndexOutOfRangeException("index");
			}

			return mEnemiesPrefabs[index];
		}
	}

	public override uint EnemiesCount => Convert.ToUInt32(mEnemiesPrefabs.Count);
}