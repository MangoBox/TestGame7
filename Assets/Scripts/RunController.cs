using UnityEngine;
using System.Collections;

public class RunController : MonoBehaviour {

    //This class should be used for run physics, e.g. starting, ending a run, etc.
    //Class references
    public RoadGenerationManager roadGenerationManager;
    public LevelGenerationManager levelGenerationManager;
	public CarController carController;

    public GameObject playerCarReference;

    //Transform
    //This object is to be instantiated when a roadObject is generated.
    public Transform parentObject;
    //This should be a static object, which should be the primary parent.

	// Use this for initialization
	void Start () {
        BeginNewRun(playerCarReference);
	}

    //A method to be called primarily by CarController, used for calling ManageRequiredSectors().
    public void EnterNewSector(GameObject playerCar)
    {
        levelGenerationManager.ManageRequiredSectors(playerCar, parentObject);
    }

    //A global method to be called when a new run is created.
    public void BeginNewRun(GameObject playerCar)
    {
        //Creating a initial generation road, and assigning it to the current road object for the car.
		roadGenerationManager.GenerateNewRoadSector(levelGenerationManager.initialGenerationPoint.position, parentObject);
        EnterNewSector(playerCar);

    }
}
