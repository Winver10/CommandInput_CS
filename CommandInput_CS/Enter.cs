using System;
using CommandLine.CommandDisplay_CS;
using CommandLine.CommandInputProcesser_CS;
using CommandLine.CommandPlaceHolder_CS;
using CommandLine.CommandValues_CS;

public interface IEnterFunction
{
    void preStart();
    void loopBegin();
    void writeCommandSign();
    void withCommand(string Command);
    void withoutCommand();
}

static public class Logic
{

    static public void Run(IEnterFunction enter)
    {
        enter.preStart();
        string input = string.Empty;
        int index = -1, real_index = index;
        bool fromHistory = false;

        while(true)
        {
            if (!fromHistory)
            {
                enter.loopBegin();
                enter.writeCommandSign();
            }
            input = Input.GetInput(input);
            fromHistory = false;

            if (input == PlaceHolders.PlaceHolderForSpace)
            {
                input = string.Empty;
                enter.withoutCommand();
            }
            else if (input == PlaceHolders.PlaceHolderForMoreAction + "_UP")
            {
                fromHistory = true;
                if (ValueInCommandInput.CommandInputHistory.Count == 0)
                {
                    index = -1;
                    input = string.Empty;
                }
                else if (index <= 0)
                {
                    index = -1;
                    input = ValueInCommandInput.CommandInputHistory[0];
                    Display.ClearLine(Console.WindowWidth);
                    enter.writeCommandSign();
                }
                else
                {
                 
                        Display.ClearLine(Console.WindowWidth);
                        enter.writeCommandSign();
                        input = ValueInCommandInput.CommandInputHistory[Convert.ToInt32(index)];
                    index--;
                   
                }
            }
            else if (input == PlaceHolders.PlaceHolderForMoreAction + "_DOWN")
            {
                fromHistory = true;
                if (index + 1 >= ValueInCommandInput.CommandInputHistory.Count - 1)
                {
                    index = real_index;
                    input = string.Empty;
                    Display.ClearLine(Console.WindowWidth);
                    enter.writeCommandSign();
                }
                else
                {
                    index++;
                    Display.ClearLine(Console.WindowWidth);
                    enter.writeCommandSign();
                    input = ValueInCommandInput.CommandInputHistory[Convert.ToInt32(index + 1)];
                }
            }
            else
            {
                if (int.Max(index, real_index) - int.Min(index, real_index) != 1)
                {
                    ValueInCommandInput.CommandInputHistory.Add(input);
                    real_index = Convert.ToInt32(ValueInCommandInput.CommandInputHistory.Count - 1);
                }
                index = real_index;
                enter.withCommand(input);
                input = string.Empty;
            }
        }
    }
}