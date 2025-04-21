namespace Core.Board;
using Pieces;
using Logic;

public class Board
{
    public Piece?[,] _board = new Piece[8, 8];
    private TurnManager _turnManager = new TurnManager();
    private MoveHistory _history = new MoveHistory();

    public Piece?[,] GetBoard() => _board;
    
    public void InitializeBoard()
    {
        _board[0, 0] = new Piece(PieceType.Rook, PieceColor.White);
        _board[0, 1] = new Piece(PieceType.Knight, PieceColor.White);
        _board[0, 2] = new Piece(PieceType.Bishop, PieceColor.White);
        _board[0, 3] = new Piece(PieceType.Queen, PieceColor.White);
        _board[0, 4] = new Piece(PieceType.King, PieceColor.White);
        _board[0, 5] = new Piece(PieceType.Bishop, PieceColor.White);
        _board[0, 6] = new Piece(PieceType.Knight, PieceColor.White);
        _board[0, 7] = new Piece(PieceType.Rook, PieceColor.White);

        for (int i = 0; i < 8; i++)
        {
            _board[1, i] = new Piece(PieceType.Pawn, PieceColor.White);
        }
        
        _board[7, 0] = new Piece(PieceType.Rook, PieceColor.Black);
        _board[7, 1] = new Piece(PieceType.Knight, PieceColor.Black);
        _board[7, 2] = new Piece(PieceType.Bishop, PieceColor.Black);
        _board[7, 3] = new Piece(PieceType.Queen, PieceColor.Black);
        _board[7, 4] = new Piece(PieceType.King, PieceColor.Black);
        _board[7, 5] = new Piece(PieceType.Bishop, PieceColor.Black);
        _board[7, 6] = new Piece(PieceType.Knight, PieceColor.Black);
        _board[7, 7] = new Piece(PieceType.Rook, PieceColor.Black);

        for (int i = 0; i < 8; i++)
        {
            _board[6, i] = new Piece(PieceType.Pawn, PieceColor.Black);
        }
    }

    public void DisplayBoard()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (_board[row, col] != null)
                    Console.Write(_board[row, col] + " ");
                else
                    Console.Write("Empty ");
            }
            Console.WriteLine();
        }
    }

    public bool MovePiece(int startRow, int startCol, int endRow, int endCol)
    {
        if (startRow < 0 || startRow >= 8 || startCol < 0 || startCol >= 8 ||
            endRow < 0 || endRow >= 8 || endCol < 0 || endCol >= 8)
        {
            Console.WriteLine("Invalid move");
            return false;
        }
        
        Piece? piece = _board[startRow, startCol];

        if (piece == null)
        {
            Console.WriteLine("No piece found");
            return false;
        }

        if (!_turnManager.IsValidTurn(piece))
        {
            Console.WriteLine("It's {_turnManager.CurrentTurn} turn");
            return false;
        }
        
        if (!IsValidMove(startRow, startCol, endRow, endCol, piece))
        {
            Console.WriteLine("Invalid move");
            return false;
        }
        
        _history.RecordMove(startRow, startCol, endRow, endCol, piece);
        
        _board[endRow, endCol] = piece;
        _board[startRow, startCol] = null;
        piece.MarkMoved();
        _turnManager.SwitchTurn();
        return true;
    }

    public void Undo()
    {
        _history.UndoLastMove(_board);
        _turnManager.SwitchTurn();
    }

    private bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece piece)
    {
        IMoveValidator moveValidator = GetMoveValidatorForPiece(piece);
        return moveValidator.IsValidMove(startRow, startCol, endRow, endCol, _board);
    }

    public IMoveValidator GetMoveValidatorForPiece(Piece piece)
    {
        switch (piece.Type)
        {
            case PieceType.Pawn:
                return new Pawn();
            case PieceType.Rook:
                return new Rook();
            case PieceType.Knight:
                return new Knight();
            case PieceType.Bishop:
                return new Bishop();
            case PieceType.Queen:
                return new Queen();
            case PieceType.King:
                return new King();
            default:
                throw new ArgumentException("Invalid piece type");
        }
    }
}