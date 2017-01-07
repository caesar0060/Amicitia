using UnityEngine;
using System.Collections;

public class AttackerScript : JobBase {
	#region Properties


	#endregion

	// ------------------------------------------------------------------------------------
	//*										Debug用
	void OnGUI() {
		GUI.Label (new Rect (120, 10, 200, 20), "Attacker: " + controller.ToString ());
		GUI.Label (new Rect (120, 50, 200, 20), "Attacker: " + conditionStatus.ToString());
	}
	//*/

	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		p_funcList = new P_Delegate[]{ Skill1};
		controller = ReadyMode.Instance;
		controller.Enter (this);
		skillBtnGenerate ();
		HideSkillBtn ();
	}
	
	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}
	#region Function
    /// <summary>
    /// スラッシュ
    /// 物理攻撃
    /// </summary>
	/// <param name="sc">Skill Script</param>
    /// <param name="target">Target.</param>
    /// <param name="effectTime">Effect time.</param>
	public void Skill1(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		StartCoroutine( LerpMove (this.gameObject, this.transform.position, target.transform.position,
			1, target, sc, "Attack"));
	}
	/// <summary>
	/// 物理強攻撃
	/// </summary>
    /// <param name="sc">Skill Script</param>
    /// <param name="target">Target.</param>
    /// <param name="effectTime">Effect time.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
        StartCoroutine(LerpMove(this.gameObject, this.transform.position, target.transform.position,
            1, target, sc, "Attack"));
	}
	/// <summary>
	/// 
	/// </summary>
    /// <param name="sc">Skill Script</param>
    /// <param name="target">Target.</param>
    /// <param name="effectTime">Effect time.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		Debug.Log ("Tank Skill1");
	}
	/// <summary>
	/// 回転切り
	/// </summary>
    /// <param name="sc">Skill Script</param>
    /// <param name="target">Target.</param>
    /// <param name="effectTime">Effect time.</param>>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
        StartCoroutine(LerpMove(this.gameObject, this.transform.position, target.transform.position,
            1, target, sc, "Attack"));
	}
	#endregion
}
