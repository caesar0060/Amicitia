using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EventQuest
{

}

[Serializable]
public class EventCollect
{
    public EventQuest[] events;
}

public class EventManager : SingletonMonoBehaviour<EventManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
