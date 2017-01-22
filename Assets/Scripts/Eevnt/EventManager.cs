using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EventJson
{
    public int all_event_id;
    public int e_id;
    public string e_name;
    public string target_type;
    public string target_name;
    public int target_num;
    public int count;
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
    /// <summary>
    /// Read event file
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>Json String</returns>
    public string ReadFile(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Event/" + fileName + ".json");
        //json fileを読み込む
        string JsonString = File.ReadAllText(filePath);
        return JsonString;
    }
    /// <summary>
    /// Ge tEvent
    /// </summary>
    /// <param name="JsonString">Json string</param>
    /// <param name="id">event id</param>
    /// <returns>EventJson</returns>
    public EventJson GetEvent(string JsonString, int id = 0)
    {
        EventCollect ec = JsonUtility.FromJson<EventCollect>(JsonString);
        foreach(var ej in ec.events){
            if (id != 0)
            {
                if (id == ej.e_id)
                    return ej; 
            }
            else if(!ej.is_finish)
                return ej;
        }
        return null;
    }
}
