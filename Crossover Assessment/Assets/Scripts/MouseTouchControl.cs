using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTouchControl : MonoBehaviour
{
    private Camera mainCamera;

    private void Start () {
        mainCamera = Camera.main;
    }

    private void Update () {
        if (Input.GetMouseButtonDown(1)) {
            // Convert mouse position to a ray
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // Check if the raycast hits a GameObject
                GameObject hitObject = hit.collider.gameObject;

                // Do something with the hit object
                Debug.Log("Hit object: " + hitObject.name);
                if (hitObject.tag == "JengaPiece") {
                    hitObject.GetComponent<JengaPiece>().OpenAdditionalInfo();
                }
            }
        }
    }
}
