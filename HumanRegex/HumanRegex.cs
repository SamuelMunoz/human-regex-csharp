using System.Text.RegularExpressions;
using InvalidOperationException = System.InvalidOperationException;

namespace HumanRegex;

public partial class HumanRegex
{
    private readonly List<string> _parts = [];
    private RegexOptions _flag;
    
    public static HumanRegex Create() => new HumanRegex();

    public HumanRegex Digit() => Add("\\d");

    public HumanRegex Special() => Add("(?=.*[!@#$%^&*])");

    public HumanRegex Word() => Add("\\w");
    
    public HumanRegex WhiteSpace() => Add("\\s");
    
    public HumanRegex NonWhiteSpace() => Add("\\S");

    public HumanRegex Literal(string text) => Add(EscapeLiteral(text));
    
    public HumanRegex Or() => Add("|");

    public HumanRegex Range(RangeKeys name)
    {
        if (!RangesDefinition.Ranges.TryGetValue(name, out var range))
        {
            throw new ArgumentException("Invalid range key", nameof(name));
        }

        return Add($"[{range}]");
    }
    
    public HumanRegex NotRange(string text) => Add($"[^{text}]");

    public HumanRegex Lazy()
    {
        if (_parts.Count < 1)
        {
            throw new InvalidOperationException("No quantifier to make lazy");
        }

        var lastPat = _parts[^1];
        _parts.RemoveAt(_parts.Count - 1);
        return Add($"{lastPat}?");
    }

    public HumanRegex Letter() => Add("[a-zA-Z]");
    
    public HumanRegex AnyCharacter() => Add(".");
    
    public HumanRegex NegativeLookahead(string pattern) => Add($"(?!{pattern})");

    public HumanRegex PositiveLookahead(string pattern) => Add($"(?={pattern})");

    public HumanRegex PositiveLookbehind(string pattern) => Add($"(?<={pattern})");

    public HumanRegex NegativeLookbehind(string pattern) => Add($"(?<!{pattern})");

    public HumanRegex HasSpecialCharacter() => Add("(?=.*[!@#$%^&*])");

    public HumanRegex HasDigit() => Add("(?=.*\\d)");

    public HumanRegex HasLetter() => Add("(?=.*[a-zA-Z])");

    public HumanRegex Optional() => Add(Quantifiers.Optional);

    public HumanRegex Exactly(int n) => Add($"{{{n}}}");

    public HumanRegex AtLeast(int n) => Add($"{{{n},}}");

    public HumanRegex AtMost(int n) => Add($"{{0,{n}}}");

    public HumanRegex Between(int min, int max) => Add($"{{{min},{max}}}");

    public HumanRegex OneOrMore() => Add(Quantifiers.OneOrMore);

    public HumanRegex ZeroOrMore() => Add(Quantifiers.ZeroOrMore);

    public HumanRegex StartNamedGroup(string name) => Add($"(?<{name}>");

    public HumanRegex StartGroup() => Add("(?:");

    public HumanRegex StartCaptureGroup() => Add("(");

    public HumanRegex WordBoundary() => Add("\\b");

    public HumanRegex NonWordBoundary() => Add("\\B");

    public HumanRegex EndGroup() => Add(")");

    public HumanRegex StartAnchor() => Add("^");

    public HumanRegex EndAnchor() => Add("$");

    public HumanRegex Global()
    {
        _flag |= RegexOptions.None;
        return this;
    }

    public HumanRegex NonSensitive()
    {
        _flag |= RegexOptions.IgnoreCase;
        return this;
    }

    public HumanRegex Multiline()
    {
        _flag |= RegexOptions.Multiline;
        return this;
    }
    
    public HumanRegex Repeat(int count)
    {
        if (_parts.Count == 0)
            throw new InvalidOperationException("No pattern to repeat");

        var lastPart = _parts[^1];
        _parts.RemoveAt(_parts.Count - 1);
        _parts.Add($"({lastPart}){{{count}}}");
        return this;
    }

    public HumanRegex Ipv4Octet() => Add("(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)");

    public HumanRegex Protocol() => Add("https?://");

    public HumanRegex Www() => Add("(www\\.)?");

    public HumanRegex Tld() => Add("(com|org|net)");

    public HumanRegex Path() => Add("(/\\w+)*");
    
    private static string EscapeLiteral(string text)
    {
        if (!EscapeCache.TryGetValue(text, out var escaped))
        {
            escaped = Regex.Escape(text);
            EscapeCache.Add(text, escaped);
        }
        return escaped;
    }

    private static readonly Dictionary<string, string> EscapeCache = new();

    private HumanRegex Add(string part)
    {
        _parts.Add(part);
        return this;
    }

    public override string ToString() => string.Join("", _parts);

    public Regex ToRegExp() => new Regex(ToString(), _flag);
}