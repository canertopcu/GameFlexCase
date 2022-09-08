public interface IGridGenerator:IGridClear 
{
    void GenerateGrid();
    int GetRowCount();
    int GetColumnCount();

    void SetRowColumnCount(int row, int column);
    GridBox[,] GetBoxes();



}