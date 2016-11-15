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

    //This is a dynamic array for runtime road objects. Should not be modified outside of runtime.
    public List<GameObject> roadObjectSectorArray;

	public LevelGenerationManager levelGenerationManager;

    //Below are methods specifically for selecting random assets for generation.
    GameObject SelectRandomRoad()
    {
        return roadAssetArray[Random.Range(0, roadAssetArray.Length)];
    }

    public GameObject GenerateNewRoadSector(Vector3 pos, Transform parentObject) {
        GameObject instantiatedRoadBase = (GameObject) Instantiate(new GameObject(), pos, Quaternion.Euler(0, 0, 0));
        GameObject instantiatedRoadObject = (GameObject) Instantiate(SelectRandomRoad(), pos, Quaternion.Euler(-90,0,0));

        //Parents the main road object to the instantiatedRoadBase for future parenting of other objects.
        instantiatedRoadObject.transform.SetParent(instantiatedRoadBase.transform, true);
        //Parents the entire object to the Heirarchy object to avoid clogging.
        instantiatedRoadBase.transform.SetParent(parentObject);

		//WIP - Since the origin point of the road is in the exact centre of the mesh, and we are trying to append it on the end, this sets the position to its own generationPoint.
		instantiatedRoadBase.transform.position = levelGenerationManager.getNextGenerationPoint (instantiatedRoadBase).position;

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
