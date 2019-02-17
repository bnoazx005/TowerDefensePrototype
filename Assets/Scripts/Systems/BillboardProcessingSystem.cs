using UnityEngine;
using Unity.Entities;


/// <summary>
/// class BillboardProcessingSystem
///
/// The class is a system's implementation that processes all billboards
/// that exist in the scene
/// </summary>

public class BillboardProcessingSystem : ComponentSystem
{
	protected Transform mMainCameraTransform;

	protected struct TBillboardGroup
	{
		public BillboardComponent mBillboard;
	}

	protected override void OnStartRunning()
	{
		mMainCameraTransform = Camera.main.GetComponent<Transform>();
	}

	protected override void OnUpdate()
	{
		Vector3 cameraPosition = mMainCameraTransform.position;

		Transform billboardTransform = null;

		foreach (var entity in GetEntities<TBillboardGroup>())
		{
			billboardTransform = entity.mBillboard.CachedTransform;
			
			//billboardTransform.rotation = QuaternionUtils.LookRotationXZ(cameraPosition - billboardTransform.position);
			billboardTransform.LookAt(cameraPosition, -Vector3.up);
		}
	}
}