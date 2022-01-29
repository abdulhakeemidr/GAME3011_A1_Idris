using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOrganizer : MonoBehaviour
{
    [SerializeField]
    GameObject ResourceSlotPrefab;
    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);
    void Start()
    {
        int numCells = GridDimensions.x * GridDimensions.y;

        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(ResourceSlotPrefab, this.transform);
        }
        Debug.Log("Generated Item Slots");
    }

}
