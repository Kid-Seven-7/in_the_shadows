using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class rotate : MonoBehaviour {
	//Public Global Variables
	public ParticleSystem ps;
  public Light spotLight;
	public Button next, exit;
  public Text msg, hint;
	public GameObject item, twin;
	public Vector3 targetRotation, targetPosition;
	public AudioSource click;

	//Private Global Variables
	Vector3 twinTarRot, twinTarPos;
	bool hori, vert, move, active, cleared, display, hasTwin;
  int level;
	string id;
	float time;
	AudioSource audioSource;

	void Awake(){
		vert = (this.CompareTag("teapot"))?false:true;

		move = (!this.CompareTag("teapot") && !this.CompareTag("elephant")) ? true : false;

		assignTargetVars();
	}

	void Start (){
		time = 0;
		display = false;
    hasTwin = false;

    level = (this.CompareTag("teapot")) ? 1
		: (this.CompareTag("elephant")) ? 2
		: (this.CompareTag("four")||this.CompareTag("two")) ? 3 : 4;

    hint.text = (level == 1) ? "Click and drag mouse to rotate object" 
		: (level == 2) ? "Drag mouse clockwise to rotate " 
		: (level == 3) ? "Hover over object and press space to (de)activate, click right crtl and drag mouse to move object" : "lvl 4";

		active = (id == "teapot" || id == "elephant") ? true : false;
    hasTwin = (id == "four" || id == "two") ? true : false;
	
		next.gameObject.SetActive(false);
    exit.gameObject.SetActive(false);
		spotLight.gameObject.SetActive(false);
	
		if (!this.CompareTag("teapot"))
			item.transform.Rotate(Random.Range(0,359),Random.Range(0, 359),Random.Range(0, 359));

	
	}

	void Update (){
		RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit)){
      if (hit.collider != null){
				if (Input.GetKey(KeyCode.Space) && hit.collider.CompareTag(id)){
					active = !active;
          spotLight.gameObject.SetActive(active);
        }
      }
    }

		if (Input.GetKey(KeyCode.Escape)){
			hint.text = "Have you been defeated???";
      exit.gameObject.SetActive(true);
      exit.onClick.AddListener(exitLevel);
    }

		if (Input.GetKey(KeyCode.R)){
			if (id == "teapot")
				hint.text = "Randomizing is not available on this level";
			else
	      item.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
		}

		if (!cleared){
      time += Time.deltaTime;
			randomTip();
      inRange();
			showTime();

			if (active){
        if (move){
          if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.RightControl))
						item.transform.position += new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
        }

        if (Input.GetMouseButton(0)){
          item.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
          if (vert)
            item.transform.Rotate(Input.GetAxis("Mouse Y"), 0, 0);
        }
			}

		}
		else{
			if (!display){
				hint.text = "";
				msg.text = "Cleared in " + timeToString();
				StartCoroutine(newLevel());
				display = true;
			}
    }
	}

	#region HelperFunctions
		void assignTargetVars(){
			targetPosition = (this.CompareTag("teapot") || this.CompareTag("elephant")) ? item.transform.position
			: (this.CompareTag("four")) ? new Vector3(0, 0, 0)
			: (this.CompareTag("two")) ? new Vector3(0, 0, 0)
			: (this.CompareTag("base")) ? new Vector3(0, 0, 0) : new Vector3(0, 0, 0);

			targetRotation = (this.CompareTag("teapot")) ? new Vector3(0, 269, 0)
			: (this.CompareTag("elephant")) ? new Vector3(86, 245, 157)
			: (this.CompareTag("four")) ? new Vector3(1, 91, 359)
			: (this.CompareTag("two")) ? new Vector3(359, 81, 178)
			: (this.CompareTag("base")) ? new Vector3(0, 0, 0) : new Vector3(0, 0, 0);

			id = (this.CompareTag("teapot")) ? "teapot"
			: (this.CompareTag("elephant")) ? "elephant"
			: (this.CompareTag("four")) ? "four"
			: (this.CompareTag("two")) ? "two"
			: (this.CompareTag("base")) ? "base" : "globe";

			twinTarRot = (id == "four") ? new Vector3(357, 81, 178) : (id == "two") ? new Vector3(1, 91, 359) : new Vector3(361, 361, 361);
  	}

		void nextLevel(){
			if (id == "teapot")
				SceneManager.LoadScene(2);
			if (id == "elephant")
				SceneManager.LoadScene(3);
			if (id == "four"||id == "four")
				SceneManager.LoadScene(0);
		}

		void exitLevel(){
			AudioSource temp = Instantiate(click);
			temp.gameObject.AddComponent<delete>();
			SceneManager.LoadScene(0);
		}

		string timeToString(){
			string timeAsString = "";
			int tempTime = Mathf.RoundToInt(time);
			int minutes = tempTime / 60; ;
			int seconds = tempTime % 60; ;

			if (minutes > 0)
				timeAsString = (minutes > 1) ? minutes + " minutes" : minutes + " minute";

			if (seconds > 0){
				if (seconds > 1)
					timeAsString += (minutes > 0) ? " and " + seconds + " seconds" : seconds + " seconds";
				else
					timeAsString += (minutes > 0) ? " and " + seconds + " second" : seconds + " second";
			}

			return (timeAsString);
		}

		void showTime(){
			if (time < 10){
      	msg.text = (id == "teapot") ? "Train me"
				: (id == "elephant") ? "Jumbo Sized"
				: "The answer is???";
				
				StartCoroutine(clearMsg());
			}

			if (Mathf.RoundToInt(time) % 30 == 0 && time > 5){
				msg.text = timeToString();
				StartCoroutine(clearMsg());
			}
		}

		void inRange(){
			Vector3 currentRot = new Vector3(item.transform.eulerAngles.x,item.transform.eulerAngles.y,item.transform.eulerAngles.z);

			if ((currentRot.x > targetRotation.x - 7.5 && currentRot.x < targetRotation.x + 7.5)
			&& (currentRot.y > targetRotation.y - 7.5 && currentRot.y < targetRotation.y + 7.5)
			&& (currentRot.z > targetRotation.z - 7.5 && currentRot.z < targetRotation.z + 7.5)){
				if (hasTwin == true){
					if (isInRange(twin.transform.eulerAngles ,twinTarRot)){
						display = false;
						cleared = true;
					}
				}else{
					display = false;
					cleared = true;
				}
			}

			if (cleared)
				clearedFunc();
		}

		bool isInRange(Vector3 rot, Vector3 baseRot){
			if ((rot.x > baseRot.x - 7.5 && rot.x < baseRot.x + 7.5)
			&& (rot.y > baseRot.y - 7.5 && rot.y < baseRot.y + 7.5)
			&& (rot.z > baseRot.z - 7.5 && rot.z < baseRot.z + 7.5)){
				return (true);
			}
			return (false);
		}

		void clearedFunc(){
			if (level == 1){
				if ((PlayerPrefs.HasKey("level_1") && PlayerPrefs.GetInt("level_1") > Mathf.RoundToInt(time)) || !PlayerPrefs.HasKey("level_1"))
					PlayerPrefs.SetInt("level_1", Mathf.RoundToInt(time));
			}else if (level == 2){
				PlayerPrefs.SetInt("level_2", Mathf.RoundToInt(time));
			}else if (level == 3){
				PlayerPrefs.SetInt("level_3", Mathf.RoundToInt(time));
			}

			PlayerPrefs.Save();
		}

		void randomTip(){
			if (time > 15){
				if (Mathf.RoundToInt(time) % 60 == 0){
					StartCoroutine(displayHint());
				}
			}
		}
	#endregion
	
	#region Coroutines
		IEnumerator displayHint(){
			int messageVal = Mathf.RoundToInt(Random.Range(0.0f, 6.0f));
			if (!display){
				hint.text = (messageVal == 0) ? "most objects face left"
				: (messageVal == 1) ? "making circles with mouse wiggles the object"
				: (messageVal == 2) ? "more than one object can be active"
				: (messageVal == 3) ? "wiggle object if they are not registered as correct"
				: (messageVal == 4) ? "press 'R' to randomize object rotation"
				: (messageVal == 5) ? "Just because it rotated left now, doesn't mean it always will"
				: "You don't have to click on an object to rotate it" ;
			
				display = true;
				yield return new WaitForSeconds(15);

				hint.text = "";
				yield return new WaitForSeconds(15);

				display = false;
			}
		}

		IEnumerator newLevel(){
			yield return new WaitForSeconds(3);
		
			if (!ps.isPlaying)
				ps.Play();

			msg.text = "level " + (level + 1) +" unlocked";
			next.gameObject.SetActive(true);
			next.onClick.AddListener(nextLevel);

			yield return new WaitForSeconds(3);
  	}

		IEnumerator clearMsg(){
			yield return new WaitForSeconds(5);

			msg.text = "";
		}
	#endregion
}
