using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using newvisionsproject.managers.events;

public class nvpSceneManager : MonoBehaviour {

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		
		// subscribe to events
		this.SubscribeToEvents();
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnGameInitialized(object s, object e){
		LoadMainMenu();
	}

	void OnDestroy()
	{
		this.UnsubscribeFromEvents();
	}

	void OnStartChatWithDeviceIdRequested(object s, object e){
		SceneManager.UnloadSceneAsync("menuMain");
		SceneManager.LoadScene("ChatWithDeviceId", LoadSceneMode.Additive);
	}

	void OnSaveSettingRequested(object s, object e){
		SceneManager.UnloadSceneAsync("menuSettings");
		SceneManager.LoadScene("menuMain", LoadSceneMode.Additive);
	}

	void OnOpenSettingsMenuRequested(object s, object e){
		SceneManager.UnloadSceneAsync("menuMain");
		SceneManager.LoadScene("menuSettings", LoadSceneMode.Additive);
	}

	void OnConnectToServerRequested(object s, object e){
		SceneManager.UnloadSceneAsync("menuSettings");	
	}




	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void LoadMainMenu(){
		SceneManager.LoadScene("menuMain",LoadSceneMode.Additive);
	}

	void SubscribeToEvents(){
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnGameInitialized, OnGameInitialized);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnOpenSettingsMenuRequested, OnOpenSettingsMenuRequested);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnSaveSettingRequested, OnSaveSettingRequested);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnStartChatWithDeviceIdRequested, OnStartChatWithDeviceIdRequested);
	}

	void UnsubscribeFromEvents(){
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnGameInitialized, OnGameInitialized);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnOpenSettingsMenuRequested, OnOpenSettingsMenuRequested);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnSaveSettingRequested, OnSaveSettingRequested);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnStartChatWithDeviceIdRequested, OnStartChatWithDeviceIdRequested);
	}
}
