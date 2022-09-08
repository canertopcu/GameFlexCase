using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GridManager : MonoBehaviour
{
   
    public IGridGenerator gridGenerator;
    public ICollectionChecker collectionChecker;

    // Start is called before the first frame update
    void Awake()
    {
        gridGenerator = GetComponent<IGridGenerator>();
        collectionChecker = GetComponent<ICollectionChecker>();
         
        EventManager.OnMatchCounterChanged += EventManager_OnMatchCounterChanged;
        EventManager.OnCheckNeighbours += EventManager_OnCheckNeighbours;
        EventManager.OnGridGenerate += EventManager_OnGridGenerate;
    }

    private void Start()
    {
        gridGenerator.GenerateGrid();
    }

    private void EventManager_OnGridGenerate(int rowColumnCount)
    {
        collectionChecker.GetSelectedBoxes().Clear();
    }

    private void EventManager_OnCheckNeighbours(GridBox selectedGridBox)
    {
        collectionChecker.AddSelectedBox(selectedGridBox); 
       
        collectionChecker.CheckAllSelectedBoxes(gridGenerator); 
    }

    

    private void OnDisable()
    {
        EventManager.OnMatchCounterChanged -= EventManager_OnMatchCounterChanged;
        EventManager.OnCheckNeighbours -= EventManager_OnCheckNeighbours;
        EventManager.OnGridGenerate -= EventManager_OnGridGenerate;
    }
     
    private void EventManager_OnMatchCounterChanged(int count)
    { 
        collectionChecker.ClearSelectedBoxes();
        gridGenerator.ResetAllGridBoxes();
    }





}
