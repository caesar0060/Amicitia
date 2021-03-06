﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SceneChange Singleton
/// </summary>
public class SceneChange : RootController
{
    #region Property
   
    #endregion
    // インスタンス
    private static SceneChange instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static SceneChange Instance
    {
        get
        {
            if (instance == null)
                instance = new SceneChange();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {

    }
    override public void Excute(PlayerRoot pr = null)
    {
       
    }
    override public void Exit(PlayerRoot pr = null)
    {
        try
        {
            if (pr.p_jb._target != null)
            {
                if (pr.p_jb._target.layer == LayerMask.NameToLayer("Enemy"))
                    pr.DestroyObj(pr.p_jb._target);
            }
        }
        catch (NullReferenceException){

        }
    }
}
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
	private GameObject player;
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
        pr.partyList = new List<GameObject>();
        pr.enemyList = new List<GameObject>();
        //pr.battelEnemyList = new List<GameObject>();
    }
    override public void Excute(PlayerRoot pr = null)
    {
        if (!GameObject.Find("Player"))
        {
            player = pr.CreateObject("Player", pr.fieldChara);
            cameraSupport = GameObject.FindGameObjectWithTag("Camera");
            cameraSupport.transform.SetParent(player.transform);
            cameraSupport.transform.localPosition = NormalPos;
            cameraSupport.transform.localEulerAngles = NormalRot;
            c_defaultRot = Camera.main.transform.localRotation;
            s_defaultRot = cameraSupport.transform.localRotation;
            p_animator = pr.p_jb.gameObject.GetComponentInChildren<Animator>();
        }
        if (!FadeManager.Instance.isFading)
        {
            #region キャラクターのコントロール
            //移動用vector3
            Vector3 move_vector = Vector3.zero;
            //移動中かどうか
            bool isMoved = false;
            if (Input.GetKey(KeyCode.A))
            {	//左
                pr.p_jb.transform.Rotate
                (Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.D))
            {	//右
                pr.p_jb.transform.Rotate
                (Vector3.up * ROTATE_SPEED * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.W))
            {	//上
                move_vector += pr.p_jb.transform.forward * Time.deltaTime;
                isMoved = true;
            }
            if (Input.GetKey(KeyCode.S))
            {	//下
                move_vector -= pr.p_jb.transform.forward * Time.deltaTime;
                isMoved = true;
            }
            //移動vector3を正規化して,移動方向を求める
            //pr.p_jb.transform.Translate(move_vector, Space.Self);
            move_vector = Vector3.Normalize(move_vector);
            move_vector.y -= 1;
            pr.p_jb.GetComponent<CharacterController>().Move(move_vector * MOVE_SPEED * Time.deltaTime);
            //移動したら
            move_vector.y += 1;
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
        }
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
            if (pr.p_jb._target != null)
            {
                pr.StartCoroutine(FadeManager.Instance.ReadyTalkUI(0.5f, TalkMode.Instance));
                ScenarioManager.Instance.UpdateLines(pr.p_jb._target.GetComponent<ScenarioScript>());
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
        pr.transform.position = player.transform.position;
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
        ScenarioManager sm = ScenarioManager.Instance;
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
            GameObject.FindGameObjectWithTag("Text").GetComponent<Text>().text = "";
            GameObject.FindGameObjectWithTag("Name").GetComponent<Text>().text = "";
			sm.ItweenMoveTo(sm.hukidasi, new Vector3(0, -600, 0), 0.5f, "easeInOutBack");
            if(SceneManager.GetActiveScene().name == "NormalScene")
                pr.StartCoroutine(FadeManager.Instance.CloseTalkUI(0.5f, WalkMode.Instance));
            else
                pr.StartCoroutine(FadeManager.Instance.CloseTalkUI(0.5f, BattelMode.Instance));
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
		pr.endBattel = false;
        pr.ChangeMode(BattelMode.Instance);
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
        d_layerMask = LayerMask.GetMask(new string[] { "Player", "Command", "Yousei" });
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
                            pr.ChangeMode(_targetMode.Instance);
                        }
                        break;
                    case 14: //Yousei
                        // 選択したボタンを保管
                        pr.p_jb.HideSkillBtn();
                        pr.p_jb = hit.collider.gameObject.GetComponentInParent<JobBase>();
                        pr.btn = hit.collider.gameObject;
                        pr.s_script = pr.btn.GetComponent<SkillScript>();
                        pr.ChangeMode(_targetMode.Instance);
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
public class _targetMode : RootController
{
    // ターゲットモードのインスタンス
    private static _targetMode instance;
    /// <summary>
    /// TargetModeeのインスタンスを取得
    /// </summary>
    /// <value>TargetModeのインスタンス</value>
    public static _targetMode Instance
    {
        get
        {
            if (instance == null)
                instance = new _targetMode();
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
        switch (pr.s_script.s_targetype)
        {
            case TargetType.PLAYER:
                u_layerMask = LayerMask.GetMask(new string[] { "Player", "Ground" });
                break;
            case TargetType.ENEMY:
                u_layerMask = LayerMask.GetMask(new string[] { "Enemy", "Ground" });
                break;
            case TargetType.BOTH:
                u_layerMask = LayerMask.GetMask(new string[] { "Player", "Enemy", "Ground" });
                break;
        }
        layerMask = LayerMask.GetMask(new string[] { "Ground", "Player", "Enemy" });
        d_layerMask = LayerMask.GetMask(new string[] { "Command", "Yousei" });
        // ボタンの初期位置を保管する
        btnTempPos = pr.btn.transform.localPosition;
    }
    override public void Excute(PlayerRoot pr = null)
    {
        // ボタンがなければ
        if (pr.btn == null)
            pr.ChangeMode(BattelMode.Instance);
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
                    case 14: //Yousei
                        // 選択したボタンを保管
                        pr.p_jb.HideSkillBtn();
                        pr.p_jb = hit.collider.gameObject.GetComponentInParent<JobBase>();
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
                        else//ボタンを初期位置に戻す
                        {
                            pr.StartCoroutine(pr.LerpMove(pr.btn,
                                pr.btn.transform.localPosition, btnTempPos, 1));
                        }
                    }
                    // SELFなら、即発動
                    else
                    {
                        SkillUse(pr, pr.p_jb.gameObject, pr.btn, pr.s_script.s_effectTime);
                    }
                }
            }
            else//ボタンを初期位置に戻す
            {
                pr.StartCoroutine(pr.LerpMove(pr.btn,
                    pr.btn.transform.localPosition, btnTempPos, 1));
            }
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
        GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(pr.p_jb.gameObject);
        if (btn.GetComponentInParent<JobBase>()._type == JobType.Leader)
            btn.GetComponentInParent<JobBase>().HideKirenBtn();
        pr.p_jb.ChangeMode(SkillMode.Instance);
        pr.p_jb._target = target; pr.p_jb.skillUsing = pr.s_script;
        pr.ChangeMode(BattelMode.Instance);
    }
}