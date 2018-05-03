using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtil {

    // public static Vector3 CreateVector3(float x, float y, float z) {
        
    //    }

    public static Vector3 CombineVectorWithZ(Vector2 vector, float z) {
        Vector3 result = vector;

        result.z = z;

        return result;
    }

}