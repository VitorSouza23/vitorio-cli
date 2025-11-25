using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenNameCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> gender = new("--gender", "-g")
        {
            Description = "Generate famale (F), male (M) or all (A) names",
            DefaultValueFactory = _ => "A"
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of names to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("name", "Generate random name")
        {
            gender,
            count
        };

        command.SetAction(parseResult =>
        {
            var genderValue = parseResult.GetValue(gender);
            var countValue = parseResult.GetValue(count);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            NameGender nameGender = genderValue switch
            {
                "F" or "f" => NameGender.Feminine,
                "M" or "m" => NameGender.Masculine,
                "A" or "a" => NameGender.All,
                _ => NameGender.NotDefined
            };

            if (nameGender == NameGender.NotDefined)
            {
                Console.Error.WriteLine("--gender must be 'A' (All), 'F' (Female) or 'M' (Male)");
                return;
            }

            Name name = new(new Random());
            for (int index = 0; index < countValue; index++)
            {
                Console.WriteLine(name.New(nameGender));
            }
        });

        return command;
    }
}
