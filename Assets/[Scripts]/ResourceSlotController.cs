using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PeripheralSlot
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT,
    TOPLEFT,
    TOPRIGHT,
    BOTTOMLEFT,
    BOTTOMRIGHT
}


public static class ResourceType
{
    public const int MaxResource = 5000;
    public const int HalfResource = 2500;
    public const int QuarterResource = 1250;
    public const int EmptyResource = 0;
    
}

public class ResourceSlotController : MonoBehaviour
{
    [SerializeField]
    private int m_ResourceScore;
    [SerializeField]
    Color m_ScoreColor = new Color();
    bool isExtracted = false;
    bool isScanned = false;

    [HideInInspector]
    public Image m_resourceSlotColor;
    Button m_resourceSlotButton;


    [SerializeField]
    Color hiddenSlotColor = new Color(0.16f, 0, 0, 1f);
    [SerializeField]
    Color EmptySlotColor = new Color(0, 0, 0, 1f);
    [SerializeField]
    Color QuarterSlotColor = new Color();
    [SerializeField]
    Color HalfSlotColor = new Color();
    [SerializeField]
    Color MaxSlotColor = new Color();
    [SerializeField]
    Color ExtractedSlotColor = new Color();

    void Start()
    {
        m_resourceSlotColor = GetComponent<Image>();
        m_resourceSlotButton = GetComponent<Button>();
        
        m_resourceSlotButton.onClick.AddListener(ExtractResource);
        m_resourceSlotButton.onClick.AddListener(ScanResource);
        /* m_resourceSlotButton.onClick.
        AddListener(
            delegate
            {
                GameplayUIManager.instance.AddCollectedResources(m_ResourceScore); 
            }
        ); */
        // Update Extract turns UI
        //m_resourceSlotButton.onClick.AddListener(GameplayUIManager.instance.DecreaseExtractTurns);
        //m_resourceSlotButton.onClick.AddListener(GameplayUIManager.instance.DecreaseScanTurns);

        m_resourceSlotColor.color = hiddenSlotColor;
        int[] RandomResource = new int[] 
        {
            ResourceType.MaxResource, 
            ResourceType.HalfResource, 
            ResourceType.QuarterResource, 
            ResourceType.EmptyResource
        };
        m_ResourceScore = RandomResource[Random.Range(0, 4)];

        SetResourceColor();
        //Debug_ToggleResourceColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug_ToggleResourceColor();
        }
    }

    void SetResourceColor()
    {
        switch(m_ResourceScore)
        {
            case ResourceType.EmptyResource:
            m_ScoreColor = EmptySlotColor;
            break;
            case ResourceType.QuarterResource:
            m_ScoreColor = QuarterSlotColor;
            break;
            case ResourceType.HalfResource:
            m_ScoreColor = HalfSlotColor;
            break;
            case ResourceType.MaxResource:
            m_ScoreColor = MaxSlotColor;
            break;
        }
    }

    void Debug_ToggleResourceColor()
    {
        if(isExtracted == true) return;
        if(isScanned == true) return;

        if(m_resourceSlotColor.color == hiddenSlotColor)
        {
            m_resourceSlotColor.color = m_ScoreColor;
        }
        else
        {
            m_resourceSlotColor.color = hiddenSlotColor;
        }
    }

    void ExtractResource()
    {
        if(GridOrganizer.instance.slotState != ResourceSlotState.ExtractMode)
        {
            Debug.Log("This is not extract mode");
            return;
        }

        if(isExtracted == true) return;

        if(GameplayUIManager.instance.NumberOfExtractTurns == 0)
        {
            Debug.Log("Game Over");
            return;
        }

        // Displays color
        m_resourceSlotColor.color = m_ScoreColor;
        // Dims the color to 1/4th after displaying to represent it is extracted
        //m_resourceSlotColor.color /= 4f;
        float dim = 0.4f;
        m_resourceSlotColor.color *= new Color(dim, dim, dim, 1f);
        isExtracted = true;
        m_resourceSlotButton.enabled = false;
        GameplayUIManager.instance.AddCollectedResources(m_ResourceScore);
        GameplayUIManager.instance.DecreaseExtractTurns();
        Debug.Log("Collected Item");
    }

    void ScanResource()
    {
        if(GridOrganizer.instance.slotState != ResourceSlotState.ScanMode)
        {
            Debug.Log("This is not scan mode");
            return;
        }

        if(isScanned == true) return;

        if(GameplayUIManager.instance.NumberOfScanTurns == 0)
        {
            Debug.Log("You are out of scans");
            return;
        }

        Vector2Int thisSlotIndex = new Vector2Int();
        // Vector2Int topLeftIndex = new Vector2Int(-1, -1);
        // Vector2Int topRightIndex = new Vector2Int(1, 1);
        // Vector2Int topIndex = new Vector2Int(0, -1);
        // Vector2Int BottomIndex = new Vector2Int(0, 1);
        // Vector2Int LeftIndex = new Vector2Int(-1, 0);
        // Vector2Int RightIndex = new Vector2Int(1, 0);
        // Vector2Int debugtest = new Vector2Int(5, 99);
        // Debug.Log(debugtest.y);
        // Debug.Log(debugtest.x);
        bool isTerminated = false;
        for(int i = 0; i < GridOrganizer.instance.resourceSlots.Count; i++)
        {
            //thisSlotIndex.y = i;
            // used to completely break out of nested for loop
            if(!isTerminated)
            {
                for(int j = 0; j < GridOrganizer.instance.resourceSlots.Count; j++)
                {
                    thisSlotIndex.y = j;
                    if(GridOrganizer.instance.resourceSlots[i][j] == this)
                    {
                        //GridOrganizer.instance.resourceSlots[i][j].m_resourceSlotColor.color = Color.black;
                        isTerminated = true;
                        break;
                    }
                }
            }
            else break;
            thisSlotIndex.x = i;
        }
        Debug.Log(thisSlotIndex.x);
        Debug.Log(thisSlotIndex.y);

        Vector2Int topIndex = new Vector2Int(thisSlotIndex.x, thisSlotIndex.y - 1);
        var topSlot = GridOrganizer.instance.resourceSlots[topIndex.x][topIndex.y];
        topSlot.m_resourceSlotColor.color = topSlot.m_ScoreColor;
        topSlot.isScanned = true;

        Vector2Int bottomIndex = new Vector2Int(thisSlotIndex.x, thisSlotIndex.y + 1);
        var bottomSlot = GridOrganizer.instance.resourceSlots[bottomIndex.x][bottomIndex.y];
        bottomSlot.m_resourceSlotColor.color = bottomSlot.m_ScoreColor;
        bottomSlot.isScanned = true;

        Vector2Int topLeftIndex = new Vector2Int(thisSlotIndex.x - 1, thisSlotIndex.y - 1);
        var topLeftSlot = GridOrganizer.instance.resourceSlots[topLeftIndex.x][topLeftIndex.y];
        topLeftSlot.m_resourceSlotColor.color = topLeftSlot.m_ScoreColor;
        topLeftSlot.isScanned = true;
        
        Vector2Int leftIndex = new Vector2Int(thisSlotIndex.x - 1, thisSlotIndex.y);
        var leftSlot = GridOrganizer.instance.resourceSlots[leftIndex.x][leftIndex.y];
        leftSlot.m_resourceSlotColor.color = leftSlot.m_ScoreColor;
        leftSlot.isScanned = true;

        
        // int Xlength = (int)Mathf.Sqrt(GridOrganizer.instance.resourceSlots.Count);
        // if(thisSlotIndex.x < (Xlength - 1))
        // {

        // }
        
        m_resourceSlotColor.color = m_ScoreColor;
        isScanned = true;
        GameplayUIManager.instance.DecreaseScanTurns();
    }

    void ScanSlotPosition(PeripheralSlot position)
    {

    }
}