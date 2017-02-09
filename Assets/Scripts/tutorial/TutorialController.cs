using UnityEngine;
using System.Collections;

public class YumaControl : RootController
{
    #region Property
    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
    GameObject yajirusi;
    #endregion
    // インスタンス
    private static YumaControl instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static YumaControl Instance
    {
        get
        {
            if (instance == null)
                instance = new YumaControl();
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
        TutorialRoot.Instance.msg = "「まずはユウマをクリックして、";
        yajirusi = PlayerRoot.Instance.CreateObject(Resources.Load<GameObject>("Prefabs/UI/Tutorial/yajirusi"));
        foreach(var player in PlayerRoot.Instance.partyList)
        {
            if (player.GetComponent<JobBase>()._type == JobType.Attacker)
                yajirusi.transform.SetParent(player.transform);
        }
        yajirusi.transform.localPosition = new Vector3(0, 0, 0);
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
                        if (hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Attacker)
                        {
                            if (hit.collider.gameObject.GetComponent<JobBase>().controller == ReadyMode.Instance)
                            {
                                if (!isBtnShow)
                                {	//ボタンはまだ生成していない
                                    pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                                    if (pr.p_jb.CanTakeAction())
                                    {
                                        TutorialRoot.Instance.msg = "スラッシュを魔物までドラッグ！」";
                                        pr.p_jb.ShowSkillBtn();
                                        isBtnShow = true;
                                    }
                                }
                            }
                        }
                        break;
                    case 10: //Command
                        if (isBtnShow)
                        {	// ボタンを選択したら
                            // 選択したボタンを保管
                            Debug.Log(hit.collider.gameObject.GetComponent<SkillScript>().s_name);
                            if (hit.collider.gameObject.GetComponent<SkillScript>().s_name == "スラッシュ")
                            {
                                pr.btn = hit.collider.gameObject;
                                pr.s_script = pr.btn.GetComponent<SkillScript>();
                                pr.ChangeMode(t_targetMode.Instance);
                            }
                        }
                        break;
                }
            }
        }
        #endregion
    }
    override public void Exit(PlayerRoot pr = null)
    {
        PlayerRoot.Instance.DestroyObj(yajirusi);
    }
}
public class KiraControl : RootController
{
    #region Property
    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
    GameObject yajirusi;
    #endregion
    // インスタンス
    private static KiraControl instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static KiraControl Instance
    {
        get
        {
            if (instance == null)
                instance = new KiraControl();
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
        TutorialRoot.Instance.msg = "「さっきと同じようにキラをクリックして、";
        yajirusi = PlayerRoot.Instance.CreateObject(Resources.Load<GameObject>("Prefabs/UI/Tutorial/yajirusi"));
        foreach (var player in PlayerRoot.Instance.partyList)
        {
            if (player.GetComponent<JobBase>()._type == JobType.Defender)
                yajirusi.transform.SetParent(player.transform);
        }
        yajirusi.transform.localPosition = new Vector3(0, 0, 0);
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
                        if (hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Defender)
                        {
                            if (hit.collider.gameObject.GetComponent<JobBase>().controller == ReadyMode.Instance)
                            {
                                if (!isBtnShow)
                                {	//ボタンはまだ生成していない
                                    pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
                                    if (pr.p_jb.CanTakeAction())
                                    {
                                        TutorialRoot.Instance.msg = "イースをユウマにドラッグ！」";
                                        pr.p_jb.ShowSkillBtn();
                                        isBtnShow = true;
                                    }
                                }
                            }
                        }
                        break;
                    case 10: //Command
                        if (isBtnShow)
                        {	// ボタンを選択したら
                            // 選択したボタンを保管
                            if (hit.collider.gameObject.GetComponent<SkillScript>().s_name == "イース")
                            {
                                pr.btn = hit.collider.gameObject;
                                pr.s_script = pr.btn.GetComponent<SkillScript>();
                                pr.ChangeMode(t_targetMode.Instance);
                            }
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
    }
    override public void Exit(PlayerRoot pr = null)
    {
        PlayerRoot.Instance.DestroyObj(yajirusi);
    }
}
public class KirenControl : RootController
{
    #region Property
    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
    GameObject yajirusi;
    #endregion
    // インスタンス
    private static KirenControl instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static KirenControl Instance
    {
        get
        {
            if (instance == null)
                instance = new KirenControl();
            return instance;
        }
    }
    override public void Enter(PlayerRoot pr = null)
    {
        //初期化
        pr.s_script = null;
        pr.btn = null;
        isBtnShow = false;
        d_layerMask = LayerMask.GetMask(new string[] { "Player", "Command" , "Yousei"});
        TutorialRoot.Instance.msg = "「ボクの周りに浮遊する青い精霊クリック。";
        yajirusi = PlayerRoot.Instance.CreateObject(Resources.Load<GameObject>("Prefabs/UI/Tutorial/yajirusi"));
        foreach (var player in PlayerRoot.Instance.partyList)
        {
            if (player.GetComponent<JobBase>()._type == JobType.Leader)
                yajirusi.transform.SetParent(player.transform);
        }
        yajirusi.transform.localPosition = new Vector3(0, 0, 0);
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
                        if (hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Leader)
                        {
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
                        }
                        break;
                    case 14: //Command
                        if (hit.collider.gameObject.GetComponent<SkillScript>().s_name == "魔力の精霊")
                        {
                            TutorialRoot.Instance.msg = "精霊をノエルまでドラッグ！」";
                            pr.p_jb = hit.collider.gameObject.GetComponentInParent<JobBase>();
                            pr.btn = hit.collider.gameObject;
                            pr.s_script = pr.btn.GetComponent<SkillScript>();
                            pr.ChangeMode(t_targetMode.Instance);
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
    }
    override public void Exit(PlayerRoot pr = null)
    {
        PlayerRoot.Instance.DestroyObj(yajirusi);
    }
}
public class Kiren2Control : RootController
{
    #region Property
    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
    GameObject yajirusi;
    #endregion
    // インスタンス
    private static Kiren2Control instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static Kiren2Control Instance
    {
        get
        {
            if (instance == null)
                instance = new Kiren2Control();
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
        TutorialRoot.Instance.msg = "「次は緑色の精霊精霊をクリックする。";
        yajirusi = PlayerRoot.Instance.CreateObject(Resources.Load<GameObject>("Prefabs/UI/Tutorial/yajirusi"));
        foreach (var player in PlayerRoot.Instance.partyList)
        {
            if (player.GetComponent<JobBase>()._type == JobType.Leader)
                yajirusi.transform.SetParent(player.transform);
        }
        yajirusi.transform.localPosition = new Vector3(0, 0, 0);
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
                        if (hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Leader)
                        {
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
                        }
                        break;
                    case 14: //Yousei
                        if (hit.collider.gameObject.GetComponent<SkillScript>().s_name == "癒しの精霊")
                        {
                            TutorialRoot.Instance.msg = "緑色の精霊をノエルにドラッグ！」";
                            pr.p_jb = hit.collider.gameObject.GetComponentInParent<JobBase>();
                            pr.btn = hit.collider.gameObject;
                            pr.s_script = pr.btn.GetComponent<SkillScript>();
                            pr.ChangeMode(t_targetMode.Instance);
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
    }
    override public void Exit(PlayerRoot pr = null)
    {
        PlayerRoot.Instance.DestroyObj(yajirusi);
    }
}

/// <summary>
/// ターゲットを選択用 Singleton
/// </summary>
public class t_targetMode : RootController
{
    // ターゲットモードのインスタンス
    private static t_targetMode instance;
    /// <summary>
    /// TargetModeeのインスタンスを取得
    /// </summary>
    /// <value>TargetModeのインスタンス</value>
    public static t_targetMode Instance
    {
        get
        {
            if (instance == null)
                instance = new t_targetMode();
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
    GameObject yajirusi;
    #endregion
    override public void Enter(PlayerRoot pr = null)
    {
        yajirusi = PlayerRoot.Instance.CreateObject(Resources.Load<GameObject>("Prefabs/UI/Tutorial/yajirusi"));
        if (pr.previous_controller == YumaControl.Instance)
        {
            TutorialRoot.Instance.msg = "スラッシュを魔物までドラッグ！」";
            yajirusi.transform.SetParent(PlayerRoot.Instance.enemyList[1].transform);
            yajirusi.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (pr.previous_controller == KiraControl.Instance)
        {
            TutorialRoot.Instance.msg = "シールドをユウマにドラッグ！」";
            foreach (var player in PlayerRoot.Instance.partyList)
            {
                if (player.GetComponent<JobBase>()._type == JobType.Attacker)
                    yajirusi.transform.SetParent(player.transform);
            }
            yajirusi.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (pr.previous_controller == KirenControl.Instance)
        {
            TutorialRoot.Instance.msg = "精霊をノエルまでドラッグ！」";
            foreach (var player in PlayerRoot.Instance.partyList)
            {
                if (player.GetComponent<JobBase>()._type == JobType.Magician)
                    yajirusi.transform.SetParent(player.transform);
            }
            yajirusi.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (pr.previous_controller == Kiren2Control.Instance)
        {
            TutorialRoot.Instance.msg = "次は緑色の精霊をノエルにドラッグ！」";
            foreach (var player in PlayerRoot.Instance.partyList)
            {
                if (player.GetComponent<JobBase>()._type == JobType.Magician)
                    yajirusi.transform.SetParent(player.transform);
            }
            yajirusi.transform.localPosition = new Vector3(0, 0, 0);
        }
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
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
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
                            if (pr.previous_controller == KiraControl.Instance && hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Attacker)
                            {
                                SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
                            }
                            if (pr.previous_controller == KirenControl.Instance && hit.collider.gameObject.GetComponent<JobBase>()._type == JobType.Magician)
                            {
                                SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
                            }
                            if (pr.previous_controller == YumaControl.Instance)
                            {
                                SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
                            }
                            if (pr.previous_controller == Kiren2Control.Instance && hit.collider.gameObject.GetComponent<StatusControl>()._type == JobType.Magician)
                            {
                                SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
                            }
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
        #endregion
        pr.CheckEndBattel();
        pr.CheckGameOver();
    }
    override public void Exit(PlayerRoot pr = null)
    {
        if (pr.previous_controller == KirenControl.Instance)
        {
            TutorialRoot.Instance.msg = "「そのままノエルの魔法で魔物に攻撃だ！」";
        }
        else
            TutorialRoot.Instance.msg = "";
        PlayerRoot.Instance.DestroyObj(yajirusi);
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
        if (btn.GetComponentInParent<JobBase>()._type == JobType.Leader)
            btn.GetComponentInParent<JobBase>().HideKirenBtn();
        pr.p_jb.ChangeMode(t_SkillMode.Instance);
        pr.p_jb._target = target; pr.p_jb.skillUsing = pr.s_script;
        GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(pr.p_jb.gameObject);
        pr.ChangeMode(T_Wait.Instance);
    }
}
public class T_Wait : RootController
{
    #region Property
    #endregion
    // インスタンス
    private static T_Wait instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static T_Wait Instance
    {
        get
        {
            if (instance == null)
                instance = new T_Wait();
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
    }
}
