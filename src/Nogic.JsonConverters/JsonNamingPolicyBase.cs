using System.ComponentModel;

namespace Nogic.JsonConverters;

/// <summary>Helper for <see cref="JsonNamingPolicy"/></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class JsonNamingPolicyBase : JsonNamingPolicy
{
    /// <summary>
    /// <inheritdoc cref="JsonNamingPolicyBase(char)" path="/param[@name='separator']"/>
    /// </summary>
    private readonly char _separator;

    /// <summary>
    /// Initializes a new instance of <see cref="JsonNamingPolicyBase"/>.
    /// </summary>
    /// <param name="separator">Word Separator</param>
    protected JsonNamingPolicyBase(char separator) => _separator = separator;

    /// <summary>
    /// Returns need to insert separator or not.
    /// </summary>
    /// <param name="c">original char</param>
    protected virtual bool IsWordSeparator(char c)
        => char.IsUpper(c) || char.IsDigit(c) || IsSkipWrite(c);

    /// <summary>
    /// Returns need to write <paramref name="c"/> or not.
    /// </summary>
    /// <param name="c">original char</param>
    protected virtual bool IsSkipWrite(char c)
        => "-_".Contains(c) || char.IsWhiteSpace(c);

    /// <summary>
    /// Convert <paramref name="c"/> for write <see langword="char[]"/>.
    /// Used in <see cref="ConvertName"/>.
    /// </summary>
    /// <param name="isTopOfWord">Cursor is top of word or not</param>
    /// <param name="c">original char</param>
    protected abstract char ConvertForWrite(bool isTopOfWord, char c);

    /// <inheritdoc/>
    public sealed override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        int length = 0;
        bool isTopOfWord = true;
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            if (IsWordSeparator(c))
            {
                if (!isTopOfWord)
                {
                    isTopOfWord = true;
                    length++;
                }
            }
            else
            {
                isTopOfWord = false;
            }
            if (!IsSkipWrite(c))
                length++;
        }
        char[] buf = new char[length];

        isTopOfWord = true;
        int written = 0;
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            if (IsWordSeparator(c))
            {
                if (!isTopOfWord)
                {
                    isTopOfWord = true;
                    buf[written++] = _separator;
                }
            }
            else
            {
                isTopOfWord = false;
            }
            if (!IsSkipWrite(c))
                buf[written++] = ConvertForWrite(isTopOfWord, c);
        }
        return new string(buf);
    }
}
