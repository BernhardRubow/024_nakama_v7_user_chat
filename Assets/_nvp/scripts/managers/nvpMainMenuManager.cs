using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;

public class nvpMainMenuManager : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private Button _editSettingsButton;
	[SerializeField] private Button _startChatDeviceId;



	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start(){

		if(nvpGameManager.HOST == string.Empty){
			_startChatDeviceId.gameObject.SetActive(false);
		}

	}

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void ConnectToServer(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnOpenSettingsMenuRequested, this, null);
	}

	public void StartChatDeviceId(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnStartChatWithDeviceIdRequested, this, null);
	}
}
