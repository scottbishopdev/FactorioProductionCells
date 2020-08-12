using System;
using System.Collections.Generic;
using System.Text;

namespace FactorioProductionCells.TestData.Common
{
    public class TestDataHelpers
    {   
        public static String GetRandomCharacterStringFromSet(String characterSet, Int32 length)
        {
            if (length <= 0 || String.IsNullOrEmpty(characterSet)) return String.Empty;

            var random = new Random();
            var stringBuilder = new StringBuilder();

            while (String.IsNullOrEmpty(stringBuilder.ToString()) || String.IsNullOrWhiteSpace(stringBuilder.ToString()))
            {
                stringBuilder.Clear();
                while (stringBuilder.Length < length)
                {
                    stringBuilder.Append(characterSet[random.Next(characterSet.Length)]);
                }
            }

            return stringBuilder.ToString();
        }

        public static String GetRandomizedUnicodeCharacterString(Int32 length)
        {
            var random = new Random();
            var stringBuilder = new StringBuilder();
            
            while (stringBuilder.Length < length)
            {
                var character = Convert.ToChar(random.Next(char.MinValue, char.MaxValue));
                if (!char.IsControl(character) && !char.IsSurrogate(character))
                {
                    stringBuilder.Append(character);
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}
