namespace ChessOnPhoneKeypad.Services.Services.BoardLayout
{
    public interface IBoardLayout
    {
        (int, string)[,] Configuration { get; set; }
    }
}
