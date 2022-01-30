using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    GameObject go_NumberOfExtracts, go_NumberOfScans, go_CollectedResources;
    GameObject go_MessageBar;

    [SerializeField]
    public int NumberOfExtractTurns = 3;
    [SerializeField]
    int NumberOfScanTurns = 6;
    int CollectedResources = 0;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach(Transform t in allChildren)
        {
            if(t.gameObject.name == "NumberOfExtracts")
            {
                go_NumberOfExtracts = t.gameObject;
            }
            else if(t.gameObject.name == "NumberOfScans")
            {
                go_NumberOfScans = t.gameObject;
            }
            else if(t.gameObject.name == "CollectedResources")
            {
                go_CollectedResources = t.gameObject;
            }
            else if(t.gameObject.name == "MessageBar")
            {
                go_MessageBar = t.gameObject;
            }
        }

        go_NumberOfExtracts.GetComponent<Text>().text = NumberOfExtractTurns.ToString();
        go_NumberOfScans.GetComponent<Text>().text = NumberOfScanTurns.ToString();
        go_CollectedResources.GetComponent<Text>().text = 0.ToString();
    }

    public void AddCollectedResources(int ResourceAdded)
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ScanMode) return;
        if(NumberOfExtractTurns == 0) return;


        Debug.Log(ResourceAdded);
        CollectedResources += ResourceAdded;
        go_CollectedResources.GetComponent<Text>().text = CollectedResources.ToString();        
    }

    public void DecreaseExtractTurns()
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ScanMode) return;
        if(NumberOfExtractTurns == 0) return;


        Debug.Log("Decreasing extract turns");
        NumberOfExtractTurns--;
        go_NumberOfExtracts.GetComponent<Text>().text = NumberOfExtractTurns.ToString();
    }

    public void DecreaseScanTurns()
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ExtractMode) return;
        if(NumberOfScanTurns == 0) return;
    }
}
