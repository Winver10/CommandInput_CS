using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CommandLine.CommandDisplay_CS;

struct StuatShoot
{
    public readonly bool isSpace;
    public readonly bool isLink;
    public readonly bool inQuete;

    public StuatShoot(bool _isSpace, bool _isLink, bool _inQuete)
    {
        isSpace = _isSpace;
        isLink = _isLink;
        inQuete = _inQuete;
    }

    public (bool isSpace, bool isLink, bool inQuete) ToTuple()
    {
        return (isSpace, isLink, inQuete);
    }
}

public class DisplayProcesser
{
    private bool _isSpace =false;
    private bool _isLink = false;
    private bool _inQuete = false;
    private Stack<StuatShoot> StuatShoots = new();
    private void SaveStuat()
    {
        StuatShoots.Push(new StuatShoot(_isSpace, _isLink, _inQuete));
    }
    public ConsoleColor GetConsoleColor(char c)
    {
        if (c == '"' && !_inQuete)
         {
            _inQuete = true;
            SaveStuat();
            return ConsoleColor.Blue;
        }
        if (c == '"' && _inQuete)
        {
            _inQuete = false;
            SaveStuat();
            return ConsoleColor.Blue;
        }
        if (c == ' ' && !_isSpace)
        {
            _isSpace = true;
            SaveStuat();
            return ConsoleColor.Gray;   
        }
        if (c == '-' && !_isLink)
        {
            _isLink = true;
            SaveStuat();
            return ConsoleColor.Gray;
        }

        if (c != ' ') _isSpace = false;

        if (_inQuete)
        {
            SaveStuat();
            return ConsoleColor.Blue;
        }
        else if (_isLink)
        {
            SaveStuat();
            return ConsoleColor.Gray;
        }
        else
        {
            SaveStuat();
            return ConsoleColor.White;
        }

    }

    public void RestoreStuat()
    {
        if (StuatShoots.Count > 0)
        {
            var snap = StuatShoots.Pop();
            _isSpace = snap.isSpace;
            _isLink = snap.isLink;
            _inQuete = snap.inQuete;
        }
        else
        {
            _isSpace =false;
            _isLink = false;
            _inQuete = false;
            StuatShoots.Clear();
        }
    }

    public int InputLength()
    {
        return StuatShoots.Count;
    }
}

static class Display
{
    public static void DisplayTextWithColor(string text, DisplayProcesser displayProcesser)
    {
        if (string.IsNullOrEmpty(text))
        return;

        for (int i = 0; i < text.Length; i++)
        {
            Console.ForegroundColor = displayProcesser.GetConsoleColor(text[i]);
            Console.Write(text[i]);
            Console.ResetColor();
        }
    }

    public static void DisplayTextWithColor(char c, DisplayProcesser displayProcesser)
    {
        if ((char.IsControl(c)))
            return ;

        Console.ForegroundColor = displayProcesser.GetConsoleColor(c);
        Console.Write(c);
        Console.ResetColor();
    }

    public static void Backspace(DisplayProcesser displayProcesser)
    {
        Console.Write("\b \b");
        displayProcesser.RestoreStuat();
    }

    public static void ClearLine(int InputLength)
    {
        Console.Write("\r");
        Console.Write(new string(' ', InputLength));
        Console.Write("\r");
    }
}
