using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ScenarioScript : MonoBehaviour {
	public string fileName = null;	    //テキストファイル
	public Sprite _image = null;		//UI用イメージ
    public string event_file = null;    //event file name
    public bool autoDestory = false;    // Destroy when talk finish
    public List<GameObject> enemy_list = new List<GameObject>();     // Enemy Prefabs
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        GameObject other_go = other.gameObject;
        if (other_go.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
            ScenarioManager.Instance.UpdateLines(this);
        }
    }
}
