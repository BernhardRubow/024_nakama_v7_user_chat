using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;

public class nvpSettingsMenuManager : MonoBehaviour {

	[SerializeField] private InputField _host;
	[SerializeField] private InputField _port;
	[SerializeField] private InputField _uniqueId;
	[SerializeField] private InputField _email;
	[SerializeField] private InputField _password;

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		_host.text = nvpGameManager.HOST;
		_port.text = nvpGameManager.PORT.ToString();
		_uniqueId.text = nvpGameManager.UNIQUEID;

	}

	void Update () {
		
	}

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void OnSaveClicked(){
		Debug.Log("OnSaveClicked called");
		string[] userSettings = new string[5];
		userSettings[0] = _host.text;
		userSettings[1] = _port.text;
		userSettings[2] = _uniqueId.text;
		userSettings[3] = "";
		userSettings[4] = "";

		if(_email.text != string.Empty){
			userSettings[3] = _email.text;
			userSettings[4] = _password.text;
			userSettings[2] = string.Empty;
		}

		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnSaveSettingRequested, this, userSettings);
	}


}
