using UnityEngine;
using System.Collections;

public class TutorialRoot : SingletonMonoBehaviour<TutorialRoot>
{
    public int counter = 1;
    public string msg = "";
    private bool onLesson = false;
	// Use this for initialization
	void Start () {
        /* 
        TODO: stop monster AI
        */
        foreach(var enemy in PlayerRoot.Instance.enemyList)
        {
            EnemyBase eb = enemy.GetComponent<EnemyBase>();
            IEnumerator coroutine = eb.Loading(5.0f);
            eb.StopCoroutine(coroutine);
        }
	}
    void OnGUI()
    {
        if (msg != "")
        {
            GUI.Label (new Rect (10, 90, 200, 20), msg);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!FadeManager.Instance.onTalk && !FadeManager.Instance.isFading)
        {
            switch (counter)
            {
                case 1: //作戦闘開始
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "T1";
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
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "T2";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 4:
                    if (!onLesson)
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(KiraControl.Instance);
                    }
                    PlayerRoot.Instance.controller.Excute(PlayerRoot.Instance);
                    break;
                case 5:
                    onLesson = false;
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "T3";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 6:
                    if (!onLesson)
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(KirenControl.Instance);
                    }
                    PlayerRoot.Instance.controller.Excute(PlayerRoot.Instance);
                    break;
                case 7:
                    if (onLesson)
                    {
                        onLesson = false;
                        EnemyBase eb = PlayerRoot.Instance.enemyList[0].GetComponent<EnemyBase>();
                        eb.controller = M_Tutorial.Instance;
                        eb.controller.Excute(eb);
                    }
                    break;
                case 8:
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "T4";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 9:
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(Kiren2Control.Instance);
                    }
                    PlayerRoot.Instance.controller.Excute(PlayerRoot.Instance);
                    break;
                case 10:
                    onLesson = false;
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "T5";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                default :
                    if (!onLesson)
                    {
                        onLesson = true;
                        EnemyBase eb = PlayerRoot.Instance.enemyList[0].GetComponent<EnemyBase>();
                        eb.controller = M_Normal.Instance;
                        foreach (var enemy in PlayerRoot.Instance.enemyList)
                        {
                            EnemyBase e_eb = enemy.GetComponent<EnemyBase>();
                            e_eb.StartCoroutine(e_eb.Loading(5.0f));
                        }
                    }
                    break;
            }
        }
	}
}
