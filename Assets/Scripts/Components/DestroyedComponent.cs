using UnityEngine;


/// <summary>
/// class DestroyedComponent
///
/// The class is a component which is a flag for DestroyEntitiesSystem
/// to destroy the entity
/// </summary>

public class DestroyedComponent : MonoBehaviour
{
	public bool mShouldBeDestroyed = false;
}
