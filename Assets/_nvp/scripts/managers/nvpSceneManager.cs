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

	void OnServerSettingsSaved(object s, object e){
		SceneManager.UnloadSceneAsync("menuSettings");
		SceneManager.LoadScene("menuMain", LoadSceneMode.Additive);
	}

	void OnUserSettingsSaved(object s, object e){
		SceneManager.UnloadSceneAsync("menuUserSettings");
		SceneManager.LoadScene("menuMain", LoadSceneMode.Additive);
	}

	void OnConnectToServerRequested(object s, object e){
		SceneManager.UnloadSceneAsync("menuSettings");	
	}

	void OpenServerSettingsScene(object s, object e){
		SceneManager.UnloadSceneAsync("menuMain");
		SceneManager.LoadScene("menuSettings", LoadSceneMode.Additive);
	}

	void OpenChatWithUniqueIdScenen(object s, object e){
		SceneManager.UnloadSceneAsync("menuMain");
		SceneManager.LoadScene("ChatWithDeviceId", LoadSceneMode.Additive);
	}

	void OpenUserSettingScene(object s, object e){
		SceneManager.UnloadSceneAsync("menuMain");
		SceneManager.LoadScene("menuUserSettings", LoadSceneMode.Additive);
	}




	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void LoadMainMenu(){
		SceneManager.LoadScene("menuMain",LoadSceneMode.Additive);
	}

	void SubscribeToEvents(){
		// other events
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnGameInitialized, OnGameInitialized);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnSaveSettingRequested, OnServerSettingsSaved);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnUserSettingsSaved, OnUserSettingsSaved);

		// open scene request events
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnOpenServerSettingsSceneRequested, OpenServerSettingsScene);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnOpenChatWithUniqueIdSceneRequested, OpenChatWithUniqueIdScenen);
		nvpEventManager.INSTANCE.SubscribeToEvent(GameEvents.OnOpenUserSettingsSceneRequested, OpenUserSettingScene);
	}

	void UnsubscribeFromEvents(){
		// other events
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnGameInitialized, OnGameInitialized);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnSaveSettingRequested, OnServerSettingsSaved);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnUserSettingsSaved, OnUserSettingsSaved);

		// open scene request events
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnOpenServerSettingsSceneRequested, OpenServerSettingsScene);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnOpenChatWithUniqueIdSceneRequested, OpenChatWithUniqueIdScenen);
		nvpEventManager.INSTANCE.UnsubscribeFromEvent(GameEvents.OnOpenUserSettingsSceneRequested, OpenUserSettingScene);
	}
}
