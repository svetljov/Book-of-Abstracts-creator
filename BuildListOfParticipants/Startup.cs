namespace BuildListOfParticipants
{
    using System;
    using System.IO;
    using System.Text;

    public class Startup
    {
        public static StringBuilder content = new StringBuilder();
        public static string[] fileText;

        public static void Main(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
            {
                string InputFileName = args[0];
                string OutputFileName = InputFileName + ".tex";

                fileText = File.ReadAllLines(InputFileName);

                LatexSpecificStrings.includePreamble();
                EssentialStringOperations.sortNames();
                EssentialStringOperations.includeParticipantsList();
                LatexSpecificStrings.includeMiddleFormating(3);
                EssentialStringOperations.includeCountriesSortingVertical(3); // 4 = numberOfColumns
                LatexSpecificStrings.includeEnding();

                File.WriteAllText(OutputFileName, content.ToString());

                Console.WriteLine("Press key to exit.");
                Console.ReadLine();
            }
            else
            {
                throw new ArgumentException("Drag and drop a valid text file.");
            }
        }

    }


}
