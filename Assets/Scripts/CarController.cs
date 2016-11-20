using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    //This class should be used for the physics and controls of the car.
    public GameObject currentRoadObject;

    public RunController runController;
    //Can be used for external retreival of the GameObject.
    public GameObject getCarObject()
    {
        return this.gameObject;
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}


    //Upon entering a road trigger collider, it sets the global currentRoadObject variable for access from other classes.
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "RoadDetectionCollider")
        {
            currentRoadObject = other.gameObject;
        }
        runController.EnterNewSector(this.gameObject);

    }
}
