using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    GameObject go_NumberOfExtracts, go_NumberOfScans, go_CollectedResources;
    GameObject go_MessageBar;
    [SerializeField, TextArea(5, 5)]
    string gameStartMessage = "";

    [SerializeField]
    public int NumberOfExtractTurns = 3;
    [SerializeField]
    public int NumberOfScanTurns = 6;
    int CollectedResources = 0;

    public GameObject endButton;
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
        go_MessageBar.GetComponent<TextMeshProUGUI>().text = gameStartMessage;
    }

    void Update() 
    {
        if(NumberOfExtractTurns == 0) GameOverMessage();
    }
    

    // Add Resources to total score
    public void AddCollectedResources(int ResourceAdded)
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ScanMode) return;
        if(NumberOfExtractTurns == 0) return;

        DisplayMessage("You acquired " + ResourceAdded + " points");
        Debug.Log(ResourceAdded);
        CollectedResources += ResourceAdded;
        go_CollectedResources.GetComponent<Text>().text = CollectedResources.ToString();        
    }

    // Decrease number of extract clicks left
    public void DecreaseExtractTurns()
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ScanMode) return;
        if(NumberOfExtractTurns == 0) return;


        Debug.Log("Decreasing extract turns");
        NumberOfExtractTurns--;
        go_NumberOfExtracts.GetComponent<Text>().text = NumberOfExtractTurns.ToString();
    }

    // Decrease number of Scan clicks left
    public void DecreaseScanTurns()
    
    {
        if(GridOrganizer.instance.slotState == ResourceSlotState.ExtractMode) return;
        if(NumberOfScanTurns == 0) return;


        Debug.Log("Decreasing scan turns");
        NumberOfScanTurns--;
        go_NumberOfScans.GetComponent<Text>().text = NumberOfScanTurns.ToString();
    }

    public void DisplayMessage(string message)
    {
        go_MessageBar.GetComponent<TextMeshProUGUI>().text = message;
    }

    public void GameOverMessage()
    {
        go_MessageBar.GetComponent<TextMeshProUGUI>().text = "Game Over.\nYou have acquired " + CollectedResources + " resources.";
        endButton.SetActive(true);
    }
}