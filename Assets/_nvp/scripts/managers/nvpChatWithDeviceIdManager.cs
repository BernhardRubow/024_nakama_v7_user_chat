using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using UnityEngine.UI;
using newvisionsproject.managers.events;


public class nvpChatWithDeviceIdManager : MonoBehaviour
{

    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField] private InputField _roomName;
    [SerializeField] private InputField _message;

    private IClient _client = new Client("defaultkey", "127.0.0.1", 7350, false);
    private ISocket _socket;
    private IChannel _channel;
    private List<IUserPresence> _connectedUsers;
    private ISession _session;
    private string _id;


    // Use this for initialization
    private async void Start()
    {
        _connectedUsers = new List<IUserPresence>(0);

        _client = new Client("defaultkey", nvpGameManager.HOST, nvpGameManager.PORT, false);

        _id = System.Guid.NewGuid().ToString();
        _session = await _client.AuthenticateDeviceAsync(_id);
        Debug.LogFormat("Session '{0}'", _session);

        _socket = _client.CreateWebSocket();

        _connectedUsers = new List<IUserPresence>(0);
        _socket.OnChannelPresence += (sender, presenceChange) =>
        {
            _connectedUsers.AddRange(presenceChange.Joins);
            foreach (var leave in presenceChange.Leaves)
            {
                _connectedUsers.RemoveAll(item => item.SessionId.Equals(leave.SessionId));
            };

            // Print connected presences.
            var presences = string.Join(", ", _connectedUsers);
            Debug.LogFormat("Presence List\n {0}", presences);
        };
        _socket.OnChannelMessage += (sender, message) =>
        {
            Debug.LogFormat("Received Message '{0}'", message);
        };
        _socket.OnConnect += (sender, evt) => Debug.Log("Socket connected.");
        _socket.OnDisconnect += (sender, evt) => Debug.Log("Socket disconnected.");

        await _socket.ConnectAsync(_session);
		
        Debug.Log("Nakama Init complete");
    }


    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public async void OnJoinRoom()
    {
		Debug.Log("OnJoinRoom called");
        var roomName = _roomName.text;
		
        _channel = await _socket.JoinChatAsync(roomName, ChannelType.Room);
        _connectedUsers.AddRange(_channel.Presences);


    }


    public async void OnSend()
    {
		Debug.Log("OnSend called");
        string message = _message.text;
        var content = new Dictionary<string, string> { { "hello", string.Format("{0}: {1}", _id, message) } }.ToJson();
        await _socket.WriteChatMessageAsync(_channel, content);
    }

    private async void OnApplicationQuit()
    {
        if (_socket != null)
        {
            await _socket.DisconnectAsync(false);
        }
    }

}
