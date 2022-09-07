
using UnityEngine;

public class RaycastBasedSelector : MonoBehaviour, ISelector
{
    private Transform _selection;

    public void Check(Ray ray)
    {
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("GridBox"))
            {
                _selection = selection.transform;
            }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}
