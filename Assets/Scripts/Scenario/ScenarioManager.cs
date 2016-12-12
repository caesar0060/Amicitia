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

	private string[] m_scenarios;	//���ʥꥪ���{����
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
		//���٤Ʊ�ʾ������
		if (m_textControl.IsCompleteDisplayText)
		{
			//�ޤ��Τ��Ф����ä���
			if (m_currentLine < m_scenarios.Length)
			{
				//�Τ��Ф��i��
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
				//�K���
				m_textControl.isScenario = false;
			}
		}
		else
		{
			//���٤Ʊ�ʾ���Ƥ��ʤ��ä���
			if (Input.GetMouseButtonDown(0))
			{
				m_textControl.ForceCompleteDisplaytext();
			}
		}
	}
	//�Τ��Ф��i��
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

	//�¤����饤���ȡ�ä���
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
	//Line�ˤ�ꡢ�ץ�������Ф���
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