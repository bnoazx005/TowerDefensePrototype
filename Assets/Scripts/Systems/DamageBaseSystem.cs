﻿using UnityEngine;
using Unity.Entities;


/// <summary>
/// class DamageBaseSystem
///
/// The class is a system's implementation that processes taking damage by player's base
/// </summary>

public class DamageBaseSystem : ComponentSystem
{
	protected struct TBaseGroup
	{
		public BaseComponent mBase;
	}

	protected override void OnUpdate()
	{
		foreach (var entity in GetEntities<TBaseGroup>())
		{
			/// method returns true if base was destroyed
			if (_processSingleBase(entity.mBase))
			{
				break;
			}
		}	
	}

	protected bool _processSingleBase(BaseComponent baseComponent)
	{
		if (baseComponent.mAttackingEntity == null)
		{
			return false;
		}

		float currHealth = Mathf.Max(0.0f, baseComponent.mHealth - baseComponent.mAttackingEntity.mConfigs.mDamage);

		baseComponent.mHealth = currHealth;

		DestroyedComponent enemyDestroyer = baseComponent.mAttackingEntity.GetComponent<DestroyedComponent>();

		enemyDestroyer.mShouldBeDestroyed = true;	 /// kill the enemy	

		/// reset the field
		baseComponent.mAttackingEntity = null;

		if (currHealth < 1e-3f)
		{
			Debug.Log("GAME OVER"); // TODO: replace with proper UI message

			return false;
		}

		return true;
	}
}