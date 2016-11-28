using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// WalkMode Singleton
/// </summary>
public class WalkMode : RootController {
	#region Property
	//移動速度
	private const float MOVE_SPEED = 20;
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
	public static WalkMode Instance{
		get {
			if(instance == null)
				instance = new WalkMode();
			return instance;
		}
	}
	override public void Enter(PlayerRoot pr = null)
	{
		pr.CreateChild("Player", pr.p_prefabList[0]);
		cameraSupport = GameObject.FindGameObjectWithTag ("Camera");
		// TODO@
		// cameraSupport.transform.position = defaultPos;
		// TODO@
		c_defaultRot = Camera.main.transform.localRotation;
		s_defaultRot = cameraSupport.transform.localRotation;
		p_animator = pr.p_jb.gameObject.GetComponentInChildren<Animator> ();
	}
	override public void Excute(PlayerRoot pr = null)
	{
		#region キャラクターのコントロール
		//移動用vector3
		Vector3  move_vector = Vector3.zero;
		//現在位置を保管する
		Vector3 p_pos = pr.transform.position;
		//移動中かどうか
		bool isMoved =false;
		if(Input.GetKey(KeyCode.A)){	//左
			pr.transform.Rotate
			(Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey(KeyCode.D)){	//右
			pr.transform.Rotate
			(Vector3.up * ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey(KeyCode.W)){	//上
			move_vector += Vector3.forward * MOVE_SPEED * Time.deltaTime;
			isMoved = true;
		}
		if(Input.GetKey(KeyCode.S)){	//下
			move_vector += Vector3.back * MOVE_SPEED / 1.5f * Time.deltaTime;
			isMoved = true;
		}
		//移動vector3を正規化して,移動方向を求める
		pr.transform.Translate(move_vector,Space.Self);
		//移動したら
		if(move_vector.magnitude >0.01f){
			//Playerの向きを移動方向に変える
			ReturnDefault();
		}
		else {
			isMoved = false;
		}
		p_animator.SetBool("isMoved", isMoved);

		#endregion
		#region カメラのコントロール
		if(Input.GetKey(KeyCode.UpArrow)){
			Camera.main.transform.Rotate (Vector3.right * CAMERA_ROTATE_SPEED * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			Camera.main.transform.Rotate (Vector3.left * CAMERA_ROTATE_SPEED * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			cameraSupport.transform.Rotate
			(Vector3.up * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			cameraSupport.transform.Rotate
			(Vector3.down * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		//マウス操作
		if(Input.GetMouseButtonDown(1)){
			touchPoint = Input.mousePosition;
		}
		if(Input.GetMouseButton(1)){
			Vector3 tempPoint = Input.mousePosition - touchPoint;
			cameraSupport.transform.Rotate
			(new Vector3(0,-tempPoint.x,0) * CAMERA_ROTATE_SPEED * Time.deltaTime, Space.Self);
			Camera.main.transform.Rotate (new Vector3(tempPoint.y,0,0) * CAMERA_ROTATE_SPEED * Time.deltaTime);
			touchPoint = Input.mousePosition;
		}
		#endregion
		#region 操作
		if(Input.GetKeyDown(KeyCode.Space)){
			if (pr.p_jb._target != null)
				Debug.Log (pr.p_jb._target.name);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
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
	private void ReturnDefault(){
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
		pr.DestroyChild("Player");
		// TODO@
		// cameraSupport.transform.position = battelPos;
		// TODO@
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
public class BattelMode : RootController {
	#region Property

	// タッチのレイヤーマスク
	private int d_layerMask;
	private int layerMask;
	private int u_layerMask;
	//
	private bool isBtnShow = false;
	#endregion
	// バトルモードのインスタンス
	private static BattelMode instance;
	/// <summary>
	/// バトルモードのインスタンスを取得
	/// </summary>
	/// <value>バトルモードのインスタンス</value>
	public static BattelMode Instance{
		get {
			if(instance == null)
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
		layerMask = LayerMask.GetMask (new string[] { "Player", "Enemy", "Ground" });
		u_layerMask = LayerMask.GetMask (new string[] { "Ground", "Player", "Enemy" });
		d_layerMask = LayerMask.GetMask (new string[] { "Player", "Command" });
	}
	override public void Excute(PlayerRoot pr = null)
	{
		#region マウス操作
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity,d_layerMask)) {
				switch(hit.collider.gameObject.layer){
				case 8: //Player
					if(!isBtnShow){	//ボタンはまだ生成していない
						pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
						if(pr.p_jb.CanTakeAction()){
							pr.p_jb.ShowSkillBtn();
							isBtnShow = true;
						}
					}
					// すでに生成したら
					else{
						pr.p_jb.HideSkillBtn();
						pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
						pr.p_jb.ShowSkillBtn();
					}
					break;
				case 10: //Command
					if(isBtnShow){	// ボタンを選択したら
						// 選択したボタンを保管
						pr.btn = hit.collider.gameObject;
						pr.s_script = pr.btn.GetComponent<SkillScript>();
						pr.ChangeMode(P_TargetMode.Instance);
					}
					break;
				}
			}
			if (Input.GetMouseButtonDown (1)) {
				pr.p_jb.HideSkillBtn();
			}
		}
		#endregion
	}
	override public void Exit(PlayerRoot pr = null)
	{
		Debug.Log("Exit");
	}
}

/// <summary>
/// ターゲットを選択用 Singleton
/// </summary>
public class P_TargetMode : RootController {
	// ターゲットモードのインスタンス
	private static P_TargetMode instance;
	/// <summary>
	/// TargetModeeのインスタンスを取得
	/// </summary>
	/// <value>TargetModeのインスタンス</value>
	public static P_TargetMode Instance{
		get {
			if(instance == null)
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
		if (pr.s_script.s_targetype == TargetType.PLAYER) {
			u_layerMask = LayerMask.GetMask (new string[] { "Player", "Ground" });
		} else {
			u_layerMask = LayerMask.GetMask (new string[] { "Enemy", "Ground" });
		}
		layerMask = LayerMask.GetMask (new string[] { "Ground", "Player", "Enemy" });
		d_layerMask = LayerMask.GetMask (new string[] { "Command" });
		// ボタンの初期位置を保管する
		btnTempPos = pr.btn.transform.position;
	}
	override public void Excute(PlayerRoot pr = null)
	{
		// ボタンがなければ
		if (pr.btn == null)
			pr.ChangeMode (BattelMode.Instance);
		#region マウス操作
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, d_layerMask)) {
				switch(hit.collider.gameObject.layer){
				case 8: //Player
					pr.p_jb.HideSkillBtn ();
					pr.p_jb = hit.collider.gameObject.GetComponent<JobBase> ();
					pr.p_jb.ShowSkillBtn ();
					pr.ChangeMode (BattelMode.Instance);
					break;
				case 10: //Command
					// 選択したボタンを保管
					pr.btn = hit.collider.gameObject;
					pr.s_script = pr.btn.GetComponent<SkillScript>();
					btnTempPos = pr.btn.transform.position;
					break;
				}
			}
		}
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity,layerMask)) {
				Debug.Log(hit.collider.gameObject.name);
				if(pr.btn != null){
					// ボタンを動かす
					Vector3 pos = hit.point;
					if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
						pos.y += 0.2f;
					pr.btn.transform.position = pos;
				}
			}
		}
		if(Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, u_layerMask)) {
				if(pr.btn != null){
					if(pr.s_script.s_targetNum != TargetNum.SELF){
						//レイヤーが同じなら
                        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
                        {
							SkillUse (pr, hit.collider.gameObject, pr.btn , pr.s_script.s_recast, pr.s_script.s_effectTime);
						}
						else
						//ボタンを初期位置に戻す
						pr.StartCoroutine(pr.LerpMove(pr.btn, btnTempPos,
							pr.btn.transform.position, 1));
					}
					// SELFなら、即発動
					else{
                        SkillUse(pr, pr.p_jb.gameObject,pr.btn, pr.s_script.s_recast, pr.s_script.s_effectTime);
					}
				}
			}
			else//ボタンを初期位置に戻す
				pr.StartCoroutine(pr.LerpMove(pr.btn, btnTempPos,
					pr.btn.transform.position, 1));
		}
		if (Input.GetMouseButtonDown (1)) {
			pr.ChangeMode (BattelMode.Instance);
		}
		#endregion
	}
	override public void Exit(PlayerRoot pr = null)
	{
		//ボタンを初期位置に戻す
		pr.StartCoroutine(pr.LerpMove(pr.btn, btnTempPos,
		pr.btn.transform.position, 1));
		pr.p_jb.HideSkillBtn();
	}
	/// <summary>
	/// スキルを使う
	/// </summary>
	/// <param name="pr">PlayerRoot.</param>
	/// <param name="target">Target.</param>
	/// <param name="btn">Skill Button.</param>
	/// <param name="recastTime">Recast time.</param>
	/// <param name="effectTime">Effect time.</param>
    private void SkillUse(PlayerRoot pr, GameObject target, GameObject btn, float recastTime, float effectTime = 0){
        pr.s_script.skillMethod(target, effectTime);
        pr.p_jb.StartCoroutine(pr.p_jb.SkillRecast(btn, recastTime));
        pr.ChangeMode(BattelMode.Instance);
    }
}