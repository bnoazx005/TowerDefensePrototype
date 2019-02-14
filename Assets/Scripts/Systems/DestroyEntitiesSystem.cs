using UnityEngine;
using Unity.Entities;


/// <summary>
/// class DestroyEntitiesSystem
///
/// The class is a system's implementation that processes all DestroyComponents 
/// </summary>

public class DestroyEntitiesSystem : ComponentSystem
{
	protected struct TDestroyedGroup
	{
		public DestroyedComponent mDestroyedEntity;
	}

	protected override void OnUpdate()
	{
		var entities = GetEntities<TDestroyedGroup>();

		DestroyedComponent currDestroyedEntity = null;

		foreach (var entity in GetEntities<TDestroyedGroup>())
		{
			currDestroyedEntity = entity.mDestroyedEntity;

			if (!currDestroyedEntity.mShouldBeDestroyed)
			{
				continue;
			}

			GameObject.Destroy(currDestroyedEntity.gameObject);

			break;
		}
	}
}