using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;

public class nvpSettingsMenuManager : MonoBehaviour {

	[SerializeField] private InputField _host;
	[SerializeField] private InputField _port;

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		
	}

	void Update () {
		
	}

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void OnSave(){
		Debug.Log("OnConnectWithSettingClicked called");
		string[] parameter = new string[2];
		parameter[0] = _host.text;
		parameter[1] = _port.text;

		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnSaveSettingRequested, this, parameter);
	}


}
