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
        uint index = 0, real_index = index;

        while(true)
        {
            enter.loopBegin();
            enter.writeCommandSign();
            input = Input.GetInput(input);

            if (input == PlaceHolders.PlaceHolderForSpace)
            {
                input = string.Empty;
                enter.withoutCommand();
            }
            else if (input == PlaceHolders.PlaceHolderForMoreAction + "UP")
            {
                if (ValueInCommandInput.CommandInputHistory.Count == 0)
                {
                    index = 0;
                }
                else
                {
                    if (index <= 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index--;
                        Display.ClearLine(Console.WindowWidth);
                        enter.writeCommandSign();
                        input = ValueInCommandInput.CommandInputHistory[Convert.ToInt32(index)];
                    }
                }
            }
            else if (input == PlaceHolders.PlaceHolderForMoreAction + "DOWN")
            {
                if (index + 2 >= ValueInCommandInput.CommandInputHistory.Count)
                {
                    index = real_index;
                }
                else
                {
                    index++;
                    Display.ClearLine(Console.WindowWidth);
                    enter.writeCommandSign();
                    input = ValueInCommandInput.CommandInputHistory[Convert.ToInt32(index)];
                }
            }
            else
            {
                if (uint.Max(index, real_index) - uint.Min(index, real_index) >= 1)
                {
                    ValueInCommandInput.CommandInputHistory.Add(input);
                    real_index = Convert.ToUInt32(ValueInCommandInput.CommandInputHistory.Count - 1);
                }
                index = real_index;
                enter.withCommand(input);
            }
        }
    }
}