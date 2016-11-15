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

    //Below are methods specifically for selecting random assets for generation.
    GameObject SelectRandomRoad()
    {
        return roadAssetArray[Random.Range(0, roadAssetArray.Length)];
    }

    public void GenerateNewRoadSector(Vector3 pos, Transform parentObject) {
        GameObject instantiatedRoadBase = (GameObject) Instantiate(new GameObject(), pos, Quaternion.Euler(0, 0, 0));
        GameObject instantiatedRoadObject = (GameObject) Instantiate(SelectRandomRoad(), pos, Quaternion.Euler(-90,0,0));

        //Parents the main road object to the instantiatedRoadBase for future parenting of other objects.
        instantiatedRoadObject.transform.SetParent(instantiatedRoadBase.transform, true);
        //Parents the entire object to the Heirarchy object to avoid clogging.
        instantiatedRoadBase.transform.SetParent(parentObject);

        //Adds the road object to the runtime generated array, for future destroying and referencing.
        //Its worth noting that since arrays are 0 based and array.length is not, we are referencing to the next element.
        roadObjectSectorArray[roadObjectSectorArray.FindLastIndex(0, )] = instantiatedRoadBase; 
    }
    
    //Must reference to the parent of the sector, NOT the parent of the entire array - the organisation object
    void DestroySector(List<GameObject> roadObjectArray, int index)
    {
        Destroy(roadObjectArray[index]);
    }


}
