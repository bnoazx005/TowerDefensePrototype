using UnityEngine;


/// <summary>
/// class GunComponent
/// 
/// The class is a component that contains data that is related with a gun
/// </summary>

public class GunComponent : MonoBehaviour
{
	public BaseGunConfig mConfigs;

	public float         mElapsedReloadingTime = float.MaxValue; /// float.MaxValue is used to allow to make a first shoot
}
