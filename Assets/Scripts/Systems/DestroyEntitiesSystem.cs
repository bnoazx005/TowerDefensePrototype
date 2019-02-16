using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

using UnityEngine.Experimental.PlayerLoop;

/// <summary>
/// class DestroyEntitiesSystem
///
/// The class is a system's implementation that processes all DestroyComponents 
/// </summary>

[UpdateBefore(typeof(PreLateUpdate))]
public class DestroyEntitiesSystem : ComponentSystem
{
	protected struct TDestroyedGroup
	{
		public DestroyedComponent mDestroyedEntity;
	}

	protected override void OnUpdate()
	{
		List<GameObject> toDestroyCommandBuffer = new List<GameObject>();

		DestroyedComponent currDestroyedEntity = null;

		foreach (var entity in GetEntities<TDestroyedGroup>())
		{
			currDestroyedEntity = entity.mDestroyedEntity;

			if (!currDestroyedEntity.mShouldBeDestroyed)
			{
				continue;
			}

			toDestroyCommandBuffer.Add(currDestroyedEntity.gameObject);

			break;
		}

		foreach (var currCommand in toDestroyCommandBuffer)
		{
			GameObject.Destroy(currCommand);
		}
	}
}