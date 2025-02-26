# Human Regex Generator

Human-friendly regular expression builder with English-like syntax.

## Features

- Intuitive builder pattern with Fluent methods.
- Exports a Regex instance or just the string to be used with other Regex libraries.

## Usage

### Basic example

```csharp
 var builder = HumanRegex.Create()
            .StartAnchor()
            .Word()
            .OneOrMore()
            .Literal("@")
            .Word()
            .OneOrMore()
            .StartGroup()
            .Literal(".")
            .Word()
            .OneOrMore()
            .EndGroup()
            .ZeroOrMore()
            .Literal(".")
            .Letter()
            .AtLeast(2)
            .EndAnchor().ToRegExp();
 
 Console.WriteLine(builder.Match("test@example.com").Success) // true
```
## API Reference

Initialize the chain with `HumanRegex.Create()` then start chaining the rest of the Regex methods.

### Core Methods

| Method               | Description                                     | Example Output |
|----------------------| ----------------------------------------------- | -------------- |
| `.Digit()`           | Adds a digit pattern (`\d`).                    | `\d`           |
| `.Word()`            | Adds a word character pattern (`\w`).           | `\w`           |
| `.Whitespace()`      | Adds a whitespace character pattern (`\s`).     | `\s`           |
| `.NonWhitespace()`   | Adds a non-whitespace character pattern (`\S`). | `\S`           |
| `.AnyCharacter()`    | Adds a pattern for any character (`.`).         | `.`            |
| `.Literal("text")`   | Adds a literal text pattern.                    | `["text"]`     |
| `.Or()`              | Adds an OR pattern.                             | `\|`           |
| `.Range("digit")`    | Adds a range pattern for digits (`0-9`).        | `[0-9]`        |
| `.NotRange("aeiou")` | Excludes characters from the pattern.           | `[^aeiou]`     |

### Quantifiers

| Method               | Description                                       | Example Output |
|----------------------| ------------------------------------------------- | -------------- |
| `.Exactly(n)`        | Adds an exact quantifier (`{n}`).                 | `{n}`          |
| `.AtLeast(n)`        | Adds a minimum quantifier (`{n,}`).               | `{n,}`         |
| `.AtMost(n)`         | Adds a maximum quantifier (`{0,n}`).              | `{0,n}`        |
| `.Between(min, max)` | Adds a range quantifier (`{min,max}`).            | `{min,max}`    |
| `.OneOrMore()`       | Adds a one-or-more quantifier (`+`).              | `+`            |
| `.Optional()`        | Adds an optional quantifier (`?`).                | `?`            |
| `.ZeroOrMore()`      | Adds a zero-or-more quantifier (`*`).             | `*`            |
| `.Lazy()`            | Makes the previous quantifier lazy.               | `?`            |
| `.Repeat(count)`     | Repeats the previous pattern exactly count times. | `{count}`      |

### Anchors & Groups

| Method                     | Description                                | Example Output |
|----------------------------| ------------------------------------------ | -------------- |
| `.StartGroup()`            | Starts a non-capturing group (`(?:`).      | `(?:`          |
| `.StartCaptureGroup()`     | Starts a capturing group (`(`).            | `(`            |
| `.StartNamedGroup("name")` | Starts a named capturing group.            | `(?<name>`     |
| `.EndGroup()`              | Ends a group (`)`).                        | `)`            |
| `.StartAnchor()`           | Adds a start anchor (`^`).                 | `^`            |
| `.EndAnchor()`             | Adds an end anchor (`$`).                  | `$`            |
| `.WordBoundary()`          | Adds a word boundary assertion (`\b`).     | `\b`           |
| `.NonWordBoundary()`       | Adds a non-word boundary assertion (`\B`). | `\B`           |

### Validation Helpers

| Method                   | Description                              | Example Output     |
|--------------------------| ---------------------------------------- | ------------------ |
| `.HasSpecialCharacter()` | Adds a lookahead for special characters. | `(?=.*[!@#$%^&*])` |
| `.HasDigit()`            | Adds a lookahead for digits.             | `(?=.*\d)`         |
| `.HasLetter()`           | Adds a lookahead for letters.            | `(?=.*[a-zA-Z])`   |

### URL Components

| Method        | Description                            | Example Output            |
|---------------| -------------------------------------- | ------------------------- |
| `.Protocol()` | Adds a protocol pattern (`https?://`). | `https?://`               |
| `.Www()`      | Adds a www pattern (`(www\.)?`).       | `(www\.)?`                |
| `.Path()`     | Adds a path pattern (`(/\w+)*`).       | `(/\w+)*`                 |
| `.Tld()`      | Adds a top-level domain pattern.       | \[\"\(com\|org\|net\)\"\] |

### Flags

| Method            | Description                           | Example Output |
|-------------------| ------------------------------------- | -------------- |
| `.Global()`       | Adds the global flag (`g`).           | `g`            |
| `.NonSensitive()` | Adds the case-insensitive flag (`i`). | `i`            |
| `.Multiline()`    | Adds the multiline flag (`m`).        | `m`            |

