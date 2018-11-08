using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartMenu : MonoBehaviour{
	public bool normalMode;
	static public StartMenu start;
	public AudioSource click, music;
    public Button play, credits, exit, test, normal, back;
    public GameObject playPanel, modePanel, spotlight, creditsPanel, progressPanel;

	void Awake(){
		start = this;
	}

    void Start(){
		music.Play();
        normalMode = false;
        modePanel.SetActive(false);
        playPanel.SetActive(true);
        progressPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    void Update(){
        play.onClick.AddListener(playOnClick);
        credits.onClick.AddListener(creditsOnClick);
        exit.onClick.AddListener(exitOnClick);
        test.onClick.AddListener(testOnClick);
        normal.onClick.AddListener(normalOnClick);
        back.onClick.AddListener(backOnClick);
		spotlight.transform.position = Input.mousePosition;
    }

#region OnClickFunctions
    void playOnClick(){
		click.Play();
        modePanel.SetActive(true);
        playPanel.SetActive(false);
    }

    void creditsOnClick(){
		click.Play();
        modePanel.SetActive(false);
        playPanel.SetActive(false);
        progressPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    void exitOnClick(){
        click.Play();
        Debug.Break();
        Application.Quit();
    }

	void testOnClick(){
        click.Play();
        normalMode = false;
        modePanel.SetActive(false);
        playPanel.SetActive(false);
        progressPanel.SetActive(true);
        creditsPanel.SetActive(false);
	}
	void normalOnClick(){
        click.Play();
		normalMode = true;
        modePanel.SetActive(false);
        playPanel.SetActive(false);
        progressPanel.SetActive(true);
        creditsPanel.SetActive(false);
	}

	void backOnClick(){
        click.Play();
        modePanel.SetActive(false);
        playPanel.SetActive(true);
        progressPanel.SetActive(false);
        creditsPanel.SetActive(false);
	}
#endregion
}
