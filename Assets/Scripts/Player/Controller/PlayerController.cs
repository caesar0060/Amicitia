using UnityEngine;
using System.Collections;

/// <summary>
/// WalkMode Singleton
/// </summary>
public class WalkMode : Controller {
	#region Property
	//移動速度
	private const float MOVE_SPEED = 30;
	//回転速度
	private const float ROTATE_SPEED = 100;
	// カメラの角度の初期値
	private Quaternion c_defaultRot;
	// cameraSupportの角度の初期値
	private Quaternion s_defaultRot;
	// GameObjectを取得
	private GameObject cameraSupport;
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
	override public void Enter(JobBase jb)
	{	
		cameraSupport = GameObject.FindGameObjectWithTag ("Camera");
		c_defaultRot = Camera.main.transform.localRotation;
		s_defaultRot = cameraSupport.transform.localRotation;

	}
	override public void Excute(JobBase jb)
	{
		#region キャラクターのコントロール
		if(Input.GetKey(KeyCode.W)){
			jb.transform.Translate (Vector3.forward * MOVE_SPEED * Time.deltaTime);
			ReturnDefault();
		}
		if(Input.GetKey(KeyCode.S)){
			jb.transform.Translate (Vector3.back * MOVE_SPEED * Time.deltaTime);
			ReturnDefault();
		}
		if(Input.GetKey(KeyCode.A)){
			jb.transform.Rotate (Vector3.down * ROTATE_SPEED * Time.deltaTime);
			ReturnDefault();
		}
		if(Input.GetKey(KeyCode.D)){
			jb.transform.Rotate (Vector3.up * ROTATE_SPEED * Time.deltaTime);
			ReturnDefault();
		}
		#endregion
		#region カメラのコントロール
		if(Input.GetKey(KeyCode.UpArrow)){
			Camera.main.transform.Rotate (Vector3.right * ROTATE_SPEED * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			Camera.main.transform.Rotate (Vector3.left * ROTATE_SPEED * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			cameraSupport.transform.Rotate
			(Vector3.up * ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			cameraSupport.transform.Rotate
			(Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		#endregion
		#region 操作
		if(Input.GetKeyDown(KeyCode.Space)){
			if (jb._target != null)
				Debug.Log (jb._target.name);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log("Esc");
		}
		#endregion
	}
	override public void Exit(JobBase jb)
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
public class BattelMode : Controller {
	#region Property
	// 長押しの時間
	private const float SHOW_COMMAND_TIME = 1;
	// タイマー
	private int timer;
	// タッチのレイヤーマスク
	private int d_layerMask;
	private int layerMask;
	private int u_layerMask;
	private GameObject p_object;
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
	override public void Enter(JobBase jb)
	{
		layerMask = LayerMask.GetMask (new string[] { "Player", "Party" });
		u_layerMask = LayerMask.GetMask (new string[] { "Command" });
		d_layerMask = LayerMask.GetMask (new string[] { "Enemy" });
	}
	override public void Excute(JobBase jb)
	{
		#region マウス操作
		switch(jb.battelStatus){
		case (BattelStatus.NORMAL):
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				}
			}
			if (Input.GetMouseButton (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
				}
			}
			if(Input.GetMouseButtonUp(0)){
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, u_layerMask)) {
				}
			}
			break;
		case(BattelStatus.TARGET_CHOOSE):
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, d_layerMask)) {
				}
			}
			break;
		}
		#endregion
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}

/// <summary>
/// ターゲットを選択用 Singleton
/// </summary>
public class TargetMode : Controller {
	// バトルモードのインスタンス
	private static TargetMode instance;
	/// <summary>
	/// TargetModeeのインスタンスを取得
	/// </summary>
	/// <value>TargetModeのインスタンス</value>
	public static TargetMode Instance{
		get {
			if(instance == null)
				instance = new TargetMode();
			return instance;
		}
	}
	// タッチのレイヤーマスク
	private int d_layerMask;
	override public void Enter(JobBase jb)
	{
		d_layerMask = LayerMask.GetMask (new string[] { "Enemy" });
	}
	override public void Excute(JobBase jb)
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, d_layerMask)) {
			}
		}
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}


