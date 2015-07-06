using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

public class test : MonoBehaviour {

	public InputField inputField;
	public InputField Room;
	public static string username;
	public string roomname;
	// Use this for initialization

	void awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showUserName(string inputFieldtext)
	{
		username = inputFieldtext;
	}
	public void showRoomName(string Roomtext)
	{
		roomname = Roomtext;
	}

	public void OnClick()
	{
		WarpClient.GetInstance ().Connect (username, "");
		//WarpClient.GetInstance ().CreateTurnRoom (roomname, username, 4, null, 10);
		Debug.Log (username);
		Debug.Log (roomname);
	}

	public void OnClickStartGame()
	{
		Application.LoadLevel(1);
	}
}
