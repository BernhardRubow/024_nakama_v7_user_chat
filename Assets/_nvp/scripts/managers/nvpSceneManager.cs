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
	}

	void UnsubscribeFromEvents(){
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnGameInitialized, OnGameInitialized);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnOpenSettingsMenuRequested, OnOpenSettingsMenuRequested);
	}
}
