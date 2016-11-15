using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelGenerationManager : MonoBehaviour {

    /* This class should be used for level generation-related methods and calculations.
    */

    public Transform initialGenerationPoint;

    //Variables
    //PREFAB variable. RoadObject to generate.
    public GameObject roadObject;

    private int playerRoadIndex;
    private int toGenerateAmount;

    public int generateAheadInstances;
    public int generateBehindInstances;

    private int indexReference;
    
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
        playerRoadIndex = roadGenerationManager.roadObjectSectorArray.IndexOf(player.GetComponent<CarController>().currentRoadObject);
        toGenerateAmount = generateBehindInstances + generateAheadInstances;

        for (int i = 0; i < toGenerateAmount; i++)
        {
            if (!roadGenerationManager.roadObjectSectorArray.Contains(roadGenerationManager.roadObjectSectorArray[i]))
            {
                roadGenerationManager.GenerateNewRoadSector(getNextGenerationPoint(roadGenerationManager.roadObjectSectorArray[i - 1]).position,baseSectorParent);
            }
        }

        foreach (GameObject roadObject in roadGenerationManager.roadObjectSectorArray)
        {
            
        }

        //Destroying ahead & behind sectors
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
