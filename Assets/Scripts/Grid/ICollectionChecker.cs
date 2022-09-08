using System.Collections.Generic;

public interface ICollectionChecker:ISelectedBox
{
    void CheckAllSelectedBoxes(IGridGenerator gridGenerator);
   
}