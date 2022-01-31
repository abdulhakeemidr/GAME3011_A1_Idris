using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        m_resourceSlotColor.color = m_ScoreColor;
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
        /* for(int i = 0; i < GridOrganizer.instance.ResourceSlots.Count; i++)
        {
            thisSlotIndex.y = i;
            for(int j = 0; j < GridOrganizer.instance.ResourceSlots.Count; j++)
            {
                thisSlotIndex.x = j;
                if(GridOrganizer.instance.ResourceSlots[i][j] == this)
                {
                    GridOrganizer.instance.ResourceSlots[i][j].m_resourceSlotColor.color = Color.black;
                    break;
                }
            }
        } */
        
        m_resourceSlotColor.color = m_ScoreColor;
        isScanned = true;
        GameplayUIManager.instance.DecreaseScanTurns();
    }
}