using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    /// <summary>
    /// シーン遷移時のフェードイン・アウトを制御するためのクラス
    /// </summary>
public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    /// <summary>暗転用黒テクスチャ</summary>
    public Texture2D blackTexture;
    /// <summary>フェード中の透明度</summary>
    private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    public bool isFading = false;
	// 会話用
	[SerializeField]private Image blackImage = null;
	private Color color = Color.black;
    public bool onTalk = false;     // 会話中

    public void OnGUI()
    {
        if (!this.isFading)
            return;

        //透明度を更新して黒テクスチャを描画
        GUI.color = new Color(0, 0, 0, this.fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
    }
	void Update(){
		try{
			if (blackImage == null)
				blackImage = GameObject.Find ("BlackImage").GetComponentInChildren<Image>();
		}
		catch(NullReferenceException){
			return;
		}
	}

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    /// <param name="rc">変更したいモード</param>
    public void LoadLevel(string scene, float interval, RootController rc = null)
    {
        StartCoroutine(TransScene(scene, interval, rc));
    }
	/// <summary>
	/// シーン遷移用コルーチン
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	/// <param name="rc">変更したいRootController.</param>
	private IEnumerator TransScene(string scene, float interval, RootController rc = null )
    {
        // Rootのmodeを変更する
        //if (rc != null)
            //this.GetComponent<PlayerRoot>().ChangeMode(SceneChange.Instance);
        //だんだん暗く
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替
		SceneManager.LoadScene(scene);
        // Rootのmodeを変更する
        if(rc != null)
            this.GetComponent<PlayerRoot>().ChangeMode(rc);

        //だんだん明るく
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
	yield break;
    }
	/// <summary>
	/// 会話用のUIを出す
	/// </summary>
	/// <param name="interval">必要時間.</param>
	/// <param name="rc">変更したいRootController.</param>
	public IEnumerator ReadyTalkUI(float interval, RootController rc)
	{
        onTalk = true;
		Time.timeScale = 0;
		this.GetComponent<PlayerRoot>().ChangeMode(rc);
		float time = 0;
		while (time <= interval)
		{
			color.a = Mathf.Lerp(0f, 0.6f, (time / interval));
			blackImage.color = color;
			time += Time.unscaledDeltaTime;
			yield return 0;
		}
		yield break;
	}
	/// <summary>
	/// 会話用のUIを閉じる
	/// </summary>
	/// <param name="interval">必要時間.</param>
	/// <param name="rc">変更したいRootController.</param>
	public IEnumerator CloseTalkUI(float interval, RootController rc = null)
	{
        if( rc != null)
        {
          
                this.GetComponent<PlayerRoot>().ChangeMode(rc);
        }
		float time = 0;
        int count = ScenarioManager.Instance.img_list.Length;
        for (int i = 0; i < count; i++)
        {
            ScenarioManager.Instance.RemoveImage(i);
        }
		while (time <= interval)
		{
			color.a = Mathf.Lerp(0.6f, 0f, (time / interval));
			blackImage.color = color;
			time += Time.unscaledDeltaTime;
			yield return 0;
		}
		Time.timeScale = 1;
        if (ScenarioManager.Instance.battelEnemyList.Count > 0)
        {
            PlayerRoot.Instance.battelEnemyList = ScenarioManager.Instance.battelEnemyList;
            ScenarioManager.Instance.battelEnemyList = new List<GameObject>();
            FadeManager.Instance.LoadLevel("TutorialScene", 2, BattelStart.Instance);
            PlayerRoot.Instance.transform.position = this.transform.position;
        }

        onTalk = false;
		yield break;
	}
}