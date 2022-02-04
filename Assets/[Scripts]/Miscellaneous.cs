using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class SpawnProbability
{
    public int resourceValue;
    
    public int minProbability = 0;
    public int maxProbability = 100;

    public SpawnProbability(int resource)
    {
        resourceValue = resource;
    }

}