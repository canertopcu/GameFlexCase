using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour
{
    public int id = -1;
    public int x, y;
    private bool isEmpty = true;
    public GameObject cross;
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

}


