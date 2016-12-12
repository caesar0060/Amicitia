using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

[RequireComponent(typeof(TextControl))]
public class ScenarioManager : MonoBehaviour
{

	public string LoadFileName;

	private string[] m_scenarios;	//シナリオを鯉�{する
	private int m_currentLine = 0;
	private bool m_isCallPreload = false;

	private TextControl m_textControl;
	//private CommandController m_commandController;
	// Use this for initialization
	void Start()
	{
		m_textControl = this.GetComponent<TextControl>();
		UpdateLines(LoadFileName);
	}

	// Update is called once per frame
	void Update()
	{
		//すべて燕幣したら
		if (m_textControl.IsCompleteDisplayText)
		{
			//まだ肝の佩があったら
			if (m_currentLine < m_scenarios.Length)
			{
				//肝の佩を�iむ
				if (!m_isCallPreload)
				{
					m_isCallPreload = true;
				}
				if (Input.GetMouseButtonDown(0))
				{
					RequestNextLine();
				}
			}
			else
			{
				//�Kわり
				m_textControl.isScenario = false;
			}
		}
		else
		{
			//すべて燕幣していなかったら
			if (Input.GetMouseButtonDown(0))
			{
				m_textControl.ForceCompleteDisplaytext();
			}
		}
	}
	//肝の佩を�iむ
	void RequestNextLine()
	{
		if (m_textControl.isScenario == true)
		{
			var currentText = m_scenarios[m_currentLine];

			m_textControl.SetNextLine(CommandProcess(currentText));
			m_currentLine++;
			m_isCallPreload = false;
		}
	}

	//仟しいラインを函誼する
	public void UpdateLines(string fileName)
	{
		var scenarioText = Resources.Load<TextAsset>("Scenario/" + fileName);

		if (scenarioText == null)
		{
			Debug.LogError("Scenario file not found");
			Debug.LogError("ScenarioManger not active");
			enabled = false;
			return;
		}
		m_scenarios = scenarioText.text.Split(new string[] { "@br" }, System.StringSplitOptions.None);
		m_currentLine = 0;

		Resources.UnloadAsset(scenarioText);
	}
	//Lineにより、プロセスを�茂个垢�
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
	/*
	/// <summary>
	/// Starts the scenario.
	/// </summary>
	public void StartScenario()
	{
		iTween.MoveTo(hukidasi, iTween.Hash("x", -71,
			"islocal", true,
			"easeTupe", "easeOutExpo",
			"time", 1,
			"oncomplete", "RequestNextLine",
			"oncompletetarget", this.gameObject
		));
		iTween.MoveTo(playerImage, iTween.Hash("x", 700,
			"islocal", true,
			"easeTupe", "easeOutExpo",
			"time", 1
		));
	}

	public void FinishScenario()
	{
		iTween.MoveTo(hukidasi, iTween.Hash("x", -1700,
			"islocal", true,
			"easeTupe", "easeInExpo",
			"time", 1
		));
		iTween.MoveTo(playerImage, iTween.Hash("x", 2000,
			"islocal", true,
			"easeTupe", "easeInExpo",
			"time", 1
		));
	}
	*/
}