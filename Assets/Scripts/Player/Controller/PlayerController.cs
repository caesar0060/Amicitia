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
	//　カメラの最初位置
	private static Vector3 NormalPos = new Vector3(0f, 3f, -5f);
	private static Vector3 NormalRot = new Vector3(15f, 0f, 0f);
    //移動速度
    private const float MOVE_SPEED = 10;
    //回転速度
    private const float ROTATE_SPEED = 100;
    private const float CAMERA_ROTATE_SPEED = 50;
    // カメラの角度の初期値
    private Quaternion c_defaultRot;
    // cameraSupportの角度の初期値
    private Quaternion s_defaultRot;
    // GameObjectを取得
    private GameObject cameraSupport;
    // タッチの位置
    private Vector3 touchPoint;
    // プレイヤーのAnimator
    private Animator p_animator;
    #endregion
    // 移動モードのインスタンス
    private static WalkMode instance;
    /// <summary>
    /// 移動モードのインスタンスを取得
    /// </summary>
    /// <value>移動のインスタンス</value>
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
        if (!GameObject.Find("Player"))
            pr.CreateChild("Player", pr.p_prefabList[1]);
        cameraSupport = GameObject.FindGameObjectWithTag("Camera");
        // 初期化する
        cameraSupport.transform.localPosition = NormalPos;
        cameraSupport.transform.localEulerAngles = NormalRot;
        //　初期値を保存する
        c_defaultRot = Camera.main.transform.localRotation;
        s_defaultRot = cameraSupport.transform.localRotation;
        p_animator = pr.p_jb.gameObject.GetComponentInChildren<Animator>();
        pr.partyList = new List<GameObject>();
        pr.enemyList = new List<GameObject>();
    }
    override public void Excute(PlayerRoot pr = null)
    {
        #region キャラクターのコントロール
        //移動用vector3
        Vector3 move_vector = Vector3.zero;
        //移動中かどうか
        bool isMoved = false;
        if (Input.GetKey(KeyCode.A))
        {	//左
            pr.transform.Rotate
            (Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {	//右
            pr.transform.Rotate
            (Vector3.up * ROTATE_SPEED * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {	//上
            move_vector += Vector3.forward * MOVE_SPEED * Time.deltaTime;
            isMoved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {	//下
            move_vector += Vector3.back * MOVE_SPEED / 1.5f * Time.deltaTime;
            isMoved = true;
        }
        //移動vector3を正規化して,移動方向を求める
        pr.transform.Translate(move_vector, Space.Self);
        //移動したら
        if (move_vector.magnitude > 0.01f)
        {
            //Playerの向きを移動方向に変える
            ReturnDefault();
        }
        else
        {
            isMoved = false;
        }
        p_animator.SetBool("isMoved", isMoved);

        #endregion
        #region カメラのコントロール
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
        //マウス操作
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
        #region 操作
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pr.p_jb.p_target != null)
            {
                pr.StartCoroutine(pr.GetComponent<FadeManager>().ReadyTalkUI(0.5f, TalkMode.Instance));
                pr.GetComponent<ScenarioManager>().UpdateLines(pr.p_jb.p_target.GetComponent<ScenarioScript>().fileName);
            }
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
    /// 初期値に戻る
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
/// TalkMode Singleton
/// </summary>
public class TalkMode : RootController
{
    #region Property
 
    #endregion
    // インスタンス
    private static TalkMode instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static TalkMode Instance
    {
        get
        {
            if (instance == null)
                instance = new TalkMode();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {

    }
    override public void Excute(PlayerRoot pr = null)
    {
        ScenarioManager sm = pr.GetComponent<ScenarioManager>();
        //すべて表示したら
        if (sm.m_textControl.IsCompleteDisplayText)
        {
            //まだ次の行があったら
            if (sm.m_currentLine < sm.m_scenarios.Count)
            {
                //次の行を読む
                if (!sm.m_isCallPreload)
                {
                    sm.m_isCallPreload = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    sm.RequestNextLine();

                }
            }
            else
            {
                //終わり
                sm.isScenario = false;
            }
        }
        else
        {
            //すべて表示していなかったら
            if (Input.GetMouseButtonDown(0))
            {
                sm.m_textControl.ForceCompleteDisplaytext();
            }
        }
        if (Input.GetMouseButtonDown(0) && !sm.isScenario)
        {
			sm.ItweenMoveTo(sm.hukidasi, new Vector3(0, -600, 0), 0.5f, "easeInOutBack");
            pr.StartCoroutine(pr.GetComponent<FadeManager>().CloseTalkUI(0.5f, WalkMode.Instance));
        }
    }
    override public void Exit(PlayerRoot pr = null)
    {

    }
    #region Function

    #endregion
}
/// <summary>
/// BattelStart Singleton
/// </summary>
public class BattelStart : RootController
{
    #region Property
	//　カメラの最初位置
	private static Vector3 battelPos = new Vector3(25.7f, 2.91f, 14.97f);
	private static Vector3 battelRot = new Vector3(28.2461f, 270f, 1.94f);
    // GameObjectを取得
    private GameObject cameraSupport;
    #endregion
    // インスタンス
    private static BattelStart instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
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
        // 初期化する
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

    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
    #endregion
    // バトルモードのインスタンス
    private static BattelMode instance;
    /// <summary>
    /// バトルモードのインスタンスを取得
    /// </summary>
    /// <value>バトルモードのインスタンス</value>
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
        //初期化
        pr.s_script = null;
        pr.btn = null;
        isBtnShow = false;
        d_layerMask = LayerMask.GetMask(new string[] { "Player", "Command" });
    }
    override public void Excute(PlayerRoot pr = null)
    {
        #region マウス操作
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
                            {	//ボタンはまだ生成していない
                                pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                                if (pr.p_jb.CanTakeAction())
                                {
                                    pr.p_jb.ShowSkillBtn();
                                    isBtnShow = true;
                                }
                            }
                            // すでに生成したら
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
                        {	// ボタンを選択したら
                            // 選択したボタンを保管
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
/// ターゲットを選択用 Singleton
/// </summary>
public class P_TargetMode : RootController
{
    // ターゲットモードのインスタンス
    private static P_TargetMode instance;
    /// <summary>
    /// TargetModeeのインスタンスを取得
    /// </summary>
    /// <value>TargetModeのインスタンス</value>
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
    // ボタンの初期位置
    private Vector3 btnTempPos;
    // タッチのレイヤーマスク
    private int d_layerMask;
    private int layerMask;
    private int u_layerMask;
    #endregion
    override public void Enter(PlayerRoot pr = null)
    {
        //初期化
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
        // ボタンの初期位置を保管する
        btnTempPos = pr.btn.transform.localPosition;
    }
    override public void Excute(PlayerRoot pr = null)
    {
        // ボタンがなければ
        if (pr.btn == null)
            pr.ChangeMode(BattelMode.Instance);
        #region 穴它旦紱釬
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
                        // 選択したボタンを保管
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
                    // ボタンを動かす
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
                        //レイヤーが同じなら
                        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
                        {
                            SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
                        }
                        else
                            //ボタンを初期位置に戻す
                            pr.StartCoroutine(pr.LerpMove(pr.btn,
                                pr.btn.transform.localPosition, btnTempPos, 1));
                    }
                    // SELFなら、即発動
                    else
                    {
                        SkillUse(pr, pr.p_jb.gameObject, pr.btn, pr.s_script.s_effectTime);
                    }
                }
            }
            else//ボタンを初期位置に戻す
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
        //ボタンを初期位置に戻す
        pr.StartCoroutine(pr.LerpMove(pr.btn,
            pr.btn.transform.localPosition, btnTempPos, 1));
        pr.p_jb.HideSkillBtn();
    }
    /// <summary>
    /// スキルを使う
    /// </summary>
    /// <param name="pr">PlayerRoot.</param>
    /// <param name="target">Target.</param>
    /// <param name="btn">Skill Button.</param>
    /// <param name="effectTime">Effect time.</param>
    private void SkillUse(PlayerRoot pr, GameObject target, GameObject btn, float effectTime = 0)
    {
        pr.p_jb.ChangeMode(SkillMode.Instance);
        pr.p_jb.p_target = target; pr.p_jb.skillUsing = pr.s_script;
        GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(pr.p_jb.gameObject);
        pr.ChangeMode(BattelMode.Instance);
    }
}