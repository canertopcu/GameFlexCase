using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionChecker : MonoBehaviour, ICollectionChecker
{
    public List<GridBox> selectedBoxes;
    public List<GridBox> connections;
    private void Start()
    {
        connections = new List<GridBox>();
    }
    public List<GridBox> GetSelectedBoxes()
    {
        if (selectedBoxes == null)
        {
            selectedBoxes = new List<GridBox>();
        }
        return selectedBoxes;
    }

    public void CheckAllSelectedBoxes(IGridGenerator gridGenerator)
    {
        GridBox[,] boxes = gridGenerator.GetBoxes();
        List<GridBox> localConnections = new List<GridBox>();
        bool anyMatched = false;
        for (int i = 0; i < selectedBoxes.Count; i++)
        {
            int connectionCount = 0;
            int x = selectedBoxes[i].x;
            int y = selectedBoxes[i].y;

            //check up
            if (y >= 1)
            {
                if (!boxes[x, y - 1].IsEmpty())
                {
                    localConnections.Add(boxes[x, y - 1]);
                    connectionCount++;
                }
            }

            //check down
            if (y <= gridGenerator.GetRowCount() - 2)
            {
                if (!boxes[x, y + 1].IsEmpty())
                {
                    localConnections.Add(boxes[x, y + 1]);
                    connectionCount++;
                }
            }

            //check left
            if (x >= 1)
            {
                if (!boxes[x - 1, y].IsEmpty())
                {
                    localConnections.Add(boxes[x - 1, y]);

                    connectionCount++;
                }
            }

            //check right
            if (x <= gridGenerator.GetColumnCount() - 2)
            {
                if (!boxes[x + 1, y].IsEmpty())
                {
                    localConnections.Add(boxes[x + 1, y]);
                    connectionCount++;
                }
            }

            //Set neighbours
            boxes[x, y].connections = localConnections;

            //Check and generate matched connected list
            if (connectionCount > 1)
            {
                connections.Add(boxes[x, y]);
                boxes[x, y].GetConnectedBoxes(ref connections);
                anyMatched = true;
            }
            localConnections.Clear();
        }

        if (anyMatched)
        {
            ClearSelectedBoxes(connections);
            connections.Clear();
            GameManager.matchCounter++;
        }
    }

    public void AddSelectedBox(GridBox selectedGridBox)
    {
        if (!selectedBoxes.Contains(selectedGridBox))
        {
            selectedBoxes.Add(selectedGridBox);
        }
    }

    public void ClearSelectedBoxes(List<GridBox> boxList)
    {
        EventManager.MatchedGridsCleared(boxList);

        foreach (var item in boxList)
        {
            selectedBoxes.Remove(item);
        }

        //selectedBoxes.Clear();
    }

}
