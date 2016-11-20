using UnityEngine;
using System.Collections;

public class RoadBaseController : MonoBehaviour {

    //This class should be NOT be used for calculations, important methods, etc.
    //It should, however, be used for local references for other classes.

    //These variables should not be referenced from by other classes, instead use the two below methods to do that.
    public Transform LocalGenerationPoint;
    public Transform LocalAppendPoint;

    //Both of the below methods are for returning local append & generation points, for future road generation.
    public Transform GetRoadGenerationPoint()
    {
        return LocalGenerationPoint;
    }

    public Transform GetRoadAppendPoint()
    {
        return LocalAppendPoint;
    }
}
