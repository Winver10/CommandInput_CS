using System;
using System.Collections.Generic;
using CommandLine.CommandValues_CS;
using CommandLine.CommandPlaceHolder_CS;
using System.Text;

namespace CommandLine.CommandExplainer_CS;

public static class Explainer
{
    static public List<string> ExplainCommand(string s)
    {
        var Command = new List<string>();

        if (string.IsNullOrEmpty(s))
        {
            Command.Add(PlaceHolders.PlaceHolderForSpace);
        }
        else
        {
            s = s.Trim();

            bool inQuotes = false;
            StringBuilder current = new();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if ( c == '"')
                {
                    if (inQuotes && i + 1 < s.Length && s[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else inQuotes = !inQuotes;
                }
                else if (c == ' ' && !inQuotes)
                {
                    if (current.Length > 0)
                    {
                        Command.Add(current.ToString());
                        current.Clear();
                    }
                }
                else current.Append(c);
            }

            if (current.Length > 0) Command.Add(current.ToString());
        }

        return Command;
    }
}
