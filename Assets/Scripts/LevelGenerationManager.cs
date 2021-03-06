﻿using UnityEngine;
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

    //An array for storing past destroyed road objects, keeping them as transforms - WIP.
    public Transform parentDestroyedSector;
    
    //Class references
    //RoadGenerationController Reference, intended for creating individual sectors.
    public RoadGenerationManager roadGenerationManager;
    public CarController carController;
    public CarManagement carManagement;

    //A transform array for destroyed objects. Before destroying, a reminant transform is left for future reference.
    public List<Transform> transformSectorArray;

    public int roadObjectSignature = 1;


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
        //An array for inserting road objects generated this instance of the method.
        List<GameObject> roadObjectArray = new List<GameObject>();

        //Does not generate objects (run for loop) if index is negative.
        for (int i = Mathf.Max(indexGenerateFrom, 1); i < indexGenerateTo; i++)
        {
            //This function ensures that the index MUST be either 0 or a positive index.
            int a = Mathf.Max(0, i);
            //This sets positiveCondition to true of a is NOT equal to zero.
            bool positiveCondition = !(a == 0);
			Debug.Log (a - 1);

            //Instantiates the road object itself. Also checks that the index of the array is not zero since that would throw an error, AND checks that the referenced gameobject is not null.
            if (roadGenerationManager.roadObjectSectorArray[a - 1] != null)
            {
                //Creates a reference to the RoadBaseController, which is the previously instantiated road object.
                RoadBaseController roadObjectController = roadGenerationManager.roadObjectSectorArray[a - 1].GetComponent<RoadBaseController>();
                GameObject instantiatedRoadObject = roadGenerationManager.GenerateNewRoadSector(positiveCondition ? roadObjectController.LocalGenerationPoint.transform : initialGenerationPoint.transform, baseSectorParent);
                roadObjectArray.Add(instantiatedRoadObject);
            }
            //Adds the road object to the instantiated array for future destroying.
            
        }

        //For every total generated roadObject in the roadObjectSectorArray, this loop will see if the roadObject has generated in this instance of this method.
		//If not (it was generated sometime else and thus is no longer needed), the roadObject is destroyed.
        foreach (GameObject roadObject in roadGenerationManager.roadObjectSectorArray)
        {
			if(!roadObjectArray.Contains(roadObject)) {
                //GameObject destroyedTransform = (GameObject) Instantiate(new GameObject(), roadObject.GetComponent<RoadBaseController>().LocalGenerationPoint.position, Quaternion.Euler(0, 0, 0));
               // destroyedTransform.transform.SetParent(parentDestroyedSector);
                //transformSectorArray.Add(destroyedTransform.transform);
                Destroy (roadObject);
			}
        }

        //This ensures that the array is OK for next method use, and not full of roads that we have already used.
        roadObjectArray.Clear();
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
