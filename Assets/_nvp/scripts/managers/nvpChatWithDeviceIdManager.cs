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
    [SerializeField] private Text _sessionText;
    [SerializeField] private Text _statusText;
    [SerializeField] private Text _chatUsers;
    [SerializeField] private Text _chatText;

    private List<System.Action> _deferedActions;
    private nvpChatNetworkManager _chatScript;




    // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private async void Start()
    {
        _deferedActions = new List<System.Action>();


        _chatScript = this.GetComponent<nvpChatNetworkManager>();
        _chatScript.OnStatusChanged += (s, e) => {
            _deferedActions.Add(() => _statusText.text = e.ToString() + "\n" + _statusText.text);            
        };
        _chatScript.OnMessageReceived += (s, e) => {
            var msg = (IApiChannelMessage)e;
            ChatMessage chatMessage = msg.Content.FromJson<ChatMessage>();
            _deferedActions.Add(() => _chatText.text = string.Format("{0} says: {1}\n{2}", chatMessage.UserName, chatMessage.Message, _chatText.text));
        };
    }

    void Update()
    {
        if(_deferedActions.Count > 0){
            foreach(var action in _deferedActions) action();
            _deferedActions.Clear();
        }
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




    // +++ unity event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private async void OnApplicationQuit()
    {
        
    }


    // +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


}
