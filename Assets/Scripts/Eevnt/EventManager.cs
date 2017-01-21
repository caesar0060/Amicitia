using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EventJson
{
    public int e_id;
    public string e_name;
    public string target_type;
    public string target_name;
    public int target_num;
    public string prefab_name;
    public float target_pos_X;
    public float target_pos_Y;
    public float target_pos_Z;
    public int e_present;
    public bool is_finish;
}

[Serializable]
public class EventCollect
{
    public EventJson[] events;
}

public class EventManager : SingletonMonoBehaviour<EventManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public EventJson ReadFile(string fileName)
    {
        EventJson result;
        //
        return result;
    }
}
