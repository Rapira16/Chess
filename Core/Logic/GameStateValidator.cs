namespace Core.Logic;
using Core.Board;
using Core.Pieces;

public class GameStateValidator
{
    private readonly Board _board;

    public GameStateValidator(Board board)
    {
        _board = board;
    }

    public bool IsCheckmate(PieceColor playerColor, Piece?[,] board)
    {
        var checkValidator = new CheckValidator(_board);
        if (!checkValidator.IsKingInCheck(playerColor, board))
            return false;

        return !HasLegalMoves(playerColor, board);
    }

    public bool IsStalemate(PieceColor playerColor, Piece?[,] board)
    {
        return !HasLegalMoves(playerColor, board);
    }
    
    private bool HasLegalMoves(PieceColor playerColor, Piece?[,] board)
    {
        for (int startRow = 0; startRow < 8; startRow++)
        {
            for (int startCol = 0; startCol < 8; startCol++)
            {
                var piece = board[startRow, startCol];
                if (piece == null || piece.Color != playerColor)
                    continue;

                var validator = _board.GetMoveValidatorForPiece(piece);

                for (int endRow = 0; endRow < 8; endRow++)
                {
                    for (int endCol = 0; endCol < 8; endCol++)
                    {
                        if (!validator.IsValidMove(startRow, startCol, endRow, endCol, board))
                            continue;

                        var clone = CloneBoard(board);
                        clone[endRow, endCol] = piece;
                        clone[startRow, startCol] = null;
                        
                        var checkValidator = new CheckValidator(_board);
                        if (!checkValidator.IsKingInCheck(playerColor, clone))
                            return true;
                    }
                }
            }
        }

        return false;
    }

    private Piece?[,] CloneBoard(Piece?[,] board)
    {
        var clone = new Piece?[8, 8];
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                clone[row, col] = board[row, col];
            }
        }
        return clone;
    }
}