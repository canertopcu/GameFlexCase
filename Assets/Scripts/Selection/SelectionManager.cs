
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Transform _selectedGridBox;

    private IRaycastProvider _rayProvider; 
    private ISelector _selector;
    private ISelectResponse _selectorResponse;

    private void Awake()
    {

        _rayProvider = GetComponent<IRaycastProvider>();
        _selector = GetComponent<ISelector>();
        _selectorResponse = GetComponent<ISelectResponse>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selector.Check(_rayProvider.CreateRay());
            _selectedGridBox = _selector.GetSelection();
            if (_selectedGridBox != null) _selectorResponse.OnSelect(_selectedGridBox);
        }
    }
}
