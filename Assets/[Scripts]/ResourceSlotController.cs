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
    Vector2Int thisSlotIndex;

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
        thisSlotIndex = new Vector2Int();
        m_resourceSlotColor = GetComponent<Image>();
        m_resourceSlotButton = GetComponent<Button>();
        
        m_resourceSlotButton.onClick.AddListener(ExtractResource);
        m_resourceSlotButton.onClick.AddListener(ScanResource);
        
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
        //FindThisSlotPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug_ToggleResourceColor();
        }
    }

    void FindThisSlotPosition()
    {
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
        //Debug.Log(thisSlotIndex.x);
        //Debug.Log(thisSlotIndex.y);
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

        if(isExtracted == true)
        {
            GameplayUIManager.instance.DisplayMessage("This grid's resources have been extracted.");
            return;
        }

        if(GameplayUIManager.instance.NumberOfExtractTurns == 0)
        {
            Debug.Log("Game Over");
            GameplayUIManager.instance.GameOverMessage();
            return;
        }

        // Displays color
        m_resourceSlotColor.color = m_ScoreColor;
        // Dims the color to 1/4th after displaying to represent it is extracted
        //m_resourceSlotColor.color /= 4f;
        float dim = 0.45f;
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

        if(isScanned == true)
        {
            GameplayUIManager.instance.DisplayMessage("This grid is already scanned");
            return;
        }

        if(GameplayUIManager.instance.NumberOfScanTurns == 0)
        {
            Debug.Log("You are out of scans");
            return;
        }


        FindThisSlotPosition();
        {
        
        Vector2Int topIndex = thisSlotIndex + new Vector2Int(0, -1);
        var topSlot = GridOrganizer.instance.resourceSlots[topIndex.x][topIndex.y];
        topSlot.m_resourceSlotColor.color = topSlot.m_ScoreColor;
        topSlot.isScanned = true;

        Vector2Int bottomIndex = thisSlotIndex + new Vector2Int(0, 1);
        var bottomSlot = GridOrganizer.instance.resourceSlots[bottomIndex.x][bottomIndex.y];
        bottomSlot.m_resourceSlotColor.color = bottomSlot.m_ScoreColor;
        bottomSlot.isScanned = true;

        Vector2Int leftIndex = thisSlotIndex + new Vector2Int(-1, 0);
        var leftSlot = GridOrganizer.instance.resourceSlots[leftIndex.x][leftIndex.y];
        leftSlot.m_resourceSlotColor.color = leftSlot.m_ScoreColor;
        leftSlot.isScanned = true;

        Vector2Int rightIndex = thisSlotIndex + new Vector2Int(1, 0);
        var rightSlot = GridOrganizer.instance.resourceSlots[rightIndex.x][rightIndex.y];
        rightSlot.m_resourceSlotColor.color = rightSlot.m_ScoreColor;
        rightSlot.isScanned = true;

        Vector2Int topLeftIndex = thisSlotIndex + new Vector2Int(-1, -1);
        var topLeftSlot = GridOrganizer.instance.resourceSlots[topLeftIndex.x][topLeftIndex.y];
        topLeftSlot.m_resourceSlotColor.color = topLeftSlot.m_ScoreColor;
        topLeftSlot.isScanned = true;

        Vector2Int bottomLeftIndex = thisSlotIndex + new Vector2Int(-1, 1);
        var bottomLeftSlot = GridOrganizer.instance.resourceSlots[bottomLeftIndex.x][bottomLeftIndex.y];
        bottomLeftSlot.m_resourceSlotColor.color = bottomLeftSlot.m_ScoreColor;
        bottomLeftSlot.isScanned = true;

        Vector2Int topRightIndex = thisSlotIndex + new Vector2Int(1, -1);
        var topRightSlot = GridOrganizer.instance.resourceSlots[topRightIndex.x][topRightIndex.y];
        topRightSlot.m_resourceSlotColor.color = topRightSlot.m_ScoreColor;
        topRightSlot.isScanned = true;
        
        Vector2Int bottomRightIndex = thisSlotIndex + new Vector2Int(1, 1);
        var bottomRightSlot = GridOrganizer.instance.resourceSlots[bottomRightIndex.x][bottomRightIndex.y];
        bottomRightSlot.m_resourceSlotColor.color = bottomRightSlot.m_ScoreColor;
        bottomRightSlot.isScanned = true;
        
        }

        // var topSlot = ScanGetPeripheralSlot(PeripheralSlot.TOP, thisSlotIndex);
        // var bottomSlot = ScanGetPeripheralSlot(PeripheralSlot.BOTTOM, thisSlotIndex);
        // var leftSlot = ScanGetPeripheralSlot(PeripheralSlot.LEFT, thisSlotIndex);
        // var rightSlot = ScanGetPeripheralSlot(PeripheralSlot.RIGHT, thisSlotIndex);
        // var topLeftSlot = ScanGetPeripheralSlot(PeripheralSlot.TOPLEFT, thisSlotIndex);
        // var bottomLeftSlot = ScanGetPeripheralSlot(PeripheralSlot.BOTTOMLEFT, thisSlotIndex);
        // var topRightSlot = ScanGetPeripheralSlot(PeripheralSlot.TOPRIGHT, thisSlotIndex);
        // var bottomRightSlot = ScanGetPeripheralSlot(PeripheralSlot.BOTTOMRIGHT, thisSlotIndex);
        
        m_resourceSlotColor.color = m_ScoreColor;
        isScanned = true;
        GameplayUIManager.instance.DecreaseScanTurns();
    }

    ResourceSlotController ScanGetPeripheralSlot(PeripheralSlot position, Vector2Int slotPosIndex)
    {
        ResourceSlotController peripheralSlot = null;
        Vector2Int index = new Vector2Int();
        switch(position)
        {
            case PeripheralSlot.TOP:
                index = thisSlotIndex + new Vector2Int(0, -1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.BOTTOM:
                index = slotPosIndex + new Vector2Int(0, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.LEFT:
                index = slotPosIndex + new Vector2Int(-1, 0);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.RIGHT:
                index = slotPosIndex + new Vector2Int(1, 0);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.TOPLEFT:
                index = slotPosIndex + new Vector2Int(-1, -1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.TOPRIGHT:
                index = slotPosIndex + new Vector2Int(1, -1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.BOTTOMLEFT:
                index = slotPosIndex + new Vector2Int(-1, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;

            case PeripheralSlot.BOTTOMRIGHT:
                index = slotPosIndex + new Vector2Int(1, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[slotPosIndex.x][slotPosIndex.y];
            break;
        }

        peripheralSlot.m_resourceSlotColor.color = peripheralSlot.m_ScoreColor;
        peripheralSlot.isScanned = true;
        Debug.Log("Scanned Resource");
        return peripheralSlot;
    }
}