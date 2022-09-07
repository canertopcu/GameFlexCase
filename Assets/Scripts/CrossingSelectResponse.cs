using UnityEngine;

public class CrossingSelectResponse : MonoBehaviour,ISelectResponse
{
    public void OnSelect(Transform selection)
    {
        selection.GetComponent<GridBox>().Fill();
    } 
}
