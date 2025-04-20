namespace Core.Pieces;

public class Piece
{
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
    public bool HasMoved { get; set; }

    public Piece(PieceType type, PieceColor color)
    {
        this.Type = type;
        this.Color = color;
        this.HasMoved = false;
    }
    
    public void MarkMoved() => HasMoved = true;
    
    public override string ToString() => $"{Type} {Color}";
}