using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraBoundGenerator : MonoBehaviour {
    public Camera sceneCamera;
    public float worldBounds;
    public GameObject boundTestObject;

    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider>();

        float cameraHeight = sceneCamera.orthographicSize * 2;
        float cameraWidth = sceneCamera.aspect * cameraHeight;

        Debug.Log("Camera Height in world space is " + cameraHeight);
        Debug.Log("Camera Width in world space is " + cameraWidth);

        boxCollider.size = new Vector3(worldBounds - cameraWidth, worldBounds - cameraHeight, 1);

        if (boundTestObject != null) {
            boundTestObject.transform.localScale = new Vector3(cameraWidth - 1, cameraHeight - 1, 1);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
