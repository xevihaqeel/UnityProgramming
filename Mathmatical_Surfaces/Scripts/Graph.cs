using UnityEngine;
using static UnityEngine.Mathf;

public class Graph : MonoBehaviour {
    [SerializeField]
    Transform pointPrefab;
    //create an array of Transforms for later use   
    Transform[] points;
    float duration;

    bool transitioning;

    FunctionLibrary.FunctionName transitionFunction;


    [SerializeField, Range(10,200)]
    int resolution = 10;

    //creates field that allows for the function to be changed

	[SerializeField]
	FunctionLibrary.FunctionName function;

    public enum TransitionMode {Cycle, Random}

    [SerializeField]
    TransitionMode transitionMode;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;


    void Awake(){

        //keeps the size of the cubes within the 
        float step = 2f/resolution;
       	// var position = Vector3.zero * step;
		var scale = Vector3.one * step;
        
        points = new Transform[resolution * resolution];
        //this loop explicitly keeps track fo the x and y coordinates
		for (int i = 0; i < points.Length; i++) {

            //assign the instantiated points to be the points designated by loop/array
			Transform point = points[i] = Instantiate(pointPrefab);
			point.localScale = scale;
            //ensures that created points remain children of the object holding the script creating them
            point.SetParent(transform,false);
		}
    }

    void Update(){
        duration += Time.deltaTime;
        if(transitioning){
            if(duration >= transitionDuration){
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if (duration >= functionDuration){
            duration -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            PickNextFunction();
        }
        if(transitioning){
            UpdateFunctionTransition();
        }
        else{
            UpdateFunction();
        }
    }


    void PickNextFunction(){
        function = transitionMode == TransitionMode.Cycle ?
        FunctionLibrary.GetNextFunctionName(function) : 
        FunctionLibrary.GetRandomFunctionNameOtherThan(function);
    }
    void UpdateFunction(){
        
		FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
    
        float time = Time.time;
        float step = 2f/resolution;

        //run through the point array and set position for each point
        float v = 0.5f * step - 1f;
        for(int i = 0, x = 0, z = 0; i < points.Length; i++, x++){
            //checks if coordinate is equal to resolution; if so, it resets it to zero 
            if(x == resolution){
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }

    }

      void UpdateFunctionTransition(){
        
		FunctionLibrary.Function from = FunctionLibrary.GetFunction(transitionFunction),
            to = FunctionLibrary.GetFunction(function);
        float progress = duration / transitionDuration;
        float time = Time.time;
        float step = 2f/resolution;

        //run through the point array and set position for each point
        float v = 0.5f * step - 1f;
        for(int i = 0, x = 0, z = 0; i < points.Length; i++, x++){
            //checks if coordinate is equal to resolution; if so, it resets it to zero 
            if(x == resolution){
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = FunctionLibrary.Morph(
                u, v, time, from, to, progress
            );
        }

    }

 
}   