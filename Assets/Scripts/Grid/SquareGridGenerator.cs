using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGridGenerator : MonoBehaviour, IGridGenerator
{
    private GridBox[,] boxes;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] public int rowCount = 5;
    [SerializeField] public int columnCount = 5;
    private float totalSize = 1.6f;
    private void Awake()
    {
        EventManager.OnGridGenerate += EventManager_OnGridGenerate;
        EventManager.OnMatchedGridsCleared += EventManager_OnMatchedGridsCleared;
    }

    private void OnDisable()
    {
        EventManager.OnMatchedGridsCleared -= EventManager_OnMatchedGridsCleared;
        EventManager.OnGridGenerate -= EventManager_OnGridGenerate;
    }

    private void EventManager_OnMatchedGridsCleared(List<GridBox> gridBoxList)
    {
        ResetMatchedGridBoxes(gridBoxList);
    }

    private void EventManager_OnGridGenerate(int rowColumnCount)
    {
        SetRowColumnCount(rowColumnCount, rowColumnCount);
        GenerateGrid();
    }

    public void ClearAllBoxes()
    {
        foreach (var item in GetComponentsInChildren<GridBox>())
        {
            DestroyImmediate(item.gameObject); //TODO:pool this after
        }
        boxes = null;
    }

    public void GenerateGrid()
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
                GameObject box = UnityEditor.PrefabUtility.InstantiatePrefab(boxPrefab, transform) as GameObject;
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

    public int GetRowCount()
    {
        return rowCount;
    }

    public int GetColumnCount()
    {
        return columnCount;
    }

    public GridBox[,] GetBoxes()
    {
        return boxes;
    }


    public void ResetAllGridBoxes()
    {
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < columnCount; x++)
            {
                boxes[x, y].SetEmpty();
            }
        }
    }

    public void ResetMatchedGridBoxes(List<GridBox> gridBoxes)
    {
        foreach (var item in gridBoxes)
        {
            item.SetEmpty();
        }
    }


    public void SetRowColumnCount(int row, int column)
    {
        rowCount = row;
        columnCount = column;

    }
}
