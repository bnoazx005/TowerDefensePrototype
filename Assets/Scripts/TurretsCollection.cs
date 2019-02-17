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
	[Serializable]
	public struct TTurretEntity
	{
		public GameObject mPrefab;

		public Sprite     mPreviewImage;
	}

	[SerializeField]
	protected List<TTurretEntity> mTurretsPrefabs = new List<TTurretEntity>();

	public Sprite GetPreviewImage(int index)
	{
		if (index >= mTurretsPrefabs.Count)
		{
			throw new IndexOutOfRangeException("index");
		}

		return mTurretsPrefabs[index].mPreviewImage;
	}

	public GameObject this[int index]
	{
		get
		{
			if (index >= mTurretsPrefabs.Count)
			{
				throw new IndexOutOfRangeException("index");
			}

			return mTurretsPrefabs[index].mPrefab;
		}
	}
}