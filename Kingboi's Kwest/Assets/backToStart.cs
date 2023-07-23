using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class backToStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("begin"); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator begin(){
		yield return new WaitForSeconds (3.0f); 
		SceneManager.LoadScene (0);
	}
}
