﻿using UnityEngine;
using System.Collections;

public class Kongregate : MonoBehaviour {
	
	static Kongregate instance;
	
	void Start() {
		
		if(instance == null) {
			
			Application.ExternalEval("if(typeof(kongregateUnitySupport) != 'undefined'){kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');};");
			
			instance = this;
			
		}

		//Debug.LogWarning ("Connected: " + isKongregate);
	}
	
	
	static bool isKongregate = false;
	static uint userId = 0;
	static string username = "Guest";
	static string gameAuthToken = "";
	
	void OnKongregateAPILoaded(string userInfoString) {
		
		isKongregate = true;
		string[] parameters = new string[3];
		parameters = userInfoString.Split("|"[0]);
		userId = uint.Parse(parameters[0]);
		username = parameters[1];
		gameAuthToken = parameters[2];
		
	}
	
	public static void SubmitStatistic(string stat, int val) {
		
		if(isKongregate) {
			
			Application.ExternalCall("kongregate.stats.submit",stat,val);
			
		}
	}
}