using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class TurretsCollection
/// The class represents a collection of all turrets that are available in the game
/// </summary>

[CreateAssetMenu(fileName = "Turrets_Collection", menuName = "Turrets Collection", order = 1)]
public class TurretsCollection: ScriptableObject
{
	public List<GameObject> mTurretsPrefabs = new List<GameObject>();

	public GameObject this[int index]
	{
		get
		{
			if (index >= mTurretsPrefabs.Count)
			{
				throw new IndexOutOfRangeException("index");
			}

			return mTurretsPrefabs[index];
		}
	}
}