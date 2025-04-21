using Core.Pieces;

namespace Core.Logic;

public class Queen : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];

        if (startRow == endRow || startCol == endCol || Math.Abs(endRow - startRow) == Math.Abs(endCol - startCol))
        {
            int rowDirection = endRow > startRow ? 1 : (endRow < startRow ? -1 : 0);
            int colDirection = endCol > startCol ? 1 : (endCol < startCol ? -1 : 0);
            
            int row = startRow + rowDirection;
            int col = startCol + colDirection;

            while (row != endRow && col != endCol)
            {
                if (board[row, col] != null)
                    return false;
                
                row += rowDirection;
                col += colDirection;
            }
            
            Piece target = board[endRow, endCol];
            if (target != null && target.Color == piece.Color)
                return false;
            
            return true;
        }
        return false;
    }
}