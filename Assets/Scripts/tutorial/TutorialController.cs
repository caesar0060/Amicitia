﻿using UnityEngine;
using System.Collections;

public class YumaControl : RootController
{
    #region Property
    // タッチのレイヤーマスク
    private int d_layerMask;
    // ボタンが表示しているかどうか
    private bool isBtnShow = false;
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
                    case 10: //Command
                        if (isBtnShow)
                        {	// ボタンを選択したら
                            // 選択したボタンを保管
                            if (hit.collider.gameObject.GetComponent<SkillScript>().name == "スラッシュ")
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
        d_layerMask = LayerMask.GetMask(new string[] { "Command" });
        // ボタンの初期位置を保管する
        btnTempPos = pr.btn.transform.localPosition;
    }
    override public void Excute(PlayerRoot pr = null)
    {
        // ボタンがなければ
        if (pr.btn == null)
            pr.ChangeMode(pr.previous_controller);
        #region Function
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
                        pr.ChangeMode(pr.previous_controller);
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
                if (pr.btn == hit.collider.gameObject)
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
                if (pr.btn == hit.collider.gameObject)
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
            pr.ChangeMode(pr.previous_controller);
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
        pr.p_jb._target = target; pr.p_jb.skillUsing = pr.s_script;
        GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(pr.p_jb.gameObject);
        TutorialRoot.Instance.counter++;
        pr.ChangeMode(pr.previous_controller);
    }
}