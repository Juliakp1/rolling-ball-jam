using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class sineRotation : MonoBehaviour
{
    void Start() {
        transform.Rotate(0, 0, -5);
    }

    void Update() {
        float rotation = Mathf.Sin(Time.time)/20;
        transform.Rotate(0, 0, rotation);
    }
}
