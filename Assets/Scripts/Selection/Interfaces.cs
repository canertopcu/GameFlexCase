
using UnityEngine;

public interface ISelector
{
    Transform GetSelection();
    void Check(Ray ray);
}
public interface ISelectResponse
{
    void OnSelect(Transform selection);
}
public interface IRaycastProvider
{
    Ray CreateRay();
}
