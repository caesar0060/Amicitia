using UnityEngine;
using System.Collections;

public class TutorialContoller : SingletonMonoBehaviour<TutorialContoller>
{
    public int counter = 0;
	// Use this for initialization
	void Start () {
        PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
        ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
        counter++;
	}
	
	// Update is called once per frame
	void Update () {
        if (!FadeManager.Instance.onTalk)
        {
            switch (counter)
            {
                case 1:
                    PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                    ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                    counter++;
                    break;
                case 2:
                    if (GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Count == 0)
                    {
                        PlayerRoot.Instance.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                        ScenarioManager.Instance.UpdateLines(this.GetComponent<ScenarioScript>());
                        counter++;
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
	}
}
