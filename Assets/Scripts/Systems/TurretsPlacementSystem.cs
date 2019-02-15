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

	protected override void OnStartRunning()
	{
		mTurretsRootTransform = new GameObject("TurretsList_Root").GetComponent<Transform>();

		mMainCamera = Camera.main;

		mGridCellPhysicsLayerMask = 1 << LayerMask.NameToLayer(GameSettings.mGridCellPhysicsLayerName);

		mGamePersistentData = GameObject.FindObjectOfType<GamePersistentData>();
	}

	protected override void OnUpdate()
	{
		if (mMainCamera == null ||
			mGamePersistentData == null)
		{
			return;
		}

		// search for a grid cell
		Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitResult;

		if (!Physics.Raycast(ray, out hitResult,  float.MaxValue, mGridCellPhysicsLayerMask))
		{
			return;
		}

		if (!Input.GetKeyDown(KeyCode.Mouse0))
		{
			return;
		}

		GridCellComponent gridCell = hitResult.collider.GetComponent<GridCellComponent>();

		// if a player selected and clicked over a cell create a new turret if the cell is empty
		if (gridCell.mTurretEntity != null)
		{
			return;
		}

		_createNewTurret(gridCell);
	}

	protected void _createNewTurret(GridCellComponent selectedGridCell)
	{
		Transform gridCellTransform = selectedGridCell.GetComponent<Transform>();

		Vector3 gridCellPosition = gridCellTransform.position;

		GameObject selectedTurretPrefab = mGamePersistentData.mTurrets[mGamePersistentData.mCurrSelectedTurretIndex];

		GameObject newTurret = GameObject.Instantiate(selectedTurretPrefab, gridCellPosition, Quaternion.identity, mTurretsRootTransform);

		selectedGridCell.mTurretEntity = newTurret.GetComponentInChildren<GunComponent>();
	}
}