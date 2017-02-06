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
    public int target_count;
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
    /// Get Event
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
                if (id == ej.all_event_id)
                    return ej; 
            }
            else if(!ej.is_finish)
                return ej;
        }
        return null;
    }
    /// <summary>
    /// Get All Event
    /// </summary>
    /// <param name="JsonString">Json String</param>
    /// <returns>Even tCollect</returns>
    public EventCollect GetAllEvent(string JsonString)
    {
        EventCollect ec = JsonUtility.FromJson<EventCollect>(JsonString);
        return ec;
    }
    /// <summary>
    /// Get Event key
    /// </summary>
    /// <param name="ec">EventCollect</param>
    /// <param name="id">key list</param>
    /// <returns>event key</returns>
    public string GetEventKey(EventCollect ec)
    {
        string key = "0";
        var event_list = PlayerRoot.Instance.evnet_list;
         //
        for(int i = 0; i < ec.events.Length; i++){
            foreach (var e in event_list)
            {
                if (e.all_event_id == ec.events[i].all_event_id)
                {
                    if (e.is_finish)
                    {
                        if (i < ec.events.Length - 1)
                            key = ec.events[i + 1].all_event_id.ToString();
                    }
                    else
                    {
                        if (e.target_num == e.target_count)
                            key = "complete" + ec.events[i].all_event_id.ToString();
                        else
                            key = ec.events[i].all_event_id.ToString();
                        return key;
                    }
                }
            }
        }
        return key;
    }
}
