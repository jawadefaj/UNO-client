using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using AssemblyCSharp;
using SimpleJSON;
using System;

public class GameControl : MonoBehaviour {

	public GameObject Red;
	public GameObject Green;
	public string givenCard;
	public InputField inputGivenCard;
	private string usercards;
	public static Boolean showUpdateCards;


	void awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (AppWarpManager.IsMyTurn) {
			Green.SetActive (true);
			Red.SetActive (false);
		}
		else
		{
			Green.SetActive(false);
			Red.SetActive(true);
		}
	}

	public void showGivenCard(string inputFieldtext)
	{
		givenCard = inputFieldtext;
	}

	public void OnClickDrawCard()
	{
		WarpClient.GetInstance ().SendChat ("DC");
	}

	public void OnClickChallenge()
	{
		WarpClient.GetInstance().SendChat("CH");
	}

	public void OnClickSend()
	{

		if (AppWarpManager.getCards.Contains (givenCard)) {
			Debug.Log ("Card exist at hand");
			AppWarpManager.getCards.Remove (givenCard);
			//AppWarpManager.disCardPile = givenCard; 
			WarpClient.GetInstance ().sendMove(givenCard);
			WarpClient.GetInstance().SendChat("PM" + givenCard);
		}
		else 
		{
			Debug.Log("Card doesnt exist in hand");
		}
		inputGivenCard.text = "";


	}

	void OnGUI()
	{
		string TempCard ="";
		for (int i=0; i< AppWarpManager.getCards.Count; i++) 
		{
			TempCard = TempCard+ AppWarpManager.getCards[i] as String;
			TempCard = TempCard + "#";
		}
		GUI.TextArea(new Rect(((Screen.width/2)-150),((Screen.height/2)+80),300,50), "Here is your cards: " + TempCard);

		GUI.TextArea (new Rect (210, 100, 100, 50), "Discard Pile: " + AppWarpManager.disCardPile);
		GUI.TextArea (new Rect (320, 100, 180, 50), "Possible Moves: " + AppWarpManager.possibleMoves);

	}
}
