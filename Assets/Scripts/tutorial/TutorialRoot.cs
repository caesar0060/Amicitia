using UnityEngine;
using System.Collections;

public class TutorialRoot : SingletonMonoBehaviour<TutorialRoot>
{
    public int counter = 0;
    public string msg = "";
    private bool onLesson = false;
	// Use this for initialization
	void Start () {
        /* 
        TODO: stop monster AI
        */
        PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
        // ScenarioScript.filename = (text name);
        ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
        counter++;
	}
    void OnGUI()
    {
        if (msg != "")
        {
            //GUI.Label (new Rect (10, 90, 200, 20), "Root: " + controller.ToString ());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!FadeManager.Instance.onTalk)
        {
            switch (counter)
            {
                case 1: //作戦闘開始
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    // ScenarioScript.filename = (text name);
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 2: //プレイヤーの操作
                    if (!onLesson)
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(YumaControl.Instance);
                    }
                    PlayerRoot.Instance.controller.Excute(PlayerRoot.Instance);
                    break;
                case 3:
                    onLesson = false;
                    break;
                case 4:
                    break;
            }
        }
	}
}
