# CommandInput_CS

Here is the Project CommandInput_CS

## How to use

You can see the Example in Program.cs. 

After a few time, you can download the NuGet Package **"CommandInput_CS"**. 

## How it works

First, Your input will into **Console.ReadKey()** and return to the value **keyInfo** in Function **CommandInputProcesser_CS.Input.ProcessInput()**.

Then, the input will be printed in console and push the input into the StringBuilder. 

Then, if you press the key **Enter**, the StringBuilder will be turned into string and be returned.

You command will be explained in **CommandExplainer_CS.Eplaner.ExplainCommand()**, this function will slice the command. For more ditail, you can find the logic in **Explaier.cs**