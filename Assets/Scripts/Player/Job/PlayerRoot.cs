using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerRoot : MonoBehaviour {
	//インスタンスを保存するコントローラ
	public RootController controller;
	//
	public JobBase p_jb;
	//
	private List<GameObject> EnemyList = new List<GameObject> ();
	//
	private List<JobBase> PartyList = new List<JobBase> ();

	void Awake(){
		if (p_jb == null)
			p_jb = GameObject.FindGameObjectWithTag ("Player").GetComponent<JobBase>();
		Debug.Log (p_jb._hp);
	}
	void OnLevelWasLoaded(int level){
		//if(SceneManager.GetSceneAt(level).name == )
		if (p_jb == null)
			p_jb = GameObject.FindGameObjectWithTag ("Player").GetComponent<JobBase>();
		Debug.Log (p_jb._hp);
	}
	// Use this for initialization
	void Start () {
		//-----test
		controller = BattelMode.Instance;
		ChangeMode (WalkMode.Instance);
		//---------
	}
	
	// Update is called once per frame
	void Update () {
			controller.Excute(this);
	}
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(RootController newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.Log ("same");
	}
}
