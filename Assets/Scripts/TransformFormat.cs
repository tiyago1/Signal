using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TransformFormat : MonoBehaviour
{

    public float pixelsToUnits = 200;
    private Camera camera;

    void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        camera.orthographicSize = Screen.height / pixelsToUnits / 2;
    }
}
