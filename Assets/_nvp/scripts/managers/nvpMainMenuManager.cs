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
	public void OnOpenServerSettingsClicked(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnOpenServerSettingsSceneRequested, this, null);
	}

	public void OnOpenChatWithUniqueId(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnOpenChatWithUniqueIdSceneRequested, this, null);
	}

	public void OnOpenUserSettingsClicked(){
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnOpenUserSettingsSceneRequested, this, null);
	}
}
