using Core.Pieces;

namespace Core.Logic;

public interface IMoveValidator
{
    bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board);
}