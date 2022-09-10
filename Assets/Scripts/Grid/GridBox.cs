using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour
{
    public int id = -1;
    public int x, y;
    private bool isEmpty = true;
    public GameObject cross;
    public List<GridBox> connections;
    public void Fill()
    {
        isEmpty = false;
        cross.SetActive(true);
        EventManager.CheckNeighbours(this);
    }

    public void SetEmpty()
    {
        isEmpty = true;
        cross.SetActive(false);

    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public void GetConnectedBoxes(ref List<GridBox> list)
    {
        if (connections != null)
        {
            foreach (var item in connections)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    item.GetConnectedBoxes(ref list);
                }
            }
        }
    }

}


