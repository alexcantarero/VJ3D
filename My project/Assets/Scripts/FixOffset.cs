using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOffset : MonoBehaviour
{
    public Transform offsetToFix;
    public Vector3 desiredLocalPosition;
    public Vector3 desiredLocalRotation;

    void Start()
    {

        if (offsetToFix != null)
        {
            offsetToFix.localPosition = desiredLocalPosition;
            offsetToFix.localEulerAngles = desiredLocalRotation;
            Debug.Log("Rotation applied: " + offsetToFix.localEulerAngles);

        }
    }
}
