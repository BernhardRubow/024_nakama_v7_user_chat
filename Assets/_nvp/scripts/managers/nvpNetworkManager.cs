using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using Nakama;

public class nvpNetworkManager : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private IClient _client;
	private ISession _session;
	private ISocket _socket;
	private List<IUserPresence> _connectedUsers = new List<IUserPresence>(0);


	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		SubscribeToEvents();
	}
	
	void Update () {
		
	}



	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnDestroy()
	{
		UnsubcribeFromEvents();
	}


	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


	void SubscribeToEvents(){
	}

	void UnsubcribeFromEvents(){		
	}
}
