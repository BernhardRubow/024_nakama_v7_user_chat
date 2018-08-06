using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvpGameManager : MonoBehaviour {

	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		
		InitiaizeGame();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	void InitiaizeGame(){


		// at end of initialization throw event
		nvpEventManager.INSTANCE.InvokeEvent(GameEvents.OnGameInitialized,this, null);
	}
}
