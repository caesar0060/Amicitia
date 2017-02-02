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
    public bool use_for_complete = false;   // 任務完成用かどうか
    public int event_id;
    public List<GameObject> enemy_list = new List<GameObject>();     // Enemy Prefabs
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerStay(Collider other)
    {
        if (this.gameObject.layer != LayerMask.NameToLayer("NPC"))
        {
            GameObject other_go = other.gameObject;
            if (other_go.layer == LayerMask.NameToLayer("Player") && !FadeManager.Instance.isFading && !FadeManager.Instance.onTalk)
            {
                JobBase jb = other_go.GetComponent<JobBase>();
                if (jb.battelStatus == BattelStatus.NOT_IN_BATTEL)
                {
                    if (!use_for_complete)
                    {
                        PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                        ScenarioManager.Instance.UpdateLines(this);
                    }
                    else
                    {
                        var e_list = PlayerRoot.Instance.evnet_list;
                        foreach (var e in e_list)
                        {
                            if (e.all_event_id == event_id && e.target_num == e.target_count)
                            {
                                PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                                ScenarioManager.Instance.UpdateLines(this);
                            }
                        }
                    }
                }
            }
        }
    }
}
