namespace BuildListOfParticipants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EssentialStringOperations
    {
        private static string[] text = Startup.fileText;

        private static Dictionary<string, int> countryToNumber = new Dictionary<string, int>();

        public static void includeCountriesSortingVertical(int numberOfColumns)
        {
            foreach (var item in text)
            {
                int firstIndex = 1 + item.IndexOf('(');
                int length = item.IndexOf(')', firstIndex) - firstIndex;
                string city = item.Substring(firstIndex, length);

                if (!Locations.countryByCity.ContainsKey(city))
                {
                    Console.WriteLine(ExceptionMessages.UnknownCityInDictionary + ": " + city);
                    return;
                }

                string country = Locations.countryByCity[city];
                if (!countryToNumber.ContainsKey(country))
                    countryToNumber.Add(country, 0);

                int existingParticipantNumber = countryToNumber[country];
                existingParticipantNumber++;
                countryToNumber.Remove(country);
                countryToNumber.Add(country, existingParticipantNumber);
            }

            foreach (var item in countryToNumber)
            {
                Console.WriteLine(item.Key + "->" + item.Value);
            }
            Console.WriteLine(line(1) + "Countries: " + countryToNumber.Count);
            Console.WriteLine("Participants: " + text.Length);

            // Create country list
            string[] countryList = countryToNumber.Keys.OrderBy(x => x).ToArray();
            int longestCountryName = countryList.Select(x => x.Length).Max();

            int numberOfRows = (int)Math.Ceiling(countryList.Length / ((double)numberOfColumns));

            int counter = 0;
            StringBuilder countryString = new StringBuilder();
            for (int row = 0; row < numberOfRows - 1; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    string country = countryList[counter];
                    countryString.Append(country.PadRight(5 + longestCountryName) + ($"&{countryToNumber[country]}&").PadRight(5));
                    counter++;
                }
                countryString.Append(@"\\" + line(1));
            }

            for (int column = (numberOfRows - 1) * numberOfColumns; column < countryList.Length; column++)
            {
                string country = countryList[counter];
                countryString.Append(country.PadRight(5 + longestCountryName) + ($"&{countryToNumber[country]}&").PadRight(5));
                counter++;
            }

            Startup.content.Append(countryString + line(1));

        }

        public static void sortNames()
        {
            var sortedNames = text.OrderBy(name =>
            {
                int firstIndex = 1 + name.IndexOf(' ');
                int length = name.IndexOf(' ', firstIndex) - firstIndex;
                if (length < 0)
                {
                    Console.WriteLine($"Warning: Entry {name} must be in the format name1 name2 (city)");
                }

                return name.Substring(firstIndex, length);
            }).ToArray();

            text = sortedNames;
        }

        public static void includeParticipantsList()
        {
            int numberOfRows = (int)Math.Ceiling(text.Length / 2.0); Console.WriteLine(numberOfRows); Console.WriteLine(text.Length);
            int maxRowLength = text.Select(name => name.Length).Max();

            for (int row = 0; row < numberOfRows - 1; row++)
            {
                Startup.content.Append($"{text[row]}".PadRight(maxRowLength + 5));
                Startup.content.Append(" & ");
                Startup.content.Append($"{text[row + numberOfRows]}".PadRight(maxRowLength + 5));
                Startup.content.Append(@"\\");
                Startup.content.Append(line(1));
            }
            if (text.Length % 2 == 0)
            {
                Startup.content.Append($"{text[numberOfRows - 1]}".PadRight(maxRowLength + 5));
                Startup.content.Append(" & ");
                Startup.content.Append($"{text[2 * numberOfRows - 1]}".PadRight(maxRowLength + 5));
                Startup.content.Append(@"\\");
                Startup.content.Append(line(1));
            }
            else
            {
                Startup.content.Append($"{text[numberOfRows - 1]} & ");
                Startup.content.Append(@"\\");
                Startup.content.Append(line(1));
            }

        }

        private static string line(int lineCount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lineCount; i++)
            {
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
