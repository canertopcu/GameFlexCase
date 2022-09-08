using System.Collections.Generic;

public interface ISelectedBox
{
    List<GridBox> GetSelectedBoxes();
    void AddSelectedBox(GridBox selectedGridBox);

    void ClearSelectedBoxes();
}