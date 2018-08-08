using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using System;

public class nvpUserSettingNetworkManager : MonoBehaviour {

	// +++ delegates ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public delegate void UserSettingsDelegate(object sender, IApiAccount account);




	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private IClient _client;
    private ISocket _socket;
    private IChannel _channel;
    private List<IUserPresence> _connectedUsers;
    private ISession _session;
	private IApiAccount _account;
    private string _id;

	public event UserSettingsDelegate OnAccountLoaded = delegate {};




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	async void Start () {
		// Init List of connected users
        _connectedUsers = new List<IUserPresence>(0);

        // get the nakama client
        _client = new Client("defaultkey", nvpGameManager.HOST, nvpGameManager.PORT, false);

        // use random guid as device id (for local testing)
        _id = System.Guid.NewGuid().ToString();

        // request a authenticated session from the server and 
        // authenticate the user with either the random device id
        // or email/password
        if(nvpGameManager.UNIQUEID != string.Empty)
        {
            _session = await _client.AuthenticateDeviceAsync(nvpGameManager.UNIQUEID);        
        }
        else 
        {
            _session = await _client.AuthenticateEmailAsync(nvpGameManager.EMAIL, nvpGameManager.PASSWORD);
        }

		_account = await _client.GetAccountAsync(_session);

		this.OnAccountLoaded(this, _account);
	}




    // +++ class methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    async internal void SaveName(string name)
    {
        await _client.UpdateAccountAsync(_session, name, name);
    }
}
