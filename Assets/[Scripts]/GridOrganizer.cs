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
    List<List<ResourceSlotController>> resourceSlots;
    public List<List<ResourceSlotController>> ResourceSlots {get;}
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

        resourceSlots = new List<List<ResourceSlotController>>();

        slotState = ResourceSlotState.ExtractMode;
        

        int numCells = GridDimensions.x * GridDimensions.y;
        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(ResourceSlotPrefab, this.transform);
        }

        int cellCount = 0;
        for(int i = 0; i < GridDimensions.x; i++)
        {
            resourceSlots.Add(new List<ResourceSlotController>());
            for(int j = 0; j < GridDimensions.y; j++)
            {
                resourceSlots[i].Add(transform.GetChild(cellCount).GetComponent<ResourceSlotController>());
                cellCount++;
            }
        }
        Debug.Log("Generated Item Slots");
    }


    // This function is called by the toggle button UI
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
