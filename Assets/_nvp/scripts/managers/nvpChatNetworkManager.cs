﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;

public class nvpChatNetworkManager : MonoBehaviour {

	// +++ delegates ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public delegate void ChatEventDelegate(object sender, object eventArgs);


	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private IClient _client = new Client("defaultkey", "127.0.0.1", 7350, false);
    private ISocket _socket;
    private IChannel _channel;
    private List<IUserPresence> _connectedUsers;
    private ISession _session;
    private string _id;

	public event ChatEventDelegate OnStatusChanged = delegate {};
	public event ChatEventDelegate OnMessageReceived = delegate {};




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	async void Start () {
		// Init List of connected users
        _connectedUsers = new List<IUserPresence>(0);

        // get the nakama client
        _client = new Client("defaultkey", nvpGameManager.HOST, nvpGameManager.PORT, false);

        // use random guid as device id (for local testing)
        _id = System.Guid.NewGuid().ToString();

        // request a authenticated session from the server and 
        // authenticate the user with the random device id
        _session = await _client.AuthenticateDeviceAsync(_id);
        

        // Creat communcation socket
        _socket = _client.CreateWebSocket();

        // Subscribe to socket events
        _socket.OnChannelPresence += OnChannelPresence;
        _socket.OnChannelMessage += OnChannelMessage;
        _socket.OnConnect += OnConnect;
        _socket.OnDisconnect += OnDisconnect; 

        // connect to server using the socket
        await _socket.ConnectAsync(_session);
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    // +++ handler for nakama events ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnChannelPresence(object sender, IChannelPresenceEvent presenceChange)
    {
        _connectedUsers.AddRange(presenceChange.Joins);
        foreach (var leave in presenceChange.Leaves)
        {
            _connectedUsers.RemoveAll(item => item.SessionId.Equals(leave.SessionId));
        };

        // Print connected presences.
        var presences = string.Join(", ", _connectedUsers);
        Debug.LogFormat("Presence List\n {0}", presences);
    }

    void OnChannelMessage(object sender, IApiChannelMessage message)
    {
		Debug.Log ("Message received");
		this.OnStatusChanged(this, string.Format("Message receive {0:dd.MM.yy hh:mm:ss}", System.DateTime.Now));
		var content = message.Content;
		this.OnMessageReceived(this, message.ToString());
    }

    void OnConnect(object sender, System.EventArgs evt)
    {
        this.OnStatusChanged(this, "Socket Connected");
    }

    void OnDisconnect(object sender, System.EventArgs evt)
    {
		this.OnStatusChanged(this, "Socket disconnected.");
    }    
	
	private async void OnApplicationQuit()
    {
        if (_socket != null)
        {
            await _socket.DisconnectAsync(false);
        }
    }

	// +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public async void JoinRoom(string roomName)
    {
        this.OnStatusChanged(this, "OnJoinRoom called");
        _channel = await _socket.JoinChatAsync(roomName, ChannelType.Room);
		this.OnStatusChanged(this, string.Format("{0} joined", roomName));
        _connectedUsers.AddRange(_channel.Presences);
    }


    public async void Send(string msg)
    {
        this.OnStatusChanged(this, "OnSend called");
        string message = msg;
        var content = new Dictionary<string, string> {{_session.UserId, message}}.ToJson();
        await _socket.WriteChatMessageAsync(_channel, content);
    }
}
