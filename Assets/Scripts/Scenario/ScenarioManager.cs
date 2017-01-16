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
	private static Vector3[] pos1 = new Vector3[]{
	   new Vector3(0,0,0),		// insert
	   new Vector3(0,0,0)		// remove
	   };
	private static Vector3[] pos2 = new Vector3[]{
	   new Vector3(0,0,0),		// insert
	   new Vector3(0,0,0)		// remove
	   };
	private static Vector3[] pos3 = new Vector3[]{
	   new Vector3(0,0,0),		// insert
	   new Vector3(0,0,0)		// remove
	   };
	private static Vector3[] pos4 = new Vector3[]{
	   new Vector3(0,0,0),		// insert
	   new Vector3(0,0,0)		// remove
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
    public void UpdateLines(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Scenario/" + fileName + ".txt");
        string scenarioText = File.ReadAllText(filePath);
        if (scenarioText == null)
        {
            Debug.LogError("Scenario file not found");
            Debug.LogError("ScenarioManger not active");
            enabled = false;
            return;
        }
        string num = "0";
        string[] scenarios = scenarioText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_scenarios = getNowScenario(scenarios, num);
        m_currentLine = 0;
        isScenario = true;
        ItweenMoveTo(hukidasi, new Vector3(0, -300, 0), 0.5f, "easeInOutBack", "RequestNextLine", this.gameObject);
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
        var text = string.Empty;
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

                }
                lineBulider.AppendLine(text);
            }
        }
        return lineBulider.ToString();
    }
    private List<string> getNowScenario(string[] scenarios, string num)
    {
        List<string> nowScenario = new List<string>();
        for (int i = 0; i < scenarios.Length; i++)
        {
            string[] lines = scenarios[i].Split();
            if (lines[0] == "@" + num)
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
    /// Get the image number.
    /// </summary>
    /// <returns>The image number.</returns>
    /// <param name="line">Line.</param>
    private string GetImageNum(string line)
    {
        var tag = Regex.Match(line, "@(\\S+)");
        return tag.Groups[1].ToString();
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
        if (method != "")
        {
            moveHash.Add("oncomplete", method);
            moveHash.Add("oncompletetarget", obj);
        }
        iTween.MoveTo(target, moveHash);
    }
	public void ChangeImage(int num, string img_name)
	{
		string path = "Talk_Image/" + img_name + ".png";
		img_list[num-1].sprite = Resources.Load<Sprite>(path);
	}
	public void RemoveImage(int num)
	{
		ItweenMoveTo(img_list[num-1].gameObject,pos_list[num-1][1],1);
	}
	public void InsertImage(int num, string img_name)
	{
		ItweenMoveTo(img_list[num - 1].gameObject, pos_list[num - 1][0], 1);
	}
	public IEnumerator SwayImage(int num)
	{
		float time = Time.time;
		while (true) { 
			if(time /Time.time >= 1)
				yield break;
			float x = UnityEngine.Random.Range(0, 10);
			float y = UnityEngine.Random.Range(0, 10);
			Vector3 move_pos = pos_list[num-1][0];
			yield return new WaitForSeconds(0.1f);
		}
	}
}