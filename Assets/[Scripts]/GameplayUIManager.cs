using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    GameObject NumberOfExtracts, NumberOfScans, CollectedResources;
    GameObject MessageBar;

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
                NumberOfExtracts = t.gameObject;
            }
            else if(t.gameObject.name == "NumberOfScans")
            {
                NumberOfScans = t.gameObject;
            }
            else if(t.gameObject.name == "CollectedResources")
            {
                CollectedResources = t.gameObject;
            }
            else if(t.gameObject.name == "MessageBar")
            {
                MessageBar = t.gameObject;
            }
        }
    }

    public void DecreaseExtractTurns()
    {
        Debug.Log("Decreasing extract turns");
    }
}
