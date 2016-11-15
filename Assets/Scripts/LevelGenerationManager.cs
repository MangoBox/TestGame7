using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LevelGenerationManager : MonoBehaviour {

    /* This class should be used for level generation-related methods and calculations.
    */

	//One-time use variable, used for the first generation point.
    public Transform initialGenerationPoint;

    //Variables
    //PREFAB variable. RoadObject to generate.
    public GameObject roadObject;

	//How many road objects should be generated ahead and behind the camera, respectively.
    public int generateAheadInstances;
    public int generateBehindInstances;
    
    //Class references
    //RoadGenerationController Reference, intended for creating individual sectors.
    public RoadGenerationManager roadGenerationManager;
    public CarController carController;
    public CarManagement carManagement;

    //This method should be called whenever the player moves into a new sector, run begins, etc.
    //Generates ALL required sectors for playing.
    public void ManageRequiredSectors(GameObject player, Transform baseSectorParent)
	{
		//Generating ahead & behind sectors
        
		//Finding the index of the players road object. WIP
		int playerRoadIndex = roadGenerationManager.roadObjectSectorArray.IndexOf (player.GetComponent<CarController> ().currentRoadObject);
		//The total amount of times generation is required.
		int toGenerateAmount = generateBehindInstances + generateAheadInstances;
		//The roadObjectSector array index to begin generating from.
		int indexGenerateFrom = playerRoadIndex - generateBehindInstances;
		//The roadObjectSector array index to cease generating from.
		int indexGenerateTo = playerRoadIndex + generateAheadInstances;
		//A local, temporary dynamic array to store road objects generated this instance of method.
		//Used for testing what needs to be deleted.
		List<GameObject> roadObjectArray;
        for (int i = indexGenerateFrom; i <= indexGenerateTo; i++)
        {
            if (!roadGenerationManager.roadObjectSectorArray.Contains(roadGenerationManager.roadObjectSectorArray[i]))
            {
                GameObject instantiatedRoadObject = roadGenerationManager.GenerateNewRoadSector(getNextGenerationPoint(roadGenerationManager.roadObjectSectorArray[i - 1]).position,baseSectorParent);
				roadObjectArray.Add (roadGenerationManager.roadObjectSectorArray.IndexOf(instantiatedRoadObject));
            }
        }

		//For every total generated roadObject in the roadObjectSectorArray, this loop will see if the roadObject has generated in this instance of this method.
		//If not (it was generated sometime else and thus is no longer needed), the roadObject is destroyed.
        foreach (GameObject roadObject in roadGenerationManager.roadObjectSectorArray)
        {
			if(!roadObjectArray.Contains(roadObject)) {
				Destroy (roadObject);
			}
        }
    }


    //2 methods for finding the next generation & append points of the parameter roadObject.
    public Transform getNextGenerationPoint(GameObject roadObject)
    {
        return roadObject.transform.FindChild("GenerationPoint");
    }

    public Transform getNextAppendPoint(GameObject roadObject)
    {
        return roadObject.transform.FindChild("AppendPoint");
    }

    //Gets the roadObject that the car is currently in.
    public GameObject getRoadObjectByCar(GameObject car) {
        return car.GetComponent<CarController>().currentRoadObject;
    }


}
