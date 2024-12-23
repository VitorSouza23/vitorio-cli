using System.CommandLine.Binding;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenNameCommand : ICommandFactory
{
    public Command Create()
    {
        Option<char> gender = new(["--gender", "-g"], () => 'R', "Generates female (F), male (M) or all (A) name");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of names to be generated");

        Command command = new("name", "Generate random name")
        {
            gender,
            count
        };

        command.SetHandler((char gender, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            NameGender nameGender = gender switch
            {
                'F' or 'f' => NameGender.Feminine,
                'M' or 'm' => NameGender.Masculine,
                'A' or 'a' => NameGender.All,
                _ => NameGender.NotDefined
            };

            if (nameGender == NameGender.NotDefined)
            {
                console.Error.WriteLine("--gender must be 'A' (All), 'F' (Female) or 'M' (Male)");
                return;
            }

            Name name = new(new Random());
            for (int index = 0; index < count; index++)
            {
                console.Out.WriteLine(name.New(nameGender));
            }
        }, gender, count);

        return command;
    }
}
