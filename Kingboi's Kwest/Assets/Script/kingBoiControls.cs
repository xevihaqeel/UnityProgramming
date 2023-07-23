using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using UnityStandardAssets.CrossPlatformInput; 
using DG.Tweening;

////////////////////////////
// I removed all "2D"
////////////////////////////



public class kingBoiControls : MonoBehaviour{
	//MOVEMENT// 
	// bool jump = false; 
	bool facingRight = true; 
	public float gravity  = 1; 
	public float moveForce; 
	public float maxSpeed; 
	Rigidbody rb;
	private Animator anim; 
	private Transform kingBoi; 
	//MOVEMENT//

	//CONTROL// 
	public float masterEase = 0.6f;
	public float duration; 
	public float h; 
	public float h1; 
	//CONTROL// 
	
	//PERSONAL CHECKS// 
	public bool grounded;
	public bool platformed;
	public bool sliding;
	public bool canMove; 
	

	//ENVIRONMENTAL STUFF//
	private GameObject cam; 
	private GameObject marker; 
	public GameObject heart; 
	private GameObject goalText; 
	private GameObject platform; 
	public float sideRay; 
	public LayerMask[] layers; 

	//ENVIRONMENTAL STUFF// 

	//SOUND//
	public AudioClip[] sfx;
	private AudioSource sounds {get { return GetComponent<AudioSource>();}}
	//SOUND// 



	public void Awake(){
	

	}

	public void Start(){
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
		DOTween.SetTweensCapacity(500,125); 
		anim = GetComponent<Animator>(); 
		rb = GetComponent<Rigidbody>();
		kingBoi = this.transform; 
		
		//arrow
		// marker = GameObject.Find("Marker");
		// marker.SetActive(false);
		heart.SetActive(false); 
		goalText = GameObject.Find("GoalText"); 
		goalText.SetActive(false); 
		cam = GameObject.FindGameObjectWithTag("MainCamera"); 
		heart.transform.DORotate(new Vector3(0,0,10), 0.3f).SetEase(Ease.InOutCubic).SetLoops(-1,LoopType.Yoyo); 

		// saws = GameObject.FindGameObjectWithTag("Saw"); 
		//environment sh*t 

	}

	private void Update(){

		float kBoiy = this.transform.localPosition.y;
		float kBoix = this.transform.localPosition.x; 
		cam.transform.position = new Vector3(this.transform.position.x,cam.transform.position.y,cam.transform.position.z); 		
		if(platformed == false){
		sMove();
		}

		// marker.transform.DOMove(new Vector3(this.transform.localPosition.x, kBoiy += 2.0f,this.transform.localPosition.z),.02f); 
		heart.transform.position = new Vector3(kBoix+=1.0f, kBoiy += 2.0f, this.transform.localPosition.z); 
		goalText.transform.position = new Vector3(this.transform.localPosition.x, kBoiy += 2.0f, this.transform.localPosition.z); 
		goalText.transform.Rotate(new Vector3(0, 100 * Time.deltaTime,0)); 

		RaycastHit hit;
		RaycastHit hit1; 
		RaycastHit hit2; 
		//GROUND && PLATFORM CHECK/// 
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), out hit, 2f, layers [0])) {
			// Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down) * hit.distance, Color.green); 
			grounded = true; 
		} else if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), out hit, 2f, layers [2])) {
			platformed = true; 
		} else{
			grounded = false;
			platformed = false; 
		}
		//GROUND && PLATFORM CHECK/// 


		//FEET CHECK// 
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), out hit, 2f, layers [1])) {
			if (facingRight) {
				StartCoroutine ("pushBack"); 
			} else if (!facingRight) {
				StartCoroutine ("pushBack1"); 
			}
			} else {
			}
		//FEET CHECK// 


		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.right), out hit1, sideRay, layers [1])) {
			// Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * hit1.distance, Color.green);
			StartCoroutine ("pushBack"); 

		} else if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.left), out hit2, sideRay, layers [1])) {
			// Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * hit1.distance, Color.white); 
			StartCoroutine ("pushBack1"); 
		} else {
		}

//		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit2, sideRay, layers[1])){
//			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left)* hit2.distance, Color.green);
//			rb.velocity = Vector3.zero; 
//			rb.AddForce(Vector3.right * h *  moveForce * 2); 
//			canSlide = false; 
//			}else{
//			canSlide = true; 
//			}
		

 		///to kill us// 
 		Vector3 screenPosition = Camera.main.WorldToScreenPoint(kingBoi.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0){
        	StartCoroutine("deathAni"); 
        }
	}
	
	public void FixedUpdate(){
		h = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(h)); 
		if(h * rb.velocity.x < maxSpeed){
			rb.AddForce(Vector3.right * h * moveForce); 
		}else if (Mathf.Abs (GetComponent<Rigidbody> ().velocity.x) > maxSpeed) {
		 	GetComponent<Rigidbody>().velocity = new Vector3 (Mathf.Sign (GetComponent<Rigidbody> ().velocity.x) * maxSpeed, GetComponent<Rigidbody> ().velocity.y,0);                 
		}

		if(h > 0 && !facingRight){
			kingBoi.DORotate(new Vector3(0,0,0), 0.3f).SetEase(Ease.InOutCubic); 
			Flip(); 
		} else if(h < 0 && facingRight){
			kingBoi.DORotate(new Vector3(0,180,0), 0.3f).SetEase(Ease.InOutCubic); 
			Flip(); 
		}
		if(rb.velocity.x > 0 || rb.velocity.x < 0){
			anim.SetBool("Moving", true);
		}else{
			anim.SetBool("Moving", false);
		}	
			mobileMovement(); 
}

	 void mobileMovement(){
		float h1 = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		anim.SetFloat("Speed",Mathf.Abs(h1));  
		if(h1 * rb.velocity.x < maxSpeed){
			rb.AddForce(Vector3.right * h1 * moveForce); 
		}else if (Mathf.Abs (GetComponent<Rigidbody> ().velocity.x) > maxSpeed) {
		 	GetComponent<Rigidbody>().velocity = new Vector3 (Mathf.Sign (GetComponent<Rigidbody> ().velocity.x) * maxSpeed, GetComponent<Rigidbody> ().velocity.y,0);                 
		}

		if(h1 > 0 && !facingRight){
			kingBoi.DORotate(new Vector3(0,0,0), 0.3f).SetEase(Ease.InOutCubic); 
			Flip(); 
		} else if(h1 < 0 && facingRight){
			kingBoi.DORotate(new Vector3(0,180,0), 0.3f).SetEase(Ease.InOutCubic); 
			Flip(); 
		}
		if(rb.velocity.x > 0 || rb.velocity.x < 0){
			anim.SetBool("Moving", true);
		}else{
			anim.SetBool("Moving", false);
		}
	}



	public void activate(){
	if(grounded == true && platformed == false && !sliding){
	StartCoroutine("slideMove"); 
		}else{

		}
	}

	 void sMove(){
		if(Input.GetButtonDown("Jump") && grounded == true && platformed == false && !sliding){
			StartCoroutine("slideMove"); 
		}else{
		}
	}

	 IEnumerator slideMove(){
		kSlide();
		yield return new WaitForSeconds(0.5f); 
		unSlide();
		yield return new WaitForSeconds(masterEase); 
		sliding = false; 
  
		StopCoroutine("slideMove"); 
	}

		public IEnumerator deathAni() {
		
		rb.velocity = Vector3.zero;
		rb.Sleep();
		GetComponent<Renderer>().material.color = Color.red;
		Sequence dying = DOTween.Sequence(); 
		dying.Append(kingBoi.DORotate(new Vector3(0,360,0),0.4f)); 
		dying.Join(kingBoi.DOScale(new Vector3(0,0,0),0.4f)); 
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
		// Destroy(marker);
		StopCoroutine("deathAni"); 
	}
		public IEnumerator winAni(){
		int scene = SceneManager.GetActiveScene().buildIndex; 
		rb.velocity = Vector2.zero;
		rb.Sleep();
		// goalText.SetActive(true); 
		heart.SetActive(true); 
		heart.transform.DOScale(new Vector3(0,0,0),0.3f).From(); 
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(scene+1); 
		StopCoroutine("winAni"); 
		}

	IEnumerator pushBack(){
		rb.AddForce (Vector3.left * 125f);
//		rb.AddForce (Vector3.down * moveForce);
		yield return new WaitForSeconds (0.5f); 
		rb.velocity = Vector3.zero; 
		StopCoroutine ("pushBack"); 
	}
	IEnumerator pushBack1(){	
		rb.AddForce (Vector3.right * 125f); 
		yield return new WaitForSeconds (0.5f);
		rb.velocity = Vector3.zero; 
		StopCoroutine ("pushBack1"); 
	}
	void OnCollisionEnter(Collision other){
		if(other.collider.CompareTag("Death")){
		StartCoroutine("deathAni"); 
		}else if(other.collider.CompareTag("Goal")){
		StartCoroutine("winAni"); 
		}
	}
	void  kSlide(){
	sliding = true;  
	if(facingRight){
	kingBoi.DORotate(new Vector3(-90,90,0),0.3f).SetEase(Ease.InOutCubic);
	// rb.AddForce(Vector3.forward * gravity * rb.mass); 
	// marker.SetActive(true); 
	}
	if(!facingRight){
	kingBoi.DORotate(new Vector3(0,180,0),0.3f).SetEase(Ease.InOutCubic).From();
	kingBoi.DORotate(new Vector3(90,90,0),0.3f).SetEase(Ease.InOutCubic); 
	// marker.SetActive(true);
		} 
	}
	void unSlide(){
		Sequence b = DOTween.Sequence(); 
		b.Append(kingBoi.DORotate(new Vector3(0,0,0),masterEase).SetEase(Ease.InOutCubic)); 
		// marker.SetActive(false); 
	}

	 void Flip(){
		facingRight = !facingRight;  
	}

	
}
//***UNUSED/OLD SHIT***//
// if(Input.GetButtonDown("Jump")){
// 		///**FILL WITH SLIDING STUFF**///
// 		slowDown = true; 
// 		kSlide(); 
// 		}else if(Input.GetButtonUp("Jump")){
// 		slowDown = false; 
// 		unSlide(); 
// 		}

//***KEEPING IN THE EVEN THAT I WANT TO CHANGE IT SO THAT THE SLIDE MOVE HAPPENS WITHOUT HOLDING THE SPACEBAR***//
	// void slideMove(){
	// Sequence a = DOTween.Sequence();
	// Sequence c = DOTween.Sequence(); 
	// if(facingRight){
	// a.Append(kingBoi.DORotate(new Vector3(-90,90,0),0.6f).SetEase(Ease.InOutCubic));
	// marker.SetActive(true);
	// }
	// if(!facingRight){
	// c.Append(kingBoi.DORotate(new Vector3(0,180,0),0.6f).SetEase(Ease.InOutCubic).From());
	// c.Join(kingBoi.DORotate(new Vector3(90,90,0),0.6f).SetEase(Ease.InOutCubic));
	// marker.SetActive(true);  
	// 	}
	// }
	//***KEEPING IN THE EVEN THAT I WANT TO CHANGE IT SO THAT THE SLIDE MOVE HAPPENS WITHOUT HOLDING THE SPACEBAR***//


	// void moving(){
	// 	float xPos = kingBoi.localPosition.x; 
	// 	float yPos = kingBoi.localPosition.y; 
	// 	float zPos = kingBoi.localPosition.z; 

	// 	if(Input.GetKeyDown(KeyCode.LeftArrow)){
	// 		anim.SetBool("Moving",true); 
	// 		duration += 0.1f; 
	// 		kingBoi.DOMove(new Vector3(xPos -= 1f, yPos,zPos),duration).SetEase(Ease.InOutCubic); 
	// 		if(facingRight){
	// 		kingBoi.DORotate(new Vector3(0,180,0), 0.3f).SetEase(Ease.InOutCubic); 
	// 			Flip(); 
	// 		}
	// 	}else if(Input.GetKeyDown(KeyCode.RightArrow)){
	// 		kingBoi.DOMove(new Vector3(xPos += 1f, yPos,zPos),duration).SetEase(Ease.InOutCubic).SetLoops(-1,LoopType.Restart); 
	// 		if(!facingRight){
	// 		kingBoi.DORotate(new Vector3(0,0,0), 0.3f).SetEase(Ease.InOutCubic); 
	// 			Flip(); 
	// 		}
	// 	}else{
	// 		anim.SetBool("Moving",false); 
	// 	}
	// }

	// public void FixedUpdate(){
		// 	moving(); 
		// }

//***UNUSED/OLD SHIT***//
