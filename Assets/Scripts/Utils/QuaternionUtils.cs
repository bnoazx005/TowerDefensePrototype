using UnityEngine;


/// <summary>
/// class QuaternionUtils
///
/// The static class contains helper methods to extend existing Quaternion's implementation 

public static class QuaternionUtils
{
	/// <summary>
    /// The method create a rotation towards forward direction around Y axis
    /// </summary>
    /// <param name="forward">The direction to look in</param>
    /// <returns>Creates a rotation with the specified forward around Y axis</returns>

	public static Quaternion LookRotationXZ(Vector3 forward)
	{
		/// exclude Y component
		forward.y = 0.0f;
		forward.Normalize();	/// renormalize the vector

		return Quaternion.LookRotation(forward);
	}
}