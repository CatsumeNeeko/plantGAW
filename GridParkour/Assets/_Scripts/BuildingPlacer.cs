using System.Collections;
using System.Collections.Generic;
using PBuild;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
public GameObject buildingPrefab;
    public LayerMask gridLayer;
    public Camera mainCamera;
    [SerializeField] private GameObject lastHighlighted;
    private GameObject currentBuildingInstance;
    private Quaternion currentRotation = Quaternion.identity;

    void Update()
    {
        HighlightCell();
        HandleRotationInput();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    void HighlightCell()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridLayer))
        {
            if (lastHighlighted != null)
            {
                // Remove highlight from the last cell
                lastHighlighted.GetComponent<Renderer>().material.color = Color.white;
            }

            lastHighlighted = hit.collider.gameObject;
            // Highlight the new cell
            lastHighlighted.GetComponent<Renderer>().material.color = Color.yellow;

            // Update the position of the building instance to follow the highlighted cell
            if (currentBuildingInstance != null)
            {
                currentBuildingInstance.transform.position = lastHighlighted.transform.position;
            }
        }
    }

    void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation *= Quaternion.Euler(0, 90, 0); // Rotate 90 degrees around the Y axis
            if (currentBuildingInstance != null)
            {
                currentBuildingInstance.transform.rotation = currentRotation;
            }
        }
    }

    void PlaceBuilding()
    {
        if (lastHighlighted != null)
        {
            BuiltOn builtOn = lastHighlighted.GetComponent<BuiltOn>();
            if (builtOn != null && builtOn._builtOn == false)
            {
                Vector3 position = lastHighlighted.transform.position;
                currentBuildingInstance = Instantiate(buildingPrefab, position, currentRotation);
                builtOn._builtOn = true;

                // Reset current building instance to allow placing a new one
                currentBuildingInstance = null;
                currentRotation = Quaternion.identity;
            }
            else
            {
                Debug.Log("Built on");
            }
        }
    }

    void StartPlacingNewBuilding()
    {
        if (currentBuildingInstance == null)
        {
            currentBuildingInstance = Instantiate(buildingPrefab);
            currentBuildingInstance.transform.rotation = currentRotation;
        }
    }
}
