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
	private const float CAMERA_ROTATE_SPEED = 50;
	// カメラの角度の初期値
	private Quaternion c_defaultRot;
	// cameraSupportの角度の初期値
	private Quaternion s_defaultRot;
	// GameObjectを取得
	private GameObject cameraSupport;
	// 
	private Vector3 touchPoint;
	//
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
		//初期化
		cameraSupport = GameObject.FindGameObjectWithTag ("Camera");
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
		Vector3 p_pos = pr.p_jb.transform.position;
		//移動中かどうか
		bool isMoved =false;
		if(Input.GetKey(KeyCode.A)){	//左
			move_vector += Vector3.left;
			isMoved = true;
		}
		if(Input.GetKey(KeyCode.D)){	//右
			move_vector += Vector3.right;
			isMoved = true;
		}
		if(Input.GetKey(KeyCode.W)){	//上
			move_vector += Vector3.forward;
			isMoved = true;
		}
		if(Input.GetKey(KeyCode.S)){	//下
			move_vector += Vector3.back;
			isMoved = true;
		}
		//移動vector3を正規化して,移動方向を求める
		move_vector.Normalize();
		move_vector *= MOVE_SPEED * Time.deltaTime;
		p_pos += move_vector;
		p_pos.y = pr.p_jb.transform.position.y;
		pr.p_jb.transform.position = p_pos;
		//移動したら
		if(move_vector.magnitude >0.01f){
			//Playerの向きを移動方向に変える
			Quaternion q = Quaternion.LookRotation(move_vector, Vector3.up);
			pr.p_jb.transform.rotation = Quaternion.Lerp(pr.p_jb.transform.rotation, q, 0.2f);
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
		Debug.Log("WExit");
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
/// BattelMode Singleton
/// </summary>
public class BattelMode : RootController {
	#region Property
	// 長押しの時間
	private const float SHOW_COMMAND_TIME = 1;
	// タイマー
	private float timer = 0;
	// タッチのレイヤーマスク
	private int d_layerMask;
	private int layerMask;
	private int u_layerMask;
	//
	private bool isBtnGenerate = false;
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
		pr.p_jb.s_script = null;
		layerMask = LayerMask.GetMask (new string[] { "Player", "Command" });
		u_layerMask = LayerMask.GetMask (new string[] { "Command" });
		d_layerMask = LayerMask.GetMask (new string[] { "Player" });
	}
	override public void Excute(PlayerRoot pr = null)
	{
		#region マウス操作
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity,d_layerMask)) {
				if(isBtnGenerate ==false){
					GameObject obj = hit.collider.gameObject;
					Debug.Log(obj.name);
					if(obj.GetComponent<JobBase>().CanTakeAction()){
						pr.p_jb = obj.GetComponent<JobBase>();
					}
				}
			}
		}
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity,layerMask)) {
				if(pr.p_jb.gameObject == hit.collider.gameObject){
					if(isBtnGenerate == false){
						timer += Time.deltaTime;
						if(timer >= SHOW_COMMAND_TIME){
							pr.p_jb.skillBtnGenerate();
							isBtnGenerate = true;
							timer = 0;
						}
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, u_layerMask)) {
				pr.p_jb.s_script = hit.collider.gameObject.GetComponent<SkillScript>();
				switch(pr.p_jb.s_script.s_targetNum){
				case TargetNum.SELF:
					pr.p_jb.s_script.skillMethod();
					break;
				default :
					if(pr.p_jb.s_script.isEnemyTarget)
						pr.ChangeMode(E_TargetMode.Instance);
					else
						pr.ChangeMode(P_TargetMode.Instance);
					break;
				}
			}
			isBtnGenerate =false;
			pr.p_jb.skillBtnRemove();
		}
		if (Input.GetMouseButtonDown (1)) {
			if(isBtnGenerate ==true){
				isBtnGenerate = false;
				pr.p_jb.skillBtnRemove();
				pr.p_jb.s_script = null;
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
	// バトルモードのインスタンス
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
	// タッチのレイヤーマスク
	private int d_layerMask;
	override public void Enter(PlayerRoot pr = null)
	{
		//初期化
		d_layerMask = LayerMask.GetMask (new string[] { "Ground", "Player" });
		//何々をする
	}
	override public void Excute(PlayerRoot pr = null)
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, d_layerMask)) {
				switch (hit.collider.gameObject.layer) {
				case 8:
				case 11:
					pr.p_jb._target = hit.collider.gameObject;
					pr.p_jb.s_script.skillMethod ();
					pr.ChangeMode (BattelMode.Instance);
					break;
				default :
					break;
				}
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			pr.ChangeMode (BattelMode.Instance);
		}
	}
	override public void Exit(PlayerRoot pr = null)
	{
		Debug.Log("Exit");
		//何々をする
	}
}

/// <summary>
/// ターゲットを選択用 Singleton
/// </summary>
public class E_TargetMode : RootController {
	// バトルモードのインスタンス
	private static E_TargetMode instance;
	/// <summary>
	/// TargetModeeのインスタンスを取得
	/// </summary>
	/// <value>TargetModeのインスタンス</value>
	public static E_TargetMode Instance{
		get {
			if(instance == null)
				instance = new E_TargetMode();
			return instance;
		}
	}
	// タッチのレイヤーマスク
	private int d_layerMask;
	override public void Enter(PlayerRoot pr = null)
	{
		d_layerMask = LayerMask.GetMask (new string[] { "Enemy" });
		//何々をする
	}
	override public void Excute(PlayerRoot pr = null)
	{

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, d_layerMask)) {
				switch (hit.collider.gameObject.layer) {
				case 12:
					pr.p_jb._target = hit.collider.gameObject;
					pr.p_jb.s_script.skillMethod ();
					pr.ChangeMode (BattelMode.Instance);
					break;
				default :
					break;
				}
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			pr.ChangeMode (BattelMode.Instance);
		}
	}
	override public void Exit(PlayerRoot pr = null)
	{
		Debug.Log("Exit");
		//何々をする
	}
}


