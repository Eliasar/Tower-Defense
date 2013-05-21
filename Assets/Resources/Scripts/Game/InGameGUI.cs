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

    // Score, cash, wave, lives labels
    private GameObject _gameMaster;
    public UILabel scoreCashLabel;
    public UILabel livesWaveLabel;

    // Tower Information window
    public bool isTowerInfoWindowVisible;
    public UIPanel towerInformationWindow;
    public LayerMask towerCompassMask;
    public LayerMask twoDGUIMask;
    public GameObject towerHovered;
    public GameObject towerSelected;

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
                    Instantiate(allStructures[structureIndex],
                        lastHitObj.transform.position, Quaternion.identity);
                    lastHitObj.tag = "PlacementPlane_Taken";
                }
            }
        } else {

            // Cast ray
            Ray towerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit towerRaycastHit;

            if (Physics.Raycast(towerRay, out towerRaycastHit, 1000, towerCompassMask)) {
                towerHovered = towerRaycastHit.collider.gameObject;
            }
            else {
                towerHovered = null;
            }

            if (Input.GetMouseButtonDown(0) && towerHovered) {
                towerSelected = towerHovered.transform.parent.gameObject;

                string type;
                int level;
                int exp;

                type = towerSelected.GetComponent<Tower>().type;
                level = towerSelected.GetComponent<Tower>().level;
                exp = towerSelected.GetComponent<Tower>().experience;

                towerInformationWindow.transform.FindChild("Type").GetComponent<UILabel>().text = "Type: " + type;
                towerInformationWindow.transform.FindChild("Level").GetComponent<UILabel>().text = "Level: " + level;
                towerInformationWindow.transform.FindChild("Exp").GetComponent<UILabel>().text = "Exp: " + exp;

                SetTowerInfoWindow(true);
            }

            /*if (Physics.Raycast(towerRay, out towerRaycastHit, twoDGUIMask)) {
                if (Input.GetMouseButtonDown(0)) {
                    //SetTowerInfoWindow(false);
                    print("Clicked inside 2dGUI");
                }
            }*/

            /*else if (Input.GetMouseButtonDown(0) && !towerSelected) {
                SetTowerInfoWindow(false);
            }*/
        }

        // Update Labels
        scoreCashLabel.text = "Score: " + _gameMaster.GetComponent<Game>().score +
                              "\nCash: $" + _gameMaster.GetComponent<Game>().cash;

        livesWaveLabel.text = "Lives: " + _gameMaster.GetComponent<Game>().lives +
                              "\nWave: " + _gameMaster.GetComponent<Game>().currentWave;

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
        isBuilding = true;

        if (btnName == "Missile Tower Button") {
            structureIndex = 0;
        } else if (btnName == "Laser Tower Button") {
            structureIndex = 1;
        } else if (btnName == "Flame Tower Button") {
            structureIndex = 2;
        } else if (btnName == "Cancel Build Button") {
            isBuilding = false;
        }

        // Display the Placement Panels
        if (isBuilding) {
            foreach (Transform thePlane in placementPlanesRoot) {
                if(thePlane.CompareTag("PlacementPlane_Open"))
                    thePlane.gameObject.renderer.enabled = true;
            }
        } else {
            foreach (Transform thePlane in placementPlanesRoot) {
                thePlane.gameObject.renderer.enabled = false;
            }
        }

        UpdateGUI();
    }

    // Sets the visibility of the Tower Information Window
    public void SetTowerInfoWindow(bool set = false) {
        //isTowerInfoWindowVisible = !isTowerInfoWindowVisible;
        isTowerInfoWindowVisible = set;

        NGUITools.SetActive(towerInformationWindow.gameObject, isTowerInfoWindowVisible);
    }

    // Sets the visibility of the Tower Information Window
    public void ToggleTowerInfoWindow() {
        isTowerInfoWindowVisible = !isTowerInfoWindowVisible;
        //isTowerInfoWindowVisible = set;

        NGUITools.SetActive(towerInformationWindow.gameObject, isTowerInfoWindowVisible);
    }

    // Set the targetting type of the tower selected
    // Called from radio buttons in the 2DGUI
    public void SetTargetType(bool btnObj) {
        if (btnObj) {
            print(UICheckbox.current.name + " selected.");
            towerSelected.GetComponent<Tower>().type = UICheckbox.current.name;
        }
    }
}
