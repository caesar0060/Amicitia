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
	//�Ƅ��ٶ�
	private const float MOVE_SPEED = 10;
	//��ܞ�ٶ�
	private const float ROTATE_SPEED = 100;
	private const float CAMERA_ROTATE_SPEED = 50;
	// �����νǶȤγ��ڂ�
	private Quaternion c_defaultRot;
	// cameraSupport�νǶȤγ��ڂ�
	private Quaternion s_defaultRot;
	// GameObject��ȡ��
	private GameObject cameraSupport;
	// ���å���λ��
	private Vector3 touchPoint;
	// �ץ쥤��`��Animator
	private Animator p_animator;
	private Vector3 NormalPos = Vector3.zero;
	private Vector3 NormalRot = Vector3.zero;
	#endregion
	// �Ƅӥ�`�ɤΥ��󥹥���
	private static WalkMode instance;
	/// <summary>
	/// �Ƅӥ�`�ɤΥ��󥹥��󥹤�ȡ��
	/// </summary>
	/// <value>�ƄӤΥ��󥹥���</value>
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
		// ���ڻ�����
		cameraSupport.transform.localPosition = NormalPos;
		cameraSupport.transform.localEulerAngles = NormalRot;
		//�����ڂ��򱣴椹��
		c_defaultRot = Camera.main.transform.localRotation;
		s_defaultRot = cameraSupport.transform.localRotation;
		p_animator = pr.p_jb.gameObject.GetComponentInChildren<Animator>();
		pr.partyList = new List<GameObject>();
		pr.enemyList = new List<GameObject>();
	}
	override public void Excute(PlayerRoot pr = null)
	{
		#region ����饯���`�Υ���ȥ�`��
		//�Ƅ���vector3
		Vector3 move_vector = Vector3.zero;
		//�F��λ�ä򱣹ܤ���
		Vector3 p_pos = pr.transform.position;
		//�Ƅ��Ф��ɤ���
		bool isMoved = false;
		if (Input.GetKey(KeyCode.A))
		{	//��
			pr.transform.Rotate
			(Vector3.down * ROTATE_SPEED * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey(KeyCode.D))
		{	//��
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
		//�Ƅ�vector3����Ҏ������,�Ƅӷ��������
		pr.transform.Translate(move_vector, Space.Self);
		//�ƄӤ�����
		if (move_vector.magnitude > 0.01f)
		{
			//Player���򤭤��Ƅӷ���ˉ䤨��
			ReturnDefault();
		}
		else
		{
			isMoved = false;
		}
		p_animator.SetBool("isMoved", isMoved);

		#endregion
		#region �����Υ���ȥ�`��
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
		//�ޥ�������
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
			if (pr.p_jb.p_target != null)
				Debug.Log(pr.p_jb.p_target.name);
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
	/// ���ڂ��ˑ���
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
	// GameObject��ȡ��
	private GameObject cameraSupport;
	private Vector3 battelPos = new Vector3(20.82f, -0.48f, 13.14f);
	private Vector3 battelRot = new Vector3(7.86f, 342.47f, 2.78f);
	#endregion
	// ���󥹥���
	private static BattelStart instance;
	/// <summary>
	/// ���󥹥��󥹤�ȡ��
	/// </summary>
	/// <value>���󥹥���</value>
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
		// ���ڻ�����
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

	// ���å��Υ쥤��`�ޥ���
	private int d_layerMask;
	private int layerMask;
	private int u_layerMask;
	//
	private bool isBtnShow = false;
	#endregion
	// �Хȥ��`�ɤΥ��󥹥���
	private static BattelMode instance;
	/// <summary>
	/// �Хȥ��`�ɤΥ��󥹥��󥹤�ȡ��
	/// </summary>
	/// <value>�Хȥ��`�ɤΥ��󥹥���</value>
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
		//���ڻ�
		pr.s_script = null;
		pr.btn = null;
		isBtnShow = false;
		layerMask = LayerMask.GetMask(new string[] { "Player", "Enemy", "Ground" });
		u_layerMask = LayerMask.GetMask(new string[] { "Ground", "Player", "Enemy" });
		d_layerMask = LayerMask.GetMask(new string[] { "Player", "Command" });
	}
	override public void Excute(PlayerRoot pr = null)
	{
		#region �ޥ�������
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
						{	//�ܥ���Ϥޤ����ɤ��Ƥ��ʤ�
							pr.p_jb = hit.collider.gameObject.GetComponent<JobBase>();
							if (pr.p_jb.CanTakeAction())
							{
								pr.p_jb.ShowSkillBtn();
								isBtnShow = true;
							}
						}
						// ���Ǥ����ɤ�����
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
					{	// �ܥ�����x�k������
						// �x�k�����ܥ���򱣹�
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
/// ���`���åȤ��x�k�� Singleton
/// </summary>
public class P_TargetMode : RootController
{
	// ���`���åȥ�`�ɤΥ��󥹥���
	private static P_TargetMode instance;
	/// <summary>
	/// TargetModee�Υ��󥹥��󥹤�ȡ��
	/// </summary>
	/// <value>TargetMode�Υ��󥹥���</value>
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
	// �ܥ���γ���λ��
	private Vector3 btnTempPos;
	// ���å��Υ쥤��`�ޥ���
	private int d_layerMask;
	private int layerMask;
	private int u_layerMask;
	#endregion
	override public void Enter(PlayerRoot pr = null)
	{
		//���ڻ�
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
		// �ܥ���γ���λ�ä򱣹ܤ���
		btnTempPos = pr.btn.transform.localPosition;
	}
	override public void Excute(PlayerRoot pr = null)
	{
		// �ܥ��󤬤ʤ����
		if (pr.btn == null)
			pr.ChangeMode(BattelMode.Instance);
		#region �ޥ�������
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
					// �x�k�����ܥ���򱣹�
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
					// �ܥ����Ӥ���
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
						//�쥤��`��ͬ���ʤ�
						if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
						{
							SkillUse(pr, hit.collider.gameObject, pr.btn, pr.s_script.s_effectTime);
						}
						else
							//�ܥ�������λ�äˑ���
							pr.StartCoroutine(pr.LerpMove(pr.btn,
								pr.btn.transform.localPosition, btnTempPos, 1));
					}
					// SELF�ʤ顢���k��
					else
					{
						SkillUse(pr, pr.p_jb.gameObject, pr.btn, pr.s_script.s_effectTime);
					}
				}
			}
			else//�ܥ�������λ�äˑ���
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
		//�ܥ�������λ�äˑ���
		pr.StartCoroutine(pr.LerpMove(pr.btn,
			pr.btn.transform.localPosition, btnTempPos, 1));
		pr.p_jb.HideSkillBtn();
	}
	/// <summary>
	/// �������ʹ��
	/// </summary>
	/// <param name="pr">PlayerRoot.</param>
	/// <param name="target">Target.</param>
	/// <param name="btn">Skill Button.</param>
	/// <param name="effectTime">Effect time.</param>
	private void SkillUse(PlayerRoot pr, GameObject target, GameObject btn, float effectTime = 0)
	{
		pr.p_jb.ChangeMode (SkillMode.Instance);
		pr.p_jb.p_target = target; pr.p_jb.skillUsing = pr.s_script;
		GameObject.FindGameObjectWithTag ("PartyRoot").GetComponent<PartyRoot> ().attackList.Add (pr.p_jb.gameObject);
		pr.ChangeMode(BattelMode.Instance);
	}
}