#nullable enable
using System;
using System.Diagnostics;
using System.Text;
using CommandLine.CommandDisplay_CS;
using Microsoft.VisualBasic;

namespace CommandLine.CommandInputProcesser_CS;

static public class Input
{
    static private string ProccessInput(string? initialText, DisplayProcesser display)
    {
        StringBuilder input = new();
        ConsoleKeyInfo keyInfo;

        if (!string.IsNullOrEmpty(initialText))
        {
            Display.DisplayTextWithColor(initialText, display);
            input.Append(initialText);
        }

        do
        {
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                return CommandPlaceHolder_CS.PlaceHolders.PlaceHolderForMoreAction + "_UP";
            }
            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                return CommandPlaceHolder_CS.PlaceHolders.PlaceHolderForMoreAction + "_DOWN";
            }

            if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Length--;
                Display.Backspace(display);
                continue;
            }

            if (!char.IsControl(keyInfo.KeyChar))
            {
                Display.DisplayTextWithColor(keyInfo.KeyChar, display);
                input.Append(keyInfo.KeyChar);
            }
        }while (keyInfo.Key != ConsoleKey.Enter);

        Console.Write('\n');
        if (input.Length != 0)
            return input.ToString();
        else
            return CommandPlaceHolder_CS.PlaceHolders.PlaceHolderForSpace;
    }

/// <summary>
/// A function to get the input
/// </summary>
/// <param name="LastCommand">The last commad in the loop</param>
/// <returns></returns>
    static public string GetInput(string? LastCommand)
    {
        var display = new DisplayProcesser();
        return ProccessInput(LastCommand, display);
    }
}
