using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GridManager : MonoBehaviour
{
    [SerializeField] private int rowCount = 5;
    [SerializeField] private int columnCount = 5;
    private GridBox[,] boxes;
    [SerializeField] private GameObject boxPrefab;
    private float totalSize = 1.6f;

    public List<GridBox> selectedBoxes;
    // Start is called before the first frame update
    void Awake()
    {
        selectedBoxes = new List<GridBox>();
        EventManager.OnGridGenerate += EventManager_OnGridGenerate;
        EventManager.OnMatchCounterChanged += EventManager_OnMatchCounterChanged;
        EventManager.OnCheckNeighbours += EventManager_OnCheckNeighbours;
    }

    private void EventManager_OnCheckNeighbours(GridBox selectedGridBox)
    {
        if (!selectedBoxes.Contains(selectedGridBox))
        {
            selectedBoxes.Add(selectedGridBox);
        }

        CheckAllSelectedBoxes();
    }

    private void CheckAllSelectedBoxes()
    {
        for (int i = 0; i < selectedBoxes.Count; i++)
        {
            int neighbourCounter = 0;
            int x = selectedBoxes[i].x;
            int y = selectedBoxes[i].y;

            //check up
            if (y >= 1)
            {
                if (!boxes[x, y - 1].IsEmpty())
                {
                    neighbourCounter++;
                }
            }

            //check down
            if (y <= rowCount - 2)
            {
                if (!boxes[x, y + 1].IsEmpty())
                {
                    neighbourCounter++;
                }
            }

            //check left
            if (x >= 1)
            {
                if (!boxes[x - 1, y].IsEmpty())
                {
                    neighbourCounter++;
                }
            }
            //check right
            if (x <= columnCount - 2)
            {
                if (!boxes[x + 1, y].IsEmpty())
                {
                    neighbourCounter++;
                }
            }

            if (neighbourCounter > 1)
            {
                GameManager.matchCounter++;
                break;
            }
        } 
        
    }

    private void OnDisable()
    {
        EventManager.OnGridGenerate -= EventManager_OnGridGenerate;
        EventManager.OnMatchCounterChanged -= EventManager_OnMatchCounterChanged;
        EventManager.OnCheckNeighbours -= EventManager_OnCheckNeighbours;
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void EventManager_OnGridGenerate(int rowColumnCount)
    {
        DrawGrid(rowColumnCount);
    }

    private void EventManager_OnMatchCounterChanged(int count)
    {
        ResetAllGridBoxes();
    }

    public void DrawGrid(int count)
    {
        rowCount = columnCount = count;
        GenerateGrid();
    }


    [ContextMenu("Draw New Grid")]
    private void GenerateGrid()
    {
        ClearAllBoxes();
        boxes = new GridBox[columnCount, rowCount];
        float boxSize = (totalSize / columnCount);
        float distance = boxSize * 10f;
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < columnCount; x++)
            {
#if UNITY_EDITOR
                GameObject box = PrefabUtility.InstantiatePrefab(boxPrefab, transform) as GameObject;
#else

                GameObject box = Instantiate(boxPrefab, transform);
#endif
                GridBox b = box.GetComponent<GridBox>();
                b.x = x;
                b.y = y;
                boxes[x, y] = b;
                box.transform.localPosition = new Vector3(x * distance - ((distance * (columnCount - 1)) / 2f), 0, y * distance - ((distance * (rowCount - 1)) / 2f));
                b.id = x + y * rowCount;

                box.transform.localScale = Vector3.one * boxSize;
            }
        }
    }

    [ContextMenu("Clear The Grid")]
    private void ClearAllBoxes()
    {
        selectedBoxes.Clear();
        foreach (var item in GetComponentsInChildren<GridBox>())
        {
            DestroyImmediate(item.gameObject); //TODO:pool this after
        }
        boxes = null;

    }

    public void ResetAllGridBoxes()
    {
        selectedBoxes.Clear();
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < columnCount; x++)
            {
                boxes[x, y].SetEmpty();
            }
        }
    }
}
