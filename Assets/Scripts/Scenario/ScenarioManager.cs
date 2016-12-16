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
	[HideInInspector] public bool isScenario = false;	//シナリオ中かどうかを判断
	[HideInInspector] public List<string> m_scenarios = new List<string>();	//シナリオを格納する
	[HideInInspector]  public int m_currentLine = 0;
	[HideInInspector] public bool m_isCallPreload = false;
	[HideInInspector] public Dictionary<string, GameObject> p_imageList = new Dictionary<string, GameObject> ();
	[SerializeField] private GameObject hukidasi = null;
	[HideInInspector] public TextControl m_textControl;

    // Use this for initialization
    void Start()
    {
			if(m_textControl == null)
        	m_textControl = this.GetComponent<TextControl>();
    }

    // Update is called once per frame
	void Update () {
		try{
			if (hukidasi == null)
				hukidasi = GameObject.FindGameObjectWithTag ("TalkUI");
		}
		catch(NullReferenceException){
			return;
		}
		
	}
    /// <summary>
	/// 次の行を読む
    /// </summary>
    public void RequestNextLine()
	{
		if (isScenario == true) {
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
		string scenarioText = File.ReadAllText (filePath);
        if (scenarioText == null)
        {
            Debug.LogError("Scenario file not found");
            Debug.LogError("ScenarioManger not active");
            enabled = false;
            return;
        }
		string num = "0";
        string[] scenarios = scenarioText.Split(new string[] { "\n"}, System.StringSplitOptions.None);
		m_scenarios = getNowScenario (scenarios, num);
        m_currentLine = 0;
		isScenario = true;
		StartScenario ();
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
                if (text[0] == '@')
                {
                   
                }
                lineBulider.AppendLine(text);
            }
        }
        return lineBulider.ToString();
    }
	private List<string> getNowScenario(string[] scenarios, string num){
		List<string> nowScenario = new List<string>();
		for (int i = 0; i < scenarios.Length; i++) {
			string[] lines = scenarios [i].Split ();
			if (lines [0] == "@" + num) {
				int j = i + 1;
				while (true) {
					lines = scenarios [j].Split ();
					if (lines [0] != "@end")
						nowScenario.Add(lines [0]);
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
    private void StartScenario()
    {
        iTween.MoveTo(hukidasi, iTween.Hash("y", -300,
            "islocal", true,
            "easeTupe", "easeOutExpo",
            "time", 0.5f,
            "oncomplete", "RequestNextLine",
            "oncompletetarget", this.gameObject
        ));
    }

    public void FinishScenario()
    {
        iTween.MoveTo(hukidasi, iTween.Hash("y", -600,
            "islocal", true,
            "easeTupe", "easeInExpo",
            "time", 0.5f
        ));
		hukidasi.GetComponentInChildren<Text> ().text = "";
    }
}