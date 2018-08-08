using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvpGameManager : MonoBehaviour {
	// +++ Fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public static string HOST;
	public static int PORT;
	public static string UNIQUEID;
	public static string EMAIL;
	public static string PASSWORD;
	

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		
		SubscribeToEvents();

		InitiaizeGame();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		UnsubscribeFromEvents();
	}

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnSaveSettingsRequested(object s, object e){
		string[] userSettings = (string[])e;
		nvpGameManager.HOST = userSettings[0];
		nvpGameManager.PORT = System.Convert.ToInt32(userSettings[1]);
		nvpGameManager.UNIQUEID = userSettings[2];

		if(userSettings[3] != string.Empty){
			nvpGameManager.UNIQUEID = string.Empty;
			nvpGameManager.EMAIL = userSettings[3];
			nvpGameManager.PASSWORD = userSettings[4];
		}
	}
	
	void InitiaizeGame(){

		nvpGameManager.HOST = "192.168.160.151";
		nvpGameManager.PORT = 7350;
		nvpGameManager.UNIQUEID = System.Guid.NewGuid().ToString();

		// at end of initialization throw event
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnGameInitialized,this, null);
	}

	void SubscribeToEvents(){
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnSaveSettingRequested, OnSaveSettingsRequested);
	}

	void UnsubscribeFromEvents(){
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnSaveSettingRequested, OnSaveSettingsRequested);
	}
}
