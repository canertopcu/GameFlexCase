using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionChecker : MonoBehaviour, ICollectionChecker
{
    public List<GridBox> selectedBoxes;

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
            if (y <= gridGenerator.GetRowCount() - 2)
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
            if (x <= gridGenerator.GetColumnCount() - 2)
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

    public void AddSelectedBox(GridBox selectedGridBox)
    {
        if (!selectedBoxes.Contains(selectedGridBox))
        {
            selectedBoxes.Add(selectedGridBox);
        }
    }

    public void ClearSelectedBoxes()
    {
        selectedBoxes.Clear();
    }

}
