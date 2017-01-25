using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

[RequireComponent(typeof(TextControl))]
public class ScenarioManager : SingletonMonoBehaviour<ScenarioManager>
{
    #region Property
    private static Vector3[] pos1 = new Vector3[]{
	   new Vector3(-475,0,0),		// insert
	   new Vector3(-1075,0,0)		// remove
	   };
	private static Vector3[] pos2 = new Vector3[]{
	   new Vector3(-200,0,0),		// insert
	   new Vector3(-800,0,0)		// remove
	   };
	private static Vector3[] pos3 = new Vector3[]{
	   new Vector3(200,0,0),		// insert
	   new Vector3(800,0,0)		// remove
	   };
	private static Vector3[] pos4 = new Vector3[]{
	   new Vector3(475,0,0),		// insert
	   new Vector3(1075,0,0)		// remove
	   };
	private static Vector3[][] pos_list = new Vector3[][]{
	   pos1, pos2, pos3, pos4
	   };
    [HideInInspector]
    public bool isScenario = false;	//シナリオ中かどうかを判断
    [HideInInspector]
    public List<string> m_scenarios = new List<string>();	//シナリオを格納する
    [HideInInspector]
    public int m_currentLine = 0;
    [HideInInspector]
    public bool m_isCallPreload = false;
    [HideInInspector]
    public Dictionary<string, GameObject> p_imageList = new Dictionary<string, GameObject>();
	[HideInInspector] public GameObject hukidasi = null;
    [HideInInspector] public TextControl m_textControl;
	public Image[] img_list;	// 会話用の絵
    #endregion
    // Use this for initialization
    void Start()
    {
        if (m_textControl == null)
            m_textControl = this.GetComponent<TextControl>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (hukidasi == null)
                hukidasi = GameObject.FindGameObjectWithTag("TalkUI");
        }
        catch (NullReferenceException)
        {
            return;
        }

    }
    /// <summary>
    /// 次の行を読む
    /// </summary>
    public void RequestNextLine()
    {
        if (isScenario == true)
        {
            var currentText = m_scenarios[m_currentLine];
            m_textControl.SetNextLine(CommandProcess(currentText));
            m_currentLine++;
            m_isCallPreload = false;
        }
    }
    /// <summary>
    /// テキストファイルを読み込む
    /// </summary>
    /// <param name="fileName">fileName</param>
    public void UpdateLines(ScenarioScript ss)
    {
        if( ss.fileName != null){
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Scenario/" + ss.fileName + ".txt");
            string scenarioText = File.ReadAllText(filePath);
            if (scenarioText == null)
            {
                Debug.LogError("Scenario file not found");
                Debug.LogError("ScenarioManger not active");
                enabled = false;
                return;
            }
            string key = "0";
            if (ss.event_file != null)
            {
                string js = EventManager.Instance.ReadFile(ss.event_file);
                EventCollect ec = EventManager.Instance.GetAllEvent(js);
                key = EventManager.Instance.GetEventKey(ec);
            }
            string[] scenarios = scenarioText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
            m_scenarios = getNowScenario(scenarios, key);
            m_currentLine = 0;
            isScenario = true;
            ItweenMoveTo(hukidasi, new Vector3(0, -300, 0), 0.5f, "easeInOutBack", "RequestNextLine", this.gameObject);
        }
    }
    /// <summary>
    /// Lineにより、プロセスを執行する
    /// </summary>
    /// <param name="line">line</param>
    /// <returns>text</returns>
    private string CommandProcess(string line)
    {
        var lineReader = new StringReader(line);
        var lineBulider = new StringBuilder();
        string text = string.Empty;
        while ((text = lineReader.ReadLine()) != null)
        {
            var commentCharacterCount = text.IndexOf("/");
            if (commentCharacterCount != -1)
            {
                text = text.Substring(0, commentCharacterCount);
            }

            if (!string.IsNullOrEmpty(text))
            {
                if (text[0] == '#')
                {
				string[] words = text.Split ();
				switch(words[1])
				{
					case "ChangeImage":
					ChangeImage(int.Parse(words[2]), words[3]);
					break;
					case "RemoveImage":
					RemoveImage(int.Parse(words[2]));
					break;
					case "InsertImage":
					InsertImage(int.Parse(words[2]), words[3]);
					break;
					case "SwayImage":
					StartCoroutine(SwayImage(int.Parse(words[2])));
					break;
				}
                }
                lineBulider.AppendLine(text);
            }
        }
        return lineBulider.ToString();
    }
    private List<string> getNowScenario(string[] scenarios, string key)
    {
        List<string> nowScenario = new List<string>();
        for (int i = 0; i < scenarios.Length; i++)
        {
            string[] lines = scenarios[i].Split();
            if (lines[0] == "@" + key)
            {
                int j = i + 1;
                while (true)
                {
                    lines = scenarios[j].Split();
                    if (lines[0] != "@end")
                        nowScenario.Add(lines[0]);
                    else
                        return nowScenario;
                    j++;
                }
            }
        }
        nowScenario.Add("Read Scenario error, haven't this event number");
        return nowScenario;
    }
    /// <summary>
    /// Starts the scenario.
    /// </summary>
    public void ItweenMoveBy(GameObject target, Vector3 pos, float time, string easeType, string method = "", GameObject obj = null)
    {
        var moveHash = new Hashtable();
        moveHash.Add("islocal", true);
        moveHash.Add("position", pos);
        moveHash.Add("time", time);
        moveHash.Add("easeType", easeType);
        if (method != "")
        {
            moveHash.Add("oncomplete", method);
            moveHash.Add("oncompletetarget", obj);
        }
        iTween.MoveBy(target, moveHash);
    }
    /// <summary>
    /// Starts the scenario.
    /// </summary>
	public void ItweenMoveTo(GameObject target, Vector3 pos, float time, string easeType = "linear", string method = "", GameObject obj = null)
    {
        var moveHash = new Hashtable();
        moveHash.Add("islocal", true);
        moveHash.Add("position", pos);
        moveHash.Add("time", time);
        moveHash.Add("easeType", easeType);
		moveHash.Add("ignoretimescale", true);
        if (method != "")
        {
            moveHash.Add("oncomplete", method);
            moveHash.Add("oncompletetarget", obj);
        }
        iTween.MoveTo(target, moveHash);
    }
	/// <summary>
	/// 画像を変更する
	/// </summary>
	/// <param name="num"></param>
	/// <param name="img_name"></param>
	public void ChangeImage(int num, string img_name)
	{
		string path = "Talk_Image/" + img_name + ".png";
		img_list[num-1].sprite = Resources.Load<Sprite>(path);
	}
	/// <summary>
	/// 画像を削除する
	/// </summary>
	/// <param name="num"></param>
	public void RemoveImage(int num)
	{
		ItweenMoveTo(img_list[num-1].gameObject, pos_list[num-1][1], 1);
	}
	/// <summary>
	/// 画像を追加する
	/// </summary>
	/// <param name="num"></param>
	/// <param name="img_name"></param>
	public void InsertImage(int num, string img_name)
	{
		ItweenMoveTo(img_list[num - 1].gameObject, pos_list[num - 1][0], 1);
	}
	/// <summary>
	/// 画像を揺れる
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	public IEnumerator SwayImage(int num)
	{
		float time = Time.unscaledTime;
		while (true) {
			if (time / Time.unscaledTime >= 1)
			{
				ItweenMoveTo(img_list[num - 1].gameObject, pos_list[num - 1][0], 0);
				yield break;
			}
			float x = UnityEngine.Random.Range(10, 11);
			float y = UnityEngine.Random.Range(10, 11);
			Vector3 pos = new Vector3(x,y,0);
			Vector3 move_pos = pos_list[num-1][0] + pos;
			ItweenMoveTo(img_list[num - 1].gameObject, move_pos, 0);
			yield return new WaitForSeconds(0.1f);
		}
	}
    /// <summary>
    /// Receive Event
    /// </summary>
    /// <param name="file_name">json file name</param>
    /// <param name="id">all event id</param>
    public void ReceiveEvent(string file_name, int id = 0)
    {
        string js = EventManager.Instance.ReadFile(file_name);
        EventJson ej = EventManager.Instance.GetEvent(js, id);
        PlayerRoot.Instance.evnet_list.Add(ej);
    }
    /// <summary>
    /// Complete Event
    /// </summary>
    /// <param name="file_name">json file name</param>
    /// <param name="id">all event id</param>
    public void CompleteEvent(string file_name, int id)
    {
        string js = EventManager.Instance.ReadFile(file_name);
        List<EventJson> event_list = PlayerRoot.Instance.evnet_list;
        foreach (var p_event in event_list)
        {
            if (p_event.all_event_id == id)
            {
                p_event.is_finish = true;
            }
        }
    }
}