using System.CommandLine.Binding;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenNameCommand : ICommandFactory
{
    public Command Create()
    {
        Option<char> gender = new(new string[] { "--gender", "-g" }, () => 'A', "Gera nome feminino (F), mascilino (M) ou aleatório (A)");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de nomes a serem gerados");

        Command command = new("name", "Gera nome aleatório")
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
                console.Error.WriteLine("--gender deve ser 'A' (Todos), 'F' (Feminino) ou 'M' (Masculino)");
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
