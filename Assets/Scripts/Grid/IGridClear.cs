using System.Collections.Generic;

public interface IGridClear
{
    void ClearAllBoxes();
    void ResetAllGridBoxes();
    void ResetMatchedGridBoxes(List<GridBox> list);
}