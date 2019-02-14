using UnityEngine;


/// <summary>
/// class BulletCollisionListener
///
/// The class is a herlper infrastructure that listens to all physics events
/// which can be caused by a bullet


[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(BulletComponent))]
public class BulletCollisionListener : MonoBehaviour
{
	protected BulletComponent mCachedBulletComponent;

	protected void Start()
	{
		mCachedBulletComponent = GetComponent<BulletComponent>();
	}

	protected void OnTriggerEnter(Collider collider)
	{
		mCachedBulletComponent.mDamagedEnemy = collider.GetComponent<EnemyComponent>();
	}
}
