using Core.Pieces;

namespace Core.Logic;

public class Bishop : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];
        
        if (Math.Abs(startRow - endRow) != Math.Abs(startCol - endCol))
            return false;
        
        int rowDirection = endRow > startRow ? 1 : -1;
        int colDirection = endCol > startCol ? 1 : -1;
        
        int row = startRow - rowDirection;
        int col = startCol - colDirection;
        while (row != endRow && col != endCol)
        {
            if (board[row, col] != null)
                return false;
            
            row += rowDirection;
            col += colDirection;
        }
        
        Piece target = board[endRow, endCol];
        if (target == null || target.Color != piece.Color)
            return true;
        
        return false;
    }
}