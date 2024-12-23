using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenBirthDateCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<uint> age = new("age", "Age from which the birthday date will be get");

        Command command = new("birthdate", "Generates the birth date corresponding to an age")
        {
            age
        };

        command.SetHandler((uint age, IConsole console) =>
        {
            try
            {
                string birthDate = new BirthDate().ByAge(age);
                console.Out.WriteLine(birthDate);
            }
            catch (Exception)
            {
                console.Error.WriteLine("Invalid 'age' value for date generation");
            }
        }, age);

        return command;
    }
}
