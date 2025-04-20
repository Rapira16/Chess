using Core.Pieces;

namespace Core.Logic;

public class Rook : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];
        
        if (startRow != endRow && startCol != endCol)
            return false;

        if (startRow == endRow)
        {
            int minCol = Math.Min(startCol, endCol);
            int maxCol = Math.Max(startCol, endCol);

            for (int col = minCol + 1; col <= maxCol; col++)
            {
                if (board[startRow, col] != null)
                    return false;
            }
        }
        else if (startCol == endCol)
        {
            int minRow = Math.Min(startRow, endRow);
            int maxRow = Math.Max(startRow, endRow);

            for (int row = minRow + 1; row < maxRow; row++)
            {
                if (board[row, startCol] != null)
                    return false;
            }
        }
        
        Piece target = board[endRow, endCol];
        if (target == null || target.Color != piece.Color)
            return true;
        
        return false;
    }
}