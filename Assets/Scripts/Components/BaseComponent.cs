using UnityEngine;


/// <summary>
/// class BaseComponent
///
/// The class is a base component. All components should derive this one
/// </summary>


public class BaseComponent : MonoBehaviour
{
	protected Transform mCachedTransform;

	public Transform CachedTransform
	{
		get
		{
			if (mCachedTransform == null)
			{
				mCachedTransform = GetComponent<Transform>();
			}

			return mCachedTransform;
		}
	}
}
