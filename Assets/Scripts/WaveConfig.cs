using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// abstract class WaveConfig
///
/// The class is used to configure a wave of enemies in the level
/// </summary>

public abstract class WaveConfig : ScriptableObject
{
	public abstract uint EnemiesCount { get; }

	public abstract GameObject this[int index] { get; }
}