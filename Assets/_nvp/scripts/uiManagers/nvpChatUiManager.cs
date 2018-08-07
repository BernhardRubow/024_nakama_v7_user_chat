using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using UnityEngine.UI;
using newvisionsproject.managers.events;


public class nvpChatUiManager : MonoBehaviour
{

    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField] private InputField _roomName;
    [SerializeField] private InputField _message;
    [SerializeField] private Text _sessionText;
    [SerializeField] private Text _statusText;
    [SerializeField] private Text _chatUsers;
    [SerializeField] private Text _chatText;

    private List<System.Action> _deferedActions;
    private nvpChatNetworkManager _chatScript;

    private Dictionary<string, string> _userDisplayNames = new Dictionary<string, string>();


    // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
        _deferedActions = new List<System.Action>();

        _chatScript = this.GetComponent<nvpChatNetworkManager>();
        _chatScript.OnStatusChanged += OnStatusChanged;
        _chatScript.OnMessageReceived += OnMessageReceived;
        _chatScript.OnChannelPresencesChanged += OnChannelPresencesChanged;
    }

    void Update()
    {
        if(_deferedActions.Count > 0){
            foreach(var action in _deferedActions) action();
            _deferedActions.Clear();
        }
    }

    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnStatusChanged(object s, object e)
    {
            _deferedActions.Add(() => _statusText.text = e.ToString() + "\n" + _statusText.text);            
    }

    void OnMessageReceived(object s, object e)
    {        
            var msg = (IApiChannelMessage)e;
            ChatMessage chatMessage = msg.Content.FromJson<ChatMessage>();
            _deferedActions.Add(() => _chatText.text = string.Format("{0} says: {1}\n{2}", chatMessage.UserName, chatMessage.Message, _chatText.text));
    }

    void OnChannelPresencesChanged(object s, object e){
        var connectedUsers = (List<IApiUser>)e;
        var userList = "";
        
        foreach(var user in connectedUsers){
            userList += user.DisplayName + "\n";
        }

        _deferedActions.Add(() => _chatUsers.text = userList);
    }


    // +++ ui event handler +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void OnJoinRoom()
    {
        _chatScript.JoinRoom(_roomName.text);
    }


    public void OnSend()
    {
        _chatScript.Send(_message.text);
    }
}
