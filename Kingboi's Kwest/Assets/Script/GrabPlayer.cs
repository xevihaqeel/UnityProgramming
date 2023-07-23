using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

////////////////////////////
// I removed all "2D"
////////////////////////////


public class GrabPlayer : MonoBehaviour {
	private GameObject cam;
	public GameObject kingBoi;
    private kingBoiControls kingBoiScript;
 
	// Use this for initialization

	void Start () {
	kingBoiScript = kingBoi.GetComponent<kingBoiControls>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
void OnTriggerStay(Collider other){
	if(other.CompareTag("Player")){
		kingBoiScript.platformed = true; 
		other.transform.SetParent(this.transform); 
		}

	}
}
