using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<int> OnGridGenerate;
    public static event Action<int> OnMatchCounterChanged;
    public static event Action<GridBox> OnCheckNeighbours;
    public static event Action<List<GridBox>> OnMatchedGridsCleared;


    public static void MatchedGridsCleared(List<GridBox> boxList)
    {
        OnMatchedGridsCleared?.Invoke(boxList);
    }

    public static void MatchCounterChanged(int count)
    {
        OnMatchCounterChanged?.Invoke(count);
    }

    public static void GenerateGrid(int columnRowCount)
    {
        OnGridGenerate?.Invoke(columnRowCount);
    }

    public static void CheckNeighbours(GridBox gridBox)
    {
        OnCheckNeighbours?.Invoke(gridBox);
    }
}
