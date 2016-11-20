using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoadGenerationManager : MonoBehaviour {

    //This class is intended for the individual generation of road objects.

    //These arrays should be used for storing possible road generation objects - asset prefabs.
    public GameObject[] roadAssetArray;
    public GameObject[] houseAssetArray;
    public GameObject[] fenceAssetArray;
    public GameObject[] letterboxAssetArray;

    //This should be used for the parent of the road objects. Empty transform.
    public GameObject roadParent;

    //This is a dynamic array for runtime road objects. Should not be modified outside of runtime.
    public List<GameObject> roadObjectSectorArray;

	public LevelGenerationManager levelGenerationManager;

    //Below are methods specifically for selecting random assets for generation.
    GameObject SelectRandomRoad()
    {
        //We should not need to worry about out of index array exceptions, as we only referencing to a static length.
        return roadAssetArray[Random.Range(0, roadAssetArray.Length)];
    }

    public GameObject GenerateNewRoadSector(Transform objectTransform, Transform parentObject) {
        GameObject instantiatedRoadBase = (GameObject) Instantiate(roadParent, objectTransform.position, Quaternion.Euler(0, 0, 0));
        GameObject instantiatedRoadObject = (GameObject) Instantiate(SelectRandomRoad(), objectTransform.position, Quaternion.Euler(-90,0,0));

        //This area finds the append & generation points for the road object, then sets the variables in the road base.
        RoadBaseController roadBaseController = instantiatedRoadBase.GetComponent<RoadBaseController>();
        roadBaseController.LocalAppendPoint = levelGenerationManager.getNextAppendPoint(instantiatedRoadObject);
        roadBaseController.LocalGenerationPoint = levelGenerationManager.getNextGenerationPoint(instantiatedRoadObject);

        //Parents the main road object to the instantiatedRoadBase for future parenting of other objects.
        instantiatedRoadObject.transform.SetParent(instantiatedRoadBase.transform, true);
        //Parents the entire object to the Heirarchy object to avoid clogging.
        instantiatedRoadBase.transform.SetParent(parentObject);

		//WIP - Since the origin point of the road is in the exact centre of the mesh, and we are trying to append it on the end, this sets the position to its own generationPoint.
		instantiatedRoadBase.transform.position = levelGenerationManager.getNextGenerationPoint (instantiatedRoadObject).position;

        //Adds the road object to the runtime generated array, for future destroying and referencing.
        //WIP - May add object to empty indexes at beginning of array, WE DON'T WANT THIS
		roadObjectSectorArray.Add(instantiatedRoadBase); 
		return instantiatedRoadBase;
    }
    
    //Must reference to the parent of the sector, NOT the parent of the entire array - the organisation object
    public void DestroySector(List<GameObject> roadObjectArray, int index)
    {
        Destroy(roadObjectArray[index]);
    }


}
