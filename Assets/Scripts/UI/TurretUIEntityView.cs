﻿using UnityEngine;
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

	protected Button mCachedButton;

	protected void Awake()
	{
		mCachedButton = GetComponent<Button>();

		mCachedButton.onClick.AddListener(_onTurretEntityWasSelected);
	}

	protected void OnDestroy()
	{
		mCachedButton.onClick.RemoveListener(_onTurretEntityWasSelected);
	}

	protected void _onTurretEntityWasSelected()
	{
		EventBus.NotifyOnStartTurretPlacement(mTurretEntityId);
	}

	public bool IsEnabled { set => mCachedButton.interactable = value; }
}