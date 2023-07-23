using UnityEngine;

public class Graph : MonoBehaviour {
    [SerializeField]
    Transform pointPrefab;
    //create an array of Transforms for later use   
    Transform[] points;


    [SerializeField, Range(10,100)]
    int resolution = 10;

    //creates field that allows for the function to be changed
    [SerializeField, Range(0,2)]
    int function;

    void Awake(){

        //keeps the size of the cubes within the 
        float step = 2f/resolution;
       	var position = Vector3.zero * step;
		var scale = Vector3.one * step;
        
        //sets the array value to the the designated float
        points = new Transform[resolution];

		for (int i = 0; i < points.Length; i++) {
            //assign the instantiated points to be the points designated by loop/array
			Transform point = points[i] = Instantiate(pointPrefab);
            // points[i] = point; 
			position.x = (i + 0.5f) * step - 1f;

            // position.y = Mathf.Pow(position.x,3f);
			point.localPosition = position;
			point.localScale = scale;
            //ensures that created points remain children of the object holding the script creating them
            point.SetParent(transform,false);
		}
    }

    void Update(){
        //run through the point array and set position for each point
        float time = Time.time;
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            if(function == 0){
            position.y = FunctionLibrary.Wave(position.x, time);
            }
            else if(function == 1){
                position.y = FunctionLibrary.MultiWave(position.x,time);
            }
            else{
                position.y = FunctionLibrary.Ripple(position.x, time); 
            }
            point.localPosition = position;
        }
    }
}   