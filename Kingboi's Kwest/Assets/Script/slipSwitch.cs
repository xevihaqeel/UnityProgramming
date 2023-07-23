// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class slipSwitch : MonoBehaviour {
// 	private kingBoiControls kbc;
// 	private PolygonCollider2D pgc;  
// 	// Use this for initialization
// 	void Start () {
// 		kbc = GameObject.Find("KingBoi").GetComponent<kingBoiControls>(); 
// 		pgc = GetComponentInParent<PolygonCollider2D>(); 
// 		pgc.enabled = true; 
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
		
// 	}
// 	void OnTriggerEnter2D(Collider2D other){
// 		if(other.CompareTag("Player") && !kbc.canSlip){
// 			kbc.canSlip = true; 
// 		}
// 	}

// 	void OnTriggerExit2D(Collider2D other){
// 		if(other.CompareTag("Player") && kbc.canSlip == true){
// 			kbc.canSlip = false; 
// 		}
// 	}

// 	public void onOff(){
// 		pgc.enabled = false; 
// 		StartCoroutine("collOff"); 
// 	}
// 	public void onOn(){
// 		pgc.enabled = true; 
// 	}
// 	public IEnumerator collOff(){
// 		yield return new WaitForSeconds(2.0f);
// 		pgc.enabled = true; 

// 	}
// }
