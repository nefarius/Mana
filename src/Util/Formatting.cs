namespace Mana.Util;
/*
* This is free and unencumbered software released into the public domain.
*
* For more information, please refer to <https://unlicense.org>
*/

public struct Formatting
{
    // Regular text
    public const string Black = "\u001b[0;30m";
    public const string Red = "\u001b[0;31m";
    public const string Green = "\u001b[0;32m";
    public const string Yellow = "\u001b[0;33m";
    public const string Blue = "\u001b[0;34m";
    public const string MAG = "\u001b[0;35m";
    public const string Cyan = "\u001b[0;36m";
    public const string White = "\u001b[0;37m";

    // Regular bold text
    public const string BBlack = "\u001b[1;30m";
    public const string BRed = "\u001b[1;31m";
    public const string BGreen = "\u001b[1;32m";
    public const string BYellow = "\u001b[1;33m";
    public const string BBlue = "\u001b[1;34m";
    public const string BMAG = "\u001b[1;35m";
    public const string BCyan = "\u001b[1;36m";
    public const string BWhite = "\u001b[1;37m";

    // Regular underline text
    public const string UBlack = "\u001b[4;30m";
    public const string URed = "\u001b[4;31m";
    public const string UGreen = "\u001b[4;32m";
    public const string UYellow = "\u001b[4;33m";
    public const string UBlue = "\u001b[4;34m";
    public const string UMAG = "\u001b[4;35m";
    public const string UCyan = "\u001b[4;36m";
    public const string UWhite = "\u001b[4;37m";

    // Regular background
    public const string BlackB = "\u001b[40m";
    public const string RedB = "\u001b[41m";
    public const string GreenB = "\u001b[42m";
    public const string YellowB = "\u001b[43m";
    public const string BlueB = "\u001b[44m";
    public const string MAGB = "\u001b[45m";
    public const string CyanB = "\u001b[46m";
    public const string WhiteB = "\u001b[47m";

    // High intensity background 
    public const string BlackHB = "\u001b[0;100m";
    public const string RedHB = "\u001b[0;101m";
    public const string GreenHB = "\u001b[0;102m";
    public const string YellowHB = "\u001b[0;103m";
    public const string BlueHB = "\u001b[0;104m";
    public const string MAGHB = "\u001b[0;105m";
    public const string CyanHB = "\u001b[0;106m";
    public const string WhiteHB = "\u001b[0;107m";

    // High intensity text
    public const string HBlack = "\u001b[0;90m";
    public const string HRed = "\u001b[0;91m";
    public const string HGreen = "\u001b[0;92m";
    public const string HYellow = "\u001b[0;93m";
    public const string HBlue = "\u001b[0;94m";
    public const string HMAG = "\u001b[0;95m";
    public const string HCyan = "\u001b[0;96m";
    public const string HWhite = "\u001b[0;97m";

    // Bold high intensity text
    public const string BHBlack = "\u001b[1;90m";
    public const string BHRed = "\u001b[1;91m";
    public const string BHGreen = "\u001b[1;92m";
    public const string BHYellow = "\u001b[1;93m";
    public const string BHBlue = "\u001b[1;94m";
    public const string BHMAG = "\u001b[1;95m";
    public const string BHCyan = "\u001b[1;96m";
    public const string BHWhite = "\u001b[1;97m";

    // Reset
    public const string Reset = "\u001b[0m";
}