using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialRoot : SingletonMonoBehaviour<TutorialRoot>
{
    public int counter = 1;
    public string msg = "";
    private bool onLesson = false;
    public GameObject hukidasi_prefab;
    private GameObject hukidasi;
    private Vector3 h_pos = new Vector3(0, 350, 0);
	// Use this for initialization
	void Start () {
        hukidasi = Instantiate(hukidasi_prefab, h_pos, Quaternion.identity) as GameObject;
        hukidasi.transform.SetParent(GameObject.FindGameObjectWithTag("ScenarioCanvas").transform);
        hukidasi.transform.localPosition = h_pos;
        hukidasi.transform.localRotation = Quaternion.Euler(Vector3.zero);
        hukidasi.transform.localScale = new Vector3(1, 1, 1);
	}
    void OnGUI()
    {
        if (msg != "")
        {
            hukidasi.SetActive(true);
            hukidasi.GetComponentInChildren<Text>().text = msg;
        }
        else
        {
            hukidasi.SetActive(false);
            hukidasi.GetComponentInChildren<Text>().text = "";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!FadeManager.Instance.onTalk && !FadeManager.Instance.isFading)
        {
            switch (counter)
            {
                case 1: //作戦闘開始
                    foreach (var enemy in PlayerRoot.Instance.enemyList)
                    {
                        EnemyBase eb = enemy.GetComponent<EnemyBase>();
                        eb.StopCoroutine(eb.coroutine);
                    }
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "Tutorial/T1";
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
                    this.GetComponent<ScenarioScript>().fileName = "Tutorial/T2";
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
                    this.GetComponent<ScenarioScript>().fileName = "Tutorial/T3";
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
                        PlayerRoot.Instance.ChangeMode(T_Wait.Instance);
                        EnemyBase eb = PlayerRoot.Instance.enemyList[0].GetComponent<EnemyBase>();
                        eb.controller = M_Tutorial.Instance;
                        eb.controller.Excute(eb);
                    }
                    break;
                case 8:
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "Tutorial/T4";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 9:
                    if (!onLesson)
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(Kiren2Control.Instance);
                    }
                    PlayerRoot.Instance.controller.Excute(PlayerRoot.Instance);
                    break;
                case 10:
                    onLesson = false;
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    this.GetComponent<ScenarioScript>().fileName = "Tutorial/T5";
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                default :
                    if (!onLesson)
                    {
                        onLesson = true;
                        PlayerRoot.Instance.ChangeMode(BattelMode.Instance);
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
