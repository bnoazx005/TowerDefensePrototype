﻿using UnityEngine;


/// <summary>
/// class GridCellComponent
///
/// The class is a component which represents single grid's cell
/// for placing turrets
/// </summary>

public class GridCellComponent : BaseComponent
{
	public GunComponent mTurretEntity;

	public GameObject   mEmptyGridView;

	public GameObject   mFilledGridView;
}
