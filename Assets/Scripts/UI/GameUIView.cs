using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class GameUIView
///
/// The clas is a view of in-game UI that provides an access to all
/// indicators
/// </summary>


public class GameUIView : MonoBehaviour
{
	public Text                    mScoreValueLabel;

	public Text                    mHealthValueLabel;

	public Text                    mWavesProgressLabel;

	protected TurretUIEntityView[] mTurretUIEntityViewsArray;

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
