using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using AssemblyCSharp;
using SimpleJSON;
using System.Collections.Generic;
using System;


public class AppWarpManager : MonoBehaviour {

	public static AppWarpManager instance;

	//settings for japan server
	public static string apiKey = "986942536a675073b6b9167fad956381f60e64eab4da283b8ab641a9995184f8";
	public static string secretKey = "4d53e2ff468eec301b3e51ddcb3b45f77b4b649d82591f30f33c4e187e032a4e";
	
	//settings for local AppWarpS2 Server
	//public static string apiKey_s2 ="d3d1f421-98b4-4202-a";
	//public static string address="127.0.0.1";
	//public static int port= 12346;

	//settings for EC@ AppWarpS2 Server
	public static string apiKey_s2 ="653ee994-070b-4501-8";
	public static string address="127.0.0.1";
	public static int port= 12346;

	public bool isConnected=false;
	internal int responseToGet=0;
	internal int frndsInfoToCollect=0;
	AppWarpListener listen ;
	public string UserName= "Jawad";
	public static Boolean showCards;
	public static ArrayList getCards;
	public static bool IsMyTurn;
	public static bool showPM = false;

	public static string disCardPile;
	public static string possibleMoves;

	
	//saved commands
	internal List<string> savedMsgd = new List<string>();

	//switch
	private bool canReset=true;


	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if(instance==null)
			AppWarpManager.instance=this;
		else
			Destroy (this);
	}

	void Start () {

		listen  = new AppWarpListener();
		getCards = new ArrayList ();


		//bind all events

		//to connect with usa server
		//WarpClient.initialize(apiKey_us,secretKey_us);

		//to connect with Japan server
		//WarpClient.initialize(apiKey,secretKey);

		//to connect to appwarps2 server on my pc
		WarpClient.initialize (apiKey_s2,address,port);
		//WarpClient.initialize (apiKey, secretKey);

		WarpClient.setRecoveryAllowance (0); //max recovery time set
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddLobbyRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		WarpClient.GetInstance().AddUpdateRequestListener(listen);
		WarpClient.GetInstance().AddZoneRequestListener(listen);

		//connect
		UserName = UserName + UnityEngine.Random.Range (1, 1000).ToString();
		//WarpClient.GetInstance ().Connect (UserName, "");

	

	}

	public void Print(string msg)
	{
		Debug.Log (msg);
	}

	void Update()
	{
		WarpClient.GetInstance ().Update ();
		//WarpClient.GetInstance ().SendChat ("this is from me");


	}



	void OnGUI()
	{
		if(showCards)
		{
			//GUI.TextField(new Rect(100,100,50,200), "Here is your cards: " + getCards);

		}
		if(Debug.isDebugBuild)
		{
			GUI.contentColor = Color.white ;
			GUI.skin.label.fontSize=20;
			GUI.Label(new Rect(10,10,500,300), listen.getDebug ());
			GUI.Label(new Rect(350,10,500,300), "Connection Status "+ WarpClient.GetInstance ().GetConnectionState());
			//string text = "<b>Hello</b><color=red>World</color>";
			//GUI.Label (new Rect(20,20,100,100),text);
		}
		if (isConnected) 
		{
			if(GUI.Button(new Rect(500,200,70,30),"join room"))
			{
				WarpClient.GetInstance().JoinRoomInRange(0,1,true);
			}
		}

	}

	byte[] GetBytes(string str)
	{
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}

}
