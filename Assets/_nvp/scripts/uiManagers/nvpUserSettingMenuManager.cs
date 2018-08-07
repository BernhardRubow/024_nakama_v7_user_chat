using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using newvisionsproject.managers.events;

public class nvpUserSettingMenuManager : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private InputField _userName;

	private nvpUserSettingNetworkManager _networkManager;

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		_networkManager = this.GetComponent<nvpUserSettingNetworkManager>();
		_networkManager.OnAccountLoaded += OnAccountLoaded;
	}

	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnAccountLoaded(object s, IApiAccount account){
		_userName.text = account.User.DisplayName;
		
	}

	// +++ ui event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void OnSaveClicked(){
		string name = _userName.text;
		_networkManager.SaveName(name);
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnUserSettingsSaved, this, null);
	}
}
