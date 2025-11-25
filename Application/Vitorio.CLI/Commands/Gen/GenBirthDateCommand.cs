using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenBirthDateCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<uint> age = new("age")
        {
            Description = "Age from which the birthday date will be get"
        };

        Command command = new("birthdate", "Generate birth date corresponding to an age")
        {
           age 
        };

        command.SetAction(parseResult =>
        {
            var ageValue = parseResult.GetValue(age);
            try
            {
                string birthDate = new BirthDate().ByAge(ageValue);
                Console.WriteLine(birthDate);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Invalid 'age' value for date generation");
            }
        });

        return command;
    }
}
