using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadGame() {
        this.GetComponent<AudioSource>().Play();
        FadeManager.Instance.LoadLevel("NormalScene", 2);
    }
}
