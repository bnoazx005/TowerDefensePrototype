using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class HealthBarView
///
/// The class is used to display current health's status of an entity 

public class HealthBarView : MonoBehaviour
{
	public RectTransform mHealthStatusImage;

	protected Vector2    mLeftPosition;

	protected Vector2    mRightPosition;

	protected void Start()
	{
		mRightPosition = mHealthStatusImage.anchoredPosition;
		mLeftPosition  = mRightPosition - new Vector2(mHealthStatusImage.rect.width, 0.0f);		
	}

	public float CurrNormalizedHealth 
	{
		set
		{
			mHealthStatusImage.anchoredPosition = Vector2.Lerp(mLeftPosition, mRightPosition, Mathf.Clamp01(value));
		}
	}
}
