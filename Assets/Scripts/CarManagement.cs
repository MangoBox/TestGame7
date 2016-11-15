using UnityEngine;
using System.Collections;

public class CarManagement : MonoBehaviour {


    public GameObject[] carAssetArray;
    public GameObject baseCarAsset;

    //This variable is for global reference by any class. Should not be modified outside of run start.
    public GameObject carReference;
	// Use this for initialization
	
    //This class should be used for asset management of the cars.
    
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //A getter method to return a GameObject of the array of cars - assets. Most likely used for instantiating.
    //This method is not complete, an out of index query will throw an error.
    public GameObject GetCarInAssets(int selectedCar)
    {
        return carAssetArray[selectedCar];
    }

    //Intended for instantiating cars into the game, together with the GetCarInAssets method.
    public void InstantiateCar(GameObject targetCar, Vector3 pos)
    {
        //Instantiates the base car, this is the parent game object to parent to - No mesh or physics
        GameObject instantiatedBaseCar = (GameObject) Instantiate(baseCarAsset, pos, Quaternion.Euler(0,0,0));

        //Instantiates the car mesh, which contains physics and whatnot.
        GameObject instantiatedCarMesh = (GameObject) Instantiate(targetCar, pos, Quaternion.Euler(0,0,0));

        //Sets the parent of the instantiated car to the base car object.
        instantiatedCarMesh.transform.SetParent(instantiatedBaseCar.transform, false);
        //NOT COMPLETE

    }
}
