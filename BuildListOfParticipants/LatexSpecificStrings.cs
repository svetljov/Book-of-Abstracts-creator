using System;
using System.Text;

namespace BuildListOfParticipants
{
    public class LatexSpecificStrings
    {
        public static void includePreamble()
        {
            Startup.content.Append(
            @"\addcontentsline{toc}{chapter}{List of Participants}" + line(1) +
                                @"%\chapter*{List of participants}" + line(1) +
                                @"\ \vspace{15mm}" + line(1) +
                                @"\begin{center}{\hspace{-2cm}\Huge{\textbf{List of Participants}}}\\\end{center}" + line(2) +
                                @"\begin{center}" + line(1) +
                                @"\hspace{-2cm}\renewcommand{\tabcolsep}{4mm}\btt[ll]" + line(2));
        }

        public static void includeMiddleFormating(int numberOfColumns)
        {
            Startup.content.Append(@"\et" + line(2));
            Startup.content.Append(@"\vspace{10mm}" + line(2));
            Startup.content.Append(@"\renewcommand{\tabcolsep}{5mm}" + line(1));
            Startup.content.Append(@"\hspace{-12mm}" + line(1));
            string alignmentString = new string('l', 2 * numberOfColumns + 1);
            Startup.content.Append($"\\btt[{alignmentString}]" + line(1));
        }

        public static void includeEnding()
        {
            Startup.content.Append(@"\et" + line(1));
            Startup.content.Append(@"\end{center}");
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
