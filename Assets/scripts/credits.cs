using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class credits : MonoBehaviour {

	public GameObject playPanel, modePanel, spotlight, creditsPanel, progressPanel, startMain;
	public Button back;

	public AudioSource click;

	void Update () {
		spotlight.transform.position = Input.mousePosition;	
		back.onClick.AddListener(backOnClick);
	}

	void backOnClick(){
		click.Play();
		startMain.SetActive(true);
		modePanel.SetActive(false);
		playPanel.SetActive(true);
		progressPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}
}
