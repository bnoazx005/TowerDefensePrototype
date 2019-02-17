using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/// <summary>
/// class GameUIView
///
/// The clas is a view of in-game UI that provides an access to all
/// indicators
/// </summary>


public class GameUIView : MonoBehaviour
{
	public event UnityAction OnPauseMenuButtonClicked;

	public event UnityAction OnPauseGameButtonClicked;

	public event UnityAction OnNormalTimeGameButtonClicked;

	public event UnityAction OnSpeedUpGameButtonClicked;

	public Text                    mScoreValueLabel;

	public Text                    mHealthValueLabel;

	public Text                    mWavesProgressLabel;

	public Button                  mPauseMenuButton;

	public Button                  mPauseGameButton;

	public Button                  mNormalTimeButton;

	public Button                  mSpeedUpGameButton;

	protected TurretUIEntityView[] mTurretUIEntityViewsArray;

	protected void Awake()
	{
		mPauseMenuButton?.onClick.AddListener(_onPauseMenuButtonClicked);
		mPauseGameButton?.onClick.AddListener(_onPauseGameButtonClicked);
		mNormalTimeButton?.onClick.AddListener(_onNormalTimeGameButtonClicked);
		mSpeedUpGameButton?.onClick.AddListener(_onSpeedUpGameButtonClicked);
	}

	protected void OnDestroy()
	{
		mPauseMenuButton?.onClick.RemoveListener(_onPauseMenuButtonClicked);
		mPauseGameButton?.onClick.RemoveListener(_onPauseGameButtonClicked);
		mNormalTimeButton?.onClick.RemoveListener(_onNormalTimeGameButtonClicked);
		mSpeedUpGameButton?.onClick.RemoveListener(_onSpeedUpGameButtonClicked);
	}

	protected void _onPauseMenuButtonClicked()
	{
		OnPauseMenuButtonClicked?.Invoke();
	}

	protected void _onPauseGameButtonClicked()
	{
		OnPauseGameButtonClicked?.Invoke();		
	}

	protected void _onNormalTimeGameButtonClicked()
	{
		OnNormalTimeGameButtonClicked?.Invoke();		
	}

	protected void _onSpeedUpGameButtonClicked()
	{
		OnSpeedUpGameButtonClicked?.Invoke();		
	}

	public uint ScoreValue { set => mScoreValueLabel.text = value.ToString(); }

	public float HealthValue { set => mHealthValueLabel.text = value.ToString(); }

	public int WavesProgress { set => mWavesProgressLabel.text = value.ToString(); }

	public TurretUIEntityView[] TurretUIEntityViewArray
	{
		get
		{
			if (mTurretUIEntityViewsArray == null)
			{
				mTurretUIEntityViewsArray = FindObjectsOfType<TurretUIEntityView>();
			}

			return mTurretUIEntityViewsArray;
		}
	}
}
