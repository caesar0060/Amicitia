using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextControl : MonoBehaviour {


	[SerializeField] 
	private Text _uiText;		//UI�ƥ����Ȥβ��դ򱣤�

	[SerializeField][Range(0.001f, 0.5f)]
	public float intervalForCharacterDisplay = 0.05f;	//1���֤α�ʾ�ˤ�����r�g
	private string currentText = string.Empty;	//�F�ڤ�������
	private float timeUntilDisplay = 0;			//��ʾ�ˤ�����r�g
	private float timeElapsed = 1;				//�����Фα�ʾ���_ʼ�����r�g
	private int lastUpdateCharacter = -1;		//��ʾ�Ф�������


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//����å�����U�^�r�g���붨��ʾ�r�g�κΣ����_�J������ʾ�����������
		int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		//��ʾ��������ǰ�ؤα�ʾ�������Ȯ��ʤ�ʤ�ƥ����Ȥ����
		if (displayCharacterCount != lastUpdateCharacter) {
			_uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}
	/// <summary>
	/// �Τ˱�ʾ���������Ф򥻥åȤ���
	/// </summary>
	public void SetNextLine(string text){
		currentText = text;

		//�붨��ʾ�r�g�ȬF�ڤΕr�̤򥭥�å���
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		//�r�g������Ȥ���ڻ�
		lastUpdateCharacter = -1;
	}
	/// <summary>
	/// ���֤α�ʾ�����ˤ��Ƥ��뤫�ɤ���
	/// </summary>
	/// <value><c>true</c> if this instance is complete display text; otherwise, <c>false</c>.</value>
	public bool IsCompleteDisplayText{
		get { return Time.time > timeElapsed + timeUntilDisplay;}
	}

	/// <summary>
	/// ���ƵĤ�ȫ�ı�ʾ����
	/// </summary>
	public void ForceCompleteDisplaytext(){
		timeUntilDisplay = 0;
	}
}