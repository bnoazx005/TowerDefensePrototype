using UnityEngine;
using Unity.Entities;


/// <summary>
/// class GridUpdateSystem
///
/// The class is a system's implementation that processes all grid cells appearance
/// </summary>

public class GridUpdateSystem : ComponentSystem
{
	protected struct TGridCellGroup
	{
		public GridCellComponent mGridCell;
	}

	protected override void OnUpdate()
	{
		foreach (var entity in GetEntities<TGridCellGroup>())
		{
			_processSingleGridCell(entity.mGridCell);
		}
	}

	protected void _processSingleGridCell(GridCellComponent gridCellComponent)
	{
		bool isEmpty = gridCellComponent.mTurretEntity == null;
		
		gridCellComponent.mEmptyGridView.SetActive(isEmpty);
		gridCellComponent.mFilledGridView.SetActive(!isEmpty);
	}
}