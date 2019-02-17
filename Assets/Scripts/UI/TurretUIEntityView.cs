using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/// <summary>
/// class TurretUIEntity
///
/// The class represents a view of UI element which is related with
/// a particular turret's type
/// </summary>

[RequireComponent(typeof(Button))]
public class TurretUIEntityView : MonoBehaviour
{
	public uint      mTurretEntityId;

	public Image     mPreviewImage;

	public Text      mPriceLabel;

	protected Button mCachedButton;

	protected void Awake()
	{
		CachedButton.onClick.AddListener(_onTurretEntityWasSelected);
	}

	protected void OnDestroy()
	{
		CachedButton.onClick.RemoveListener(_onTurretEntityWasSelected);
	}

	protected void _onTurretEntityWasSelected()
	{
		EventBus.NotifyOnStartTurretPlacement(mTurretEntityId);
	}

	public bool IsEnabled { set => CachedButton.interactable = value; }

	public Sprite PreviewImage { set => mPreviewImage.sprite = value; }

	public uint Price { set => mPriceLabel.text = value.ToString(); }

	public Button CachedButton
	{
		get
		{
			/// caching script's component
			if (mCachedButton == null)
			{
				mCachedButton = GetComponent<Button>();
			}

			return mCachedButton;
		}
	}
}
