using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridOrganizer : MonoBehaviour
{
    public static GridOrganizer instance;

    [SerializeField]
    GameObject ResourceSlotPrefab;
    GridLayoutGroup gridLayout;
    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);
    [SerializeField]
    List<ResourceSlotController> resourceSlots;
    [SerializeField]
    public ResourceSlotState slotState;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.y;

        slotState = ResourceSlotState.ExtractMode;
        
        int numCells = GridDimensions.x * GridDimensions.y;

        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(ResourceSlotPrefab, this.transform);
        }

        for(int i = 0; i < numCells; i++)
        {
            resourceSlots.Add(transform.GetChild(i).GetComponent<ResourceSlotController>());
        }
        Debug.Log("Generated Item Slots");
    }

    public void ToggleResourceState()
    {
        if(slotState == ResourceSlotState.ExtractMode)
        {
            slotState = ResourceSlotState.ScanMode;
        }
        else
        {
            slotState = ResourceSlotState.ExtractMode;
        }
    }
}
