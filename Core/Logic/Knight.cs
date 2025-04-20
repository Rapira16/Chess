using Core.Pieces;

namespace Core.Logic;

public class Knight : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];
        
        int rowDiff = Math.Abs(endRow - startRow);
        int colDiff = Math.Abs(endCol - startCol);

        if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
        {
            Piece target = board[endRow, endCol];
            if (target == null || target.Color != piece.Color)
                return true;
        }
        
        return false;
    }
}