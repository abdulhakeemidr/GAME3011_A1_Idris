using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ResourceSlotController : MonoBehaviour
{
    [SerializeField, Header("Resource Slot Info")]
    private int m_ResourceScore;
    [SerializeField]
    Color m_ScoreColor = new Color();

    [Header("Resource Probability Percentage")]
    public int MaxResourceProbability = 5;
    public int HalfResourceProbability = 5;
    public int QuarterResourceProbability = 20;
    public int emptyResourceProbability = 70;
    SpawnProbability[] probability = new SpawnProbability[4];
    bool isExtracted = false;
    bool isScanned = false;
    Vector2Int thisSlotIndex;

    [HideInInspector]
    public Image m_displayedSlotColor;
    Button m_resourceSlotButton;


    [Header("Resource Colours")]
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
        m_displayedSlotColor = GetComponent<Image>();
        m_resourceSlotButton = GetComponent<Button>();
        
        m_resourceSlotButton.onClick.AddListener(ExtractResource);
        m_resourceSlotButton.onClick.AddListener(ScanResource);
        
        m_displayedSlotColor.color = hiddenSlotColor;
        
        
        // 10 percent chance
        probability[0] = new SpawnProbability(ResourceType.MaxResource);
        probability[0].minProbability = 0;
        probability[0].maxProbability = MaxResourceProbability;
        
        // 10 percent chance
        probability[1] = new SpawnProbability(ResourceType.HalfResource);
        probability[1].minProbability = probability[0].maxProbability;
        probability[1].maxProbability = HalfResourceProbability + probability[0].maxProbability;

        // 20 percent chance
        probability[2] = new SpawnProbability(ResourceType.QuarterResource);
        probability[2].minProbability = probability[1].maxProbability;
        probability[2].maxProbability = QuarterResourceProbability + probability[1].maxProbability;

        // 60 percent chance
        probability[3] = new SpawnProbability(ResourceType.EmptyResource);
        probability[3].minProbability = probability[2].maxProbability;
        probability[3].maxProbability = emptyResourceProbability + probability[2].maxProbability;

        int i = Random.Range(0, 100);

        for(int index = 0; index < probability.Length; index++)
        {
            if(i > probability[index].minProbability && i <= probability[index].maxProbability)
            {
                m_ResourceScore = probability[index].resourceValue;
                break;
            }
        }

        SetResourceColor();
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

        if(m_displayedSlotColor.color == hiddenSlotColor)
        {
            m_displayedSlotColor.color = m_ScoreColor;
        }
        else
        {
            m_displayedSlotColor.color = hiddenSlotColor;
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
        m_displayedSlotColor.color = m_ScoreColor;
        // Dims the color to 1/4th after displaying to represent it is extracted
        //m_resourceSlotColor.color /= 4f;
        float dim = 0.45f;
        m_displayedSlotColor.color *= new Color(dim, dim, dim, 1f);
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

        // check if top slot is out of bounds
        if(thisSlotIndex.y != 0)
        {
            ScanGetPeripheralSlot(PeripheralSlot.TOP, thisSlotIndex);

            if(thisSlotIndex.x != 0)
            {
            ScanGetPeripheralSlot(PeripheralSlot.TOPLEFT, thisSlotIndex);
            }

            if(thisSlotIndex.x != GridOrganizer.instance.resourceSlots.Count - 1)
            {
            ScanGetPeripheralSlot(PeripheralSlot.TOPRIGHT, thisSlotIndex);
            }
        }

        // check if bottom slot is out of bounds (if clicked on bottom edge)
        if(thisSlotIndex.y != GridOrganizer.instance.resourceSlots[0].Count - 1)
        {
            ScanGetPeripheralSlot(PeripheralSlot.BOTTOM, thisSlotIndex);
            
            // check if bottom left slot is out of array (if clicked on left edge)
            if(thisSlotIndex.x != 0)
            {
                ScanGetPeripheralSlot(PeripheralSlot.BOTTOMLEFT, thisSlotIndex);
            }

            // check if bottom left slot is out of array (if clicked on right edge)
            if(thisSlotIndex.x != GridOrganizer.instance.resourceSlots.Count - 1)
            {
                ScanGetPeripheralSlot(PeripheralSlot.BOTTOMRIGHT, thisSlotIndex);
            }
        }

        if(thisSlotIndex.x != 0)
        {
            ScanGetPeripheralSlot(PeripheralSlot.LEFT, thisSlotIndex);
        }

        if(thisSlotIndex.x != GridOrganizer.instance.resourceSlots.Count - 1)
        {
            ScanGetPeripheralSlot(PeripheralSlot.RIGHT, thisSlotIndex);
        }

        
        
        m_displayedSlotColor.color = m_ScoreColor;
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
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.BOTTOM:
                index = slotPosIndex + new Vector2Int(0, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.LEFT:
                index = slotPosIndex + new Vector2Int(-1, 0);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.RIGHT:
                index = slotPosIndex + new Vector2Int(1, 0);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.TOPLEFT:
                index = slotPosIndex + new Vector2Int(-1, -1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.TOPRIGHT:
                index = slotPosIndex + new Vector2Int(1, -1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.BOTTOMLEFT:
                index = slotPosIndex + new Vector2Int(-1, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;

            case PeripheralSlot.BOTTOMRIGHT:
                index = slotPosIndex + new Vector2Int(1, 1);
                peripheralSlot = GridOrganizer.instance.resourceSlots[index.x][index.y];
            break;
        }

        peripheralSlot.m_displayedSlotColor.color = peripheralSlot.m_ScoreColor;
        peripheralSlot.isScanned = true;
        Debug.Log("Scanned Resource");
        return peripheralSlot;
    }
}