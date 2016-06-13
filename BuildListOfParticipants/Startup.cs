using System;
using System.IO;
using System.Text;

namespace BuildListOfParticipants
{
    public class Startup
    {
        
        public static StringBuilder content = new StringBuilder();
        public static string[] fileText;

        public static void Main()
        {
            string inputFilePath = @"D:\PRESENTATIONS\Book of abstracts\Camel 2016\Additional\ListOfParticipants2016.txt";
            string outputFilePath = @"D:\PRESENTATIONS\Book of abstracts\Camel 2016\Additional\ListOfParticipants2016.tex";

            fileText = File.ReadAllLines(inputFilePath);

            LatexSpecificStrings.includePreamble();
            EssentialStringOperations.sortNames();
            EssentialStringOperations.includeParticipantsList();
            LatexSpecificStrings.includeMiddleFormating(3);
            EssentialStringOperations.includeCountriesSortingVertical(3); // 4 = numberOfColumns
            LatexSpecificStrings.includeEnding();

            File.WriteAllText(outputFilePath, content.ToString());
        }

    }


}
