using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    private bool isFading = false;
    public void start()
    {
        //ここで黒テクスチャ作る
		//this.blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        //this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
        //this.blackTexture.SetPixel(0, 0, Color.white);
        //this.blackTexture.Apply();
    }

    public void OnGUI()
    {
        if (!this.isFading)
            return;

        //透明度を更新して黒テクスチャを描画
        GUI.color = new Color(0, 0, 0, this.fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadLevel(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }


    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
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
        Application.LoadLevel(scene);

        //だんだん明るく
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}


   





    //public IEnumerator FadeIn()
    //{
    //    float startTime = Time.time;
    //    float nowTime = 0;
    //    while (true)
    //    {
    //        nowTime = (Time.time - startTime) / timer;
    //        if (nowTime >= 1)
    //        {
    //            nowTime = 1;
    //            float rate = nowTime * 255;
    //            image.color = new Color(0, 0, 0, rate);
    //            yield break;
    //        }
    //        else
    //        {
    //            float rate = nowTime * 255;
    //            image.color = new Color(0, 0, 0, rate);
    //            yield return new WaitForEndOfFrame();
    //        }
    //    }
    //}


    //public IEnumerator FeadeOut()
    //{
    //    float startTime = Time.time;
    //    float nowTime = 0;
    //    while (true)
    //    {
    //        nowTime = (Time.time - startTime) / timer;
    //        if (nowTime >= 1)
    //        {
    //            nowTime = 1;
    //            float rate = nowTime * 1;
    //            image.color = new Color(0, 0, 0, rate);
    //            yield break;
    //        }
    //        else
    //        {
    //            float rate = nowTime * 1;
    //            image.color = new Color(0, 0, 0, rate);
    //            yield return new WaitForEndOfFrame();

    //        }




    //    }

    //}


    //public float speed = 0.01f;
    //float alfa;
    //float red, green, blue;

    //void Start() {
    //    red = GetComponent<Image>().color.r;
    //    green = GetComponent<Image>().color.g;
    //    blue = GetComponent<Image>().color.b;
    //}

    //void Update(){
    //    GetComponent<Image>().color = new Color(red, green, blue, alfa);
    //    alfa += speed;
    //   }
