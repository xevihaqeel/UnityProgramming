using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using DG.Tweening; 

public class StartScreen : MonoBehaviour {
	public Transform[] UI; 
	public bool hovering = false; 
	private AssetBundle myLoadedAssetBundle; 
	private string[] scenePaths; 

	//For the title// 
	public PathType pathType = PathType.Linear; 
	public Vector3[] waypoints = new[] {
	new Vector3(0,2,0)	};
	//For the title// 

	//For the button//	
	public Vector3[] waypoints1 = new[]{
		new Vector3(0,2,0) 
	}; 
	//For the button// 


	// Use this for initialization
	void Start () {
	DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
	Tween a = UI[0].DOLocalPath(waypoints, 3, pathType)
	.SetOptions(true); 
	a.SetEase(Ease.InOutCubic).SetLoops(-1); 
	Screen.orientation = ScreenOrientation.LandscapeLeft; 
	}	
	
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
	}

	public void loadNextLevel(){
	int scene = SceneManager.GetActiveScene().buildIndex; 
	SceneManager.LoadScene(scene+1); 
	}
}
