using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceSlotState
{
    ScanMode,
    ExtractMode
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
    
    ResourceSlotState slotState;
    Image m_resourceSlotColor;
    [SerializeField]
    Color hiddenSlotColor = new Color(0.16f, 0, 0, 1f);
    [SerializeField]
    Color QuarterSlotColor = new Color();
    [SerializeField]
    Color HalfSlotColor = new Color();
    [SerializeField]
    Color MaxSlotColor = new Color();
    [SerializeField]
    Color m_ResourceColor = new Color();

    void Start()
    {
        m_resourceSlotColor = GetComponent<Image>();
        
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
        ToggleResourceColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetResourceColor()
    {
        switch(m_ResourceScore)
        {
            case ResourceType.EmptyResource:
            m_ResourceColor = hiddenSlotColor;
            break;
            case ResourceType.QuarterResource:
            m_ResourceColor = QuarterSlotColor;
            break;
            case ResourceType.HalfResource:
            m_ResourceColor = HalfSlotColor;
            break;
            case ResourceType.MaxResource:
            m_ResourceColor = MaxSlotColor;
            break;
        }
    }

    void ToggleResourceColor()
    {
        if(m_resourceSlotColor.color == hiddenSlotColor)
        {
            m_resourceSlotColor.color = m_ResourceColor;
        }
        else
        {
            m_resourceSlotColor.color = hiddenSlotColor;
        }
    }

    void ToggleScanMode()
    {

    }

    void ToggleExtractMode()
    {

    }
}
