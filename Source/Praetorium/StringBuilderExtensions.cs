using System;
using System.Text;

namespace Praetorium
{
    public static class StringBuilderExtensions
    {

        public static StringBuilder AppendFormatIf(this StringBuilder builder, bool condition, string format, params object[] args)
        {
            Ensure.ArgumentNotNull(() => builder);
            Ensure.ArgumentNotNullOrWhiteSpace(() => format);

            if (condition)
                builder.AppendFormat(format, args);

            return builder;
        }

        public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
        {
            Ensure.ArgumentNotNull(() => builder);

            if (condition)
                builder.Append(value);

            return builder;
        }

        /// <summary>
        /// Converts all of the letter characters in the builer to their uppercase equivalent.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public static StringBuilder ToUpper(this StringBuilder builder)
        {
            Ensure.ArgumentNotNull(() => builder);

            for (int index = 0; index < builder.Length; index++)
            {
                char c = builder[index];

                if (char.IsLetter(c) && char.IsLower(c))
                    builder[index] = char.ToUpper(c);
            }

            return builder;
        }

        /// <summary>
        /// Converts all of the letter characters in the builer to their lowercase equivalent.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public static StringBuilder ToLower(this StringBuilder builder)
        {
            Ensure.ArgumentNotNull(() => builder);

            for (int index = 0; index < builder.Length; index++)
            {
                char c = builder[index];

                if (char.IsLetter(c) && char.IsUpper(c))
                    builder[index] = char.ToLower(c);
            }

            return builder;
        }


        /// <summary>
        /// Returns the index of the first occurrence of the specified <paramref name="value"/> in this instance. 
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="source">
        /// The StringBuilder instance.
        /// </param>
        /// <param name="value">
        /// The search string.
        /// </param>
        /// <returns>
        /// The index position of value if that string is found, or -1 if it is not. 
        /// </returns>
        public static int IndexOf(this StringBuilder source, string value)
        {
            Ensure.ArgumentNotNull(() => source);

            return IndexOf(source, value, 0);
        }

        /// <summary>
        /// Returns the index of the first occurrence of the specified <paramref name="value"/> in this instance; 
        /// the search starts at the <paramref name="startIndex"/>
        /// </summary>
        /// <param name="source">
        /// The StringBuilder instance.
        /// </param>
        /// <param name="value">
        /// The search string.
        /// </param>
        /// <param name="startIndex">
        /// The character position at which the search starts.
        /// </param>
        /// <returns>
        /// The index position of value if that string is found, or -1 if it is not. 
        /// </returns>
        public static int IndexOf(this StringBuilder source, string value, int startIndex)
        {
            Ensure.ArgumentNotNull(() => source);
            Ensure.ArgumentNotNullOrWhiteSpace(() => value);

            if (startIndex > source.Length - 1)
                throw new ArgumentException("startIndex may not be greater than the length of the source.", "startIndex");

            if (startIndex < 0)
                startIndex = 0;

            var firstChar = value[0];
            var maxSourceIndex = source.Length - 1;

            if (maxSourceIndex > source.Length - 1)
                return -1;

            for (int sourceIndex = startIndex; sourceIndex <= maxSourceIndex; sourceIndex++)
            {

                // Look for first character.
                if (source[sourceIndex] != firstChar)
                    while (++sourceIndex <= maxSourceIndex && source[sourceIndex] != firstChar);

                // Found first character, now look at the rest of v2
                if (sourceIndex <= maxSourceIndex)
                {
                    int nextSourceIndex = sourceIndex + 1;
                    int endSourceIndex = sourceIndex + value.Length - 1;

                    for (int valueIndex = 1; valueIndex < value.Length && source[nextSourceIndex] == value[valueIndex];
                                  nextSourceIndex++, valueIndex++) ;

                    if (nextSourceIndex - 1 == endSourceIndex)
                        //Found whole string.
                        return sourceIndex;
                }
            }

            return -1;
        }

    }
}
