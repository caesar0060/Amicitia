using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// WalkMode Singleton
/// </summary>
public class WalkMode : RootController
{
    #region Property
    //�ړ����x
    private const float MOVE_SPEED = 10;
    //��]���x
    private const float ROTATE_SPEED = 100;
    private const float CAMERA_ROTATE_SPEED = 50;
    // �J�����̊p�x�̏����l
    private Quaternion c_defaultRot;
    // cameraSupport�̊p�x�̏����l
    private Quaternion s_defaultRot;
    // GameObject���擾
    private GameObject cameraSupport;
    // �^�b�`�̈ʒu
    private Vector3 touchPoint;
    // �v���C���[��Animator
    private Animator p_animator;
    private Vector3 NormalPos = Vector3.zero;
    private Vector3 NormalRot = Vector3.zero;
    #endregion
    // �ړ����[�h�̃C���X�^���X
    private static WalkMode instance;
    /// <summary>
    /// �ړ����[�h�̃C���X�^���X���擾
    /// </summary>
    /// <value>�ړ��̃C���X�^���X</value>
    public static WalkMode Instance
    {
        get
        {
            if (instance == null)
                instance = new WalkMode();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {
        pr.CreateChild("Player", pr.p_prefabList[1]);
        cameraSupport = GameObject.FindGameObjectWithTag("Camera");
        // ����������
        cameraSupport.transform.localPosition = NormalPos;
        cameraSupport.transform.localEulerAngles = NormalRot;
        //�@�����l��ۑ�����
        c_defaultRot = Camera.main.transform.localRotation;
        s_defaultRot = cameraSupport.transform.localRotation;
        p_animator = pr.p_jb.gameObject.GetComponentInChildren<Animator>();
        pr.partyList = new List<GameObject>();
        pr.enemyList = new List<GameObject>();
    }
    override public void Excute(PlayerRoot pr = null)
    {
        #region �L�����N�^�[�̃R���g���[��
        //�ړ��pvector3
        Vector3 move_vector = Vector3.zero;
        //���݈ʒu��ۊǂ���
        Vector3 p_pos = pr.transform.position;
        //�ړ������ǂ���
        bool isMoved = false;
        if (Input.GetKey(KeyCode.A))
        {	//��
            pr.transform.Rotate
            (Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {	//�E
            pr.transform.Rotate
            (Vector3.up * ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {	//��
            move_vector += Vector3.forward * MOVE_SPEED * Time.deltaTime;
            isMoved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {	//��
            move_vector += Vector3.back * MOVE_SPEED / 1.5f * Time.deltaTime;
            isMoved = true;
        }
        //�ړ�vector3�𐳋K������,�ړ����������߂�
        pr.transform.Translate(move_vector, Space.Self);
        //�ړ�������
        if (move_vector.magnitude > 0.01f)
        {
            //Player�̌������ړ������ɕς���
            ReturnDefault();
        }
        else
        {
            isMoved = false;
        }
        p_animator.SetBool("isMoved", isMoved);

        #endregion
        #region �J�����̃R���g���[��
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.Rotate(Vector3.right * CAMERA_ROTATE_SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.Rotate(Vector3.left * CAMERA_ROTATE_SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraSupport.transform.Rotate
            (Vector3.up * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cameraSupport.transform.Rotate
            (Vector3.down * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        //�}�E�X����
        if (Input.GetMouseButtonDown(1))
        {
            touchPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 tempPoint = Input.mousePosition - touchPoint;
            cameraSupport.transform.Rotate
            (new Vector3(0, -tempPoint.x, 0) * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
            Camera.main.transform.Rotate(new Vector3(tempPoint.y, 0, 0) * CAMERA_ROTATE_SPEED * Time.deltaTime);
            touchPoint = Input.mousePosition;
        }
        #endregion
        #region ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pr.p_jb._target != null)
                Debug.Log(pr.p_jb._target.name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc");
        }
        #endregion
    }
    override public void Exit(PlayerRoot pr = null)
    {

    }
    #region Function
    /// <summary>
    /// �����l�ɖ߂�
    /// </summary>
    private void ReturnDefault()
    {
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation,
            c_defaultRot, Time.deltaTime * 5);
        cameraSupport.transform.localRotation = Quaternion.Slerp(cameraSupport.transform.localRotation,
            s_defaultRot, Time.deltaTime * 5);
    }
    #endregion
}
/// <summary>
/// BattelStart Singleton
/// </summary>
public class BattelStart : RootController
{
    #region Property
    // GameObject���擾
    private GameObject cameraSupport;
    private Vector3 battelPos = new Vector3(22.82f, 0.44f, 10.72f);
    private Vector3 battelRot = new Vector3(1.12f, 328.4f, 0.8f);
    #endregion
    // �C���X�^���X
    private static BattelStart instance;
    /// <summary>
    /// �C���X�^���X���擾
    /// </summary>
    /// <value>�C���X�^���X</value>
    public static BattelStart Instance
    {
        get
        {
            if (instance == null)
                instance = new BattelStart();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {
        // ����������
        cameraSupport = GameObject.FindGameObjectWithTag("Camera");
        pr.DestroyChild("Player");
        cameraSupport.transform.position = battelPos;
        cameraSupport.transform.rotation = Quaternion.Euler(battelRot);
        pr.ChangeMode(BattelMode.Instance);
        pr.endBattel = false;
    }
    override public void Excute(PlayerRoot pr = null)
    {


    }
    override public void Exit(PlayerRoot pr = null)
    {

    }
    #region Function

    #endregion
}

/// <summary>
/// BattelMode Singleton
/// </summary>
public class BattelMode : RootController
{
    #region Property

    // �^�b�`�̃��C���[�}�X�N
    private int d_layerMask;
    private int layerMask;
    private int u_layerMask;
    //
    private bool isBtnShow = false;
    #endregion
    // �o�g�����[�h�̃C���X�^���X
    private static BattelMode instance;
    /// <summary>
    /// �o�g�����[�h�̃C���X�^���X���擾
    /// </summary>
    /// <value>�o�g�����[�h�̃C���X�^���X</value>
    public static BattelMode Instance
    {
        get
        {
            if (instance == null)
                instance = new BattelMode();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {
        //������
        pr.s_script = null;
        pr.btn = null;
        isBtnShow = false;
        layerMask = LayerMask.GetMask(new string[] { "Player", "Enemy", "Ground" });
        u_layerMask = LayerMask.GetMask(new string[] { "Ground", "Player", "Enemy" });
        d_layerMask = LayerMask.GetMask(new string[] { "Player", "Command" });
    }
    override public void Excute(PlayerRoot pr = null)
    {
        #region �}�E�X����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, d_layerMask))
            {
                switch (hit.collider.gameObject.layer)
                {
                    case 8: //Player
                        if (hit.collider.gameObject.GetComponent<JobBase>().controller == ReadyMode.Instance)
                        {
                            if (!isBtnShow)
                            {	//�{�^���͂܂��������Ă��Ȃ�
                                pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                                if (pr.p_jb.CanTakeAction())
                                {
                                    pr.p_jb.ShowSkillBtn();
                                    isBtnShow = true;
                                }
                            }
                            // ���łɐ���������
                            else
                            {
                                pr.p_jb.HideSkillBtn();
                                pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                                pr.p_jb.ShowSkillBtn();
                            }
                        }
                        break;
                    case 10: //Command
                        if (isBtnShow)
                        {	// �{�^����I��������
                            // �I�������{�^����ۊ�
                            pr.btn = hit.collider.gameObject;
                            pr.s_script = pr.btn.GetComponent<SkillScript>();
                            pr.ChangeMode(P_TargetMode.Instance);
                        }
                        break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            pr.p_jb.HideSkillBtn();
        }
        #endregion
        pr.CheckEndBattel();
        pr.CheckGameOver();
    }
    override public void Exit(PlayerRoot pr = null)
    {

    }
}

/// <summary>
/// �^�[�Q�b�g��I��p Singleton
/// </summary>
public class P_TargetMode : RootController
{
    // �^�[�Q�b�g���[�h�̃C���X�^���X
    private static P_TargetMode instance;
    /// <summary>
    /// TargetModee�̃C���X�^���X���擾
    /// </summary>
    /// <value>TargetMode�̃C���X�^���X</value>
    public static P_TargetMode Instance
    {
        get
        {
            if (instance == null)
                instance = new P_TargetMode();
            return instance;
        }
    }
    #region Property
    // �{�^���̏����ʒu
    private Vector3 btnTempPos;
    // �^�b�`�̃��C���[�}�X�N
    private int d_layerMask;
    private int layerMask;
    private int u_layerMask;
    #endregion
    override public void Enter(PlayerRoot pr = null)
    {
        //������
        if (pr.s_script.s_targetype == TargetType.PLAYER)
        {
            u_layerMask = LayerMask.GetMask(new string[] { "Player", "Ground" });
        }
        else
        {
            u_layerMask = LayerMask.GetMask(new string[] { "Enemy", "Ground" });
        }
        layerMask = LayerMask.GetMask(new string[] { "Ground", "Player", "Enemy" });
        d_layerMask = LayerMask.GetMask(new string[] { "Command" });
        // �{�^���̏����ʒu��ۊǂ���
        btnTempPos = pr.btn.transform.localPosition;
    }
    override public void Excute(PlayerRoot pr = null)
    {
        // �{�^�����Ȃ����
        if (pr.btn == null)
            pr.ChangeMode(BattelMode.Instance);
        #region �}�E�X����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, d_layerMask))
            {
                switch (hit.collider.gameObject.layer)
                {
                    case 8: //Player
                        pr.p_jb.HideSkillBtn();
                        pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                        pr.p_jb.ShowSkillBtn();
                        pr.ChangeMode(BattelMode.Instance);
                        break;
                    case 10: //Command
                        // �I�������{�^����ۊ�
                        pr.btn = hit.collider.gameObject;
                        pr.s_script = pr.btn.GetComponent<SkillScript>();
                        btnTempPos = pr.btn.transform.localPosition;
                        break;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (pr.btn != null)
                {
                    // �{�^���𓮂���
                    Vector3 pos = hit.point;
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                        pos.y += 0.2f;
                    pr.btn.transform.position = pos;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, u_layerMask))
            {
                if (pr.btn != null)
                {
                    if (pr.s_script.s_targetNum != TargetNum.SELF)
                    {
                        //���C���[�������Ȃ�
                        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
                        {
                            SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_recast, pr.s_script.s_effectTime);
                        }
                        else
                            //�{�^���������ʒu�ɖ߂�
                            pr.StartCoroutine(pr.LerpMove(pr.btn,
                                pr.btn.transform.localPosition, btnTempPos, 1));
                    }
                    // SELF�Ȃ�A������
                    else
                    {
                        SkillUse(pr, pr.p_jb.gameObject, pr.btn, pr.s_script.s_recast, pr.s_script.s_effectTime);
                    }
                }
            }
            else//�{�^���������ʒu�ɖ߂�
                pr.StartCoroutine(pr.LerpMove(pr.btn,
                    pr.btn.transform.localPosition, btnTempPos, 1));
        }
        if (Input.GetMouseButtonDown(1))
        {
            pr.ChangeMode(BattelMode.Instance);
        }
        #endregion
        pr.CheckEndBattel();
        pr.CheckGameOver();
    }
    override public void Exit(PlayerRoot pr = null)
    {
        //�{�^���������ʒu�ɖ߂�
        pr.StartCoroutine(pr.LerpMove(pr.btn,
            pr.btn.transform.localPosition, btnTempPos, 1));
        pr.p_jb.HideSkillBtn();
    }
    /// <summary>
    /// �X�L�����g��
    /// </summary>
    /// <param name="pr">PlayerRoot.</param>
    /// <param name="target">Target.</param>
    /// <param name="btn">Skill Button.</param>
    /// <param name="recastTime">Recast time.</param>
    /// <param name="effectTime">Effect time.</param>
    private void SkillUse(PlayerRoot pr, GameObject target, GameObject btn, float recastTime, float effectTime = 0)
    {
        pr.s_script.skillMethod(pr.s_script, target, effectTime);
        pr.p_jb.ChangeMode(SkillMode.Instance);
        pr.ChangeMode(BattelMode.Instance);
    }
}