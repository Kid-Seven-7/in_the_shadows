using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour {
	void Start () {
		DestroyObject(gameObject, 0.3f);
	}
}
