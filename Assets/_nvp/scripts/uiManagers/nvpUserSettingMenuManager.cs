using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using newvisionsproject.managers.events;

public class nvpUserSettingMenuManager : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private InputField _userName;
	[SerializeField] private Text _lbUsername;

	private nvpUserSettingNetworkManager _networkManager;

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		_networkManager = this.GetComponent<nvpUserSettingNetworkManager>();
		_networkManager.OnAccountLoaded += OnAccountLoaded;

		// Show authentication type in Usernamelabel
		if(nvpGameManager.UNIQUEID != string.Empty){
			_lbUsername.text = string.Format("Username by Id ({0})", nvpGameManager.UNIQUEID);
		}
		else {
			_lbUsername.text = string.Format("Username by Email ({0})", nvpGameManager.EMAIL);
		}
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
