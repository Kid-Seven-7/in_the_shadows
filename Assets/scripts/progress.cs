using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class progress : MonoBehaviour {
	static public progress prog;
	public GameObject spotlight, infoPanel;
	public GameObject playPanel, modePanel, creditsPanel, progressPanel, startMain;
	public Button back, next, play, main, reset;
	public Text level, bestTime;
	public AudioSource click;
	public List<int> levelTimes;
	int lvl = 0;
	int size;

	void Awake(){
		prog = this;
	}

	void Start () {
		back.onClick.AddListener(backOnClick);
		next.onClick.AddListener(nextOnClick);
		play.onClick.AddListener(playOnClick);
		main.onClick.AddListener(mainOnClick);
    reset.onClick.AddListener(resetOnClick);

		if (PlayerPrefs.HasKey("level_1"))
			levelTimes.Add(PlayerPrefs.GetInt("level_1"));
    if (PlayerPrefs.HasKey("level_2"))
      levelTimes.Add(PlayerPrefs.GetInt("level_2"));
    if (PlayerPrefs.HasKey("level_3"))
      levelTimes.Add(PlayerPrefs.GetInt("level_3"));
		size = levelTimes.Count;
	}
	
	void Update () {
		spotlight.transform.position = Input.mousePosition;

		if (StartMenu.start.normalMode){
      if (lvl > size)
        play.gameObject.SetActive(false);
      else
        play.gameObject.SetActive(true);
		}

		level.text = (lvl == 0) ? "Level : One"
		:(lvl == 1) ? "Level : Two" 
		:(lvl == 2) ? "Level : Three"
		:(lvl == 3) ? "Level : Four"
		:(lvl == 4) ? "Level : Five" :"Other";

		if (lvl > size-1)
	    bestTime.text = "Best Time : " + "Not yet played";
		else{
	    bestTime.text = "Best Time : " + levelTimes[lvl].ToString();
			if (levelTimes[lvl] > 1)
        bestTime.text += " seconds";
			else
      	bestTime.text += " second";
		}
	}

	void backOnClick(){
		click.Play();
		lvl--;
	}

	void nextOnClick(){
		click.Play();
		lvl++;
	}

	void playOnClick(){
    click.Play();
		SceneManager.LoadScene(lvl+1);
	}

  void resetOnClick(){
    click.Play();
		PlayerPrefs.DeleteAll();
		levelTimes.Clear();
    size = levelTimes.Count;
  }

	void mainOnClick(){
    click.Play();
		startMain.SetActive(true);
		modePanel.SetActive(false);
		playPanel.SetActive(true);
		progressPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}
}
