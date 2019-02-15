using UnityEngine;


/// <summary>
/// class GamePersistentData
///
/// The class represents a hub of all global in-game variables that are available for all systems and classes
/// in the project
/// </summary>

public class GamePersistentData : MonoBehaviour
{
	public TurretsCollection mTurrets;

	public int               mCurrSelectedTurretIndex = -1;

	public uint              mCurrScore;
}
