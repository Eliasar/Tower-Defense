using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

    // NGUI
    /*public bool buildPanelOpen = false;
    public TweenPosition buildPanelTweener;
    public TweenRotation buildPanelArrowTweener;*/

    // Placement Plane Items
    public Transform placementPlanesRoot;
    public Material hoverMat;
    public LayerMask placementLayerMask;
    private Material originalMat;
    private GameObject lastHitObj;

    // Build selection items
    public bool isBuilding = false;
    public Color onColor;
    public Color offColor;
    public GameObject[] allStructures;
    public UISlicedSprite[] buildBtnGraphics;
    public int structureIndex;

	// Use this for initialization
	void Start () {
        structureIndex = 0;
        UpdateGUI();
	}
	
	// Update is called once per frame
	void Update () {

        // If able to build, cast ray to find space
        if (isBuilding) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Select new object, deselect old
            if (Physics.Raycast(ray, out hit, 1000, placementLayerMask)) {
                if (lastHitObj) {
                    lastHitObj.renderer.material = originalMat;
                }

                lastHitObj = hit.collider.gameObject;
                originalMat = lastHitObj.renderer.material;
                lastHitObj.renderer.material = hoverMat;
            } else {
                if (lastHitObj) {
                    lastHitObj.renderer.material = originalMat;
                    lastHitObj = null;
                }
            }

            // Place turret
            if (Input.GetMouseButtonDown(0) && lastHitObj) {
                if (lastHitObj.CompareTag("PlacementPlane_Open")) {
                    GameObject newStructure = Instantiate(allStructures[structureIndex],
                        lastHitObj.transform.position, Quaternion.identity) as GameObject;
                    lastHitObj.tag = "PlacementPlane_Taken";
                }
            }
        }
	}

    // Custom Functions

    void UpdateGUI() {
        // Turn all build buttons to off
        foreach (UISlicedSprite theBtnGraphic in buildBtnGraphics) {
            theBtnGraphic.color = offColor;
        }

        // Set selected build button to on
        if(isBuilding)
            buildBtnGraphics[structureIndex].color = onColor;
    }

    public void SetBuildChoice(GameObject btnObj) {
        string btnName = btnObj.name;
        print(btnName);
        isBuilding = true;

        if (btnName == "Missile Tower Button") {
            structureIndex = 0;
        } else if (btnName == "Laser Tower Button") {
            structureIndex = 1;
        } else if (btnName == "Cancel Build Button") {
            isBuilding = false;
        }

        // Display the Placement Panels
        if (isBuilding) {
            foreach (Transform thePlane in placementPlanesRoot) {
                thePlane.gameObject.renderer.enabled = true;
            }
        } else {
            foreach (Transform thePlane in placementPlanesRoot) {
                thePlane.gameObject.renderer.enabled = false;
            }
        }

        print("structureIndex = " + structureIndex + "; UpdateGUI() called.");
        UpdateGUI();
    }
}
