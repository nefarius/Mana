using System.Drawing;

namespace Mana.Util;

public static class ColorsExtensions
{
    public static string AsEscapeSequence(this Color256 color, bool foreground = true)
    {
        var modifier = foreground ? "38" : "48";

        return $"\u001b[{modifier};5;{(int)color}m";
    }

    public static string AsEscapeSequence(this Color color, bool foreground = true)
    {
        var modifier = foreground ? "38" : "48";

        return $"\u001b[{modifier};2;{color.R};{color.G};{color.B}m";
    }
}