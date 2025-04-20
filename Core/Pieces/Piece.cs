namespace Core.Pieces;

public class Piece(PieceType type, PieceColor color)
{
    public bool HasMoved { get; set; } = false;
    
    public void MarkMoved() => HasMoved = true;
    
    public override string ToString() => $"{type} {color}";
}