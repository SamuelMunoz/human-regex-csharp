namespace HumanRegex;

public static class RangesDefinition
{
    public static readonly Dictionary<HumanRegex.RangeKeys, string> Ranges = new()
    {
        { HumanRegex.RangeKeys.Digit, "0-9"},
        { HumanRegex.RangeKeys.LowercaseLetter, "a-z"},
        { HumanRegex.RangeKeys.UppercaseLetter, "A-Z"},
        { HumanRegex.RangeKeys.Letter, "a-zA-Z"},
        { HumanRegex.RangeKeys.Alphanumeric, "a-zA-Z0-9"},
        { HumanRegex.RangeKeys.AnyCharacter, "." }
    };
    
}