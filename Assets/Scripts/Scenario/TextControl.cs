using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextControl : MonoBehaviour
{


    [SerializeField]
    private Text _uiText;		//UI�e�L�X�g�̎Q�Ƃ�ۂ�

    [SerializeField]
    [Range(0.001f, 0.5f)]
    public float intervalForCharacterDisplay = 0.05f;	//1�����̕\���ɂ����鎞��

    public bool isScenario = false;	//�V�i���I�����ǂ����𔻒f

    private string currentText = string.Empty;	//���݂̕�����
    private float timeUntilDisplay = 0;			//�\���ɂ����鎞��
    private float timeElapsed = 1;				//������̕\�����J�n��������
    private int lastUpdateCharacter = -1;		//�\�����̕�����


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�N���b�N����o�ߎ��Ԃ��z��\�����Ԃ̉������m�F���A�\�����������o��
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        //�\�����������O��̕\���������ƈقȂ�Ȃ�e�L�X�g���X�V
        if (displayCharacterCount != lastUpdateCharacter)
        {
            _uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }
    /// <summary>
    /// ���ɕ\�����镶������Z�b�g����
    /// </summary>
    public void SetNextLine(string text)
    {
        currentText = text;

        //�z��\�����Ԃƌ��݂̎������L���b�V��
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        //���ԃJ�E���g��������
        lastUpdateCharacter = -1;
    }
    /// <summary>
    /// �����̕\�����������Ă��邩�ǂ���
    /// </summary>
    /// <value><c>true</c> if this instance is complete display text; otherwise, <c>false</c>.</value>
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    /// <summary>
    /// �����I�ɑS���\������
    /// </summary>
    public void ForceCompleteDisplaytext()
    {
        timeUntilDisplay = 0;
    }
}