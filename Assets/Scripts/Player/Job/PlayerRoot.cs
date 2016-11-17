using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerRoot : MonoBehaviour
{
    #region Properties
    //インスタンスを保存するコントローラ
	public RootController controller;
	//
	public JobBase p_jb;
	//
	public List<GameObject> enemyList = new List<GameObject> ();
	//
	public List<JobBase>partyList = new List<JobBase> ();
    #endregion

    void Awake(){
		if (p_jb == null)
			p_jb = GameObject.FindGameObjectWithTag ("Player").GetComponent<JobBase>();
	}

	void OnLevelWasLoaded(int level){
		//if(SceneManager.GetSceneAt(level).name == )
		if (p_jb == null)
			p_jb = GameObject.FindGameObjectWithTag ("Player").GetComponent<JobBase>();
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		//-----test
		controller = BattelMode.Instance;
		controller.Enter (this);
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
			Debug.LogError ("same");
	}
	void OnGUI() {
		if (GUI.Button (new Rect (10, 10, 100, 20), "Battel Mode"))
			ChangeMode (BattelMode.Instance);

		if (GUI.Button(new Rect(10, 50, 100, 20), "Walk Mode"))
			ChangeMode (WalkMode.Instance);

	}
}
