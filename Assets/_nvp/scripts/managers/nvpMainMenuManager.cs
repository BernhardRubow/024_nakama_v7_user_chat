using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvpMainMenuManager : MonoBehaviour {

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void ConnectToServer(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnOpenSettingsMenuRequested, this, null);
	}
}
