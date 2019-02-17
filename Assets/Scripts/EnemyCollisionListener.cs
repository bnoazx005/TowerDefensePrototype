using UnityEngine;


/// <summary>
/// class EnemyCollisionListener
///
/// The class is a helper infrastructure that listens to all physics events
/// which can be caused by an enemy
/// </summary>


[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(EnemyComponent))]
public class EnemyCollisionListener : MonoBehaviour
{
	protected void OnTriggerEnter(Collider collider)
	{
		PlayerBaseComponent baseComponent = collider.GetComponent<PlayerBaseComponent>();

		// it's not a base object 
		if (baseComponent == null)
		{
			return;
		}

		baseComponent.mAttackingEntity = GetComponent<EnemyComponent>();
	}
}
