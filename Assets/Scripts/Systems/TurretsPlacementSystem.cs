using System;
using UnityEngine;
using Unity.Entities;


/// <summary>
/// class TurretsPlacementSystem
///
/// The class is a system's implementation that provides an ability
/// of building and placing of new turrets
/// </summary>

public class TurretsPlacementSystem : ComponentSystem
{
	protected Transform          mTurretsRootTransform;

	protected Camera             mMainCamera;

	protected int                mGridCellPhysicsLayerMask;

	protected GamePersistentData mGamePersistentData;

	protected int                mCurrSelectedTurretIndex;

	protected override void OnStartRunning()
	{
		mTurretsRootTransform = new GameObject("TurretsList_Root").GetComponent<Transform>();

		mMainCamera = Camera.main;

		mGridCellPhysicsLayerMask = 1 << LayerMask.NameToLayer(GameSettings.mGridCellPhysicsLayerName);

		mGamePersistentData = GameObject.FindObjectOfType<GamePersistentData>();

		mCurrSelectedTurretIndex = -1;

		EventBus.OnStartTurretPlacement += _onStartTurretPlacement;
	}

	protected override void OnUpdate()
	{
		if (mMainCamera == null ||
			mGamePersistentData == null ||
			mCurrSelectedTurretIndex < 0)
		{
			return;
		}

		// search for a grid cell
		Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitResult;

		if (!Physics.Raycast(ray, out hitResult,  float.MaxValue, mGridCellPhysicsLayerMask))
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				mCurrSelectedTurretIndex = -1; // if a player clicks somewhere else then a cell

				return;
			}

			return;
		}

		GridCellComponent gridCell = hitResult.collider.GetComponent<GridCellComponent>();

		/// update cell's appearance
		gridCell.mEmptyGridView.SetActive(false);
		gridCell.mFilledGridView.SetActive(true);

		if (!Input.GetKeyDown(KeyCode.Mouse0))
		{
			return;
		}

		// if a player selected and clicked over a cell create a new turret if the cell is empty
		if (gridCell.mTurretEntity != null)
		{
			mCurrSelectedTurretIndex = -1; // if a player clicks over a filled cell disable placement mode

			return;
		}

		_createNewTurret(gridCell);
	}

	protected void _createNewTurret(GridCellComponent selectedGridCell)
	{
		Transform gridCellTransform = selectedGridCell.GetComponent<Transform>();

		Vector3 gridCellPosition = gridCellTransform.position;

		GameObject selectedTurretPrefab = mGamePersistentData.mTurrets[mCurrSelectedTurretIndex];
		GameObject newTurretGO          = GameObject.Instantiate(selectedTurretPrefab, gridCellPosition, Quaternion.identity, mTurretsRootTransform);

		GunComponent turretGunComponent = newTurretGO.GetComponentInChildren<GunComponent>();

		selectedGridCell.mTurretEntity = turretGunComponent;

		uint turretPrice = turretGunComponent.mConfigs.mPrice;

		EventBus.NotifyOnNewTurretWasCreated(turretPrice);

		mCurrSelectedTurretIndex = -1;
	}

	protected void _onStartTurretPlacement(uint turretEntityId)
	{
		mCurrSelectedTurretIndex = Convert.ToInt32(turretEntityId);

		Debug.Log(mCurrSelectedTurretIndex);
	}
}