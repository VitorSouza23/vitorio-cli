using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text.Json;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatDateCommand : ICommandFactory
    {
        public Command Create()
        {
            Argument<string> date = new("date", "Valor da data a ser formatada");
            Option<string> mask = new(new string[] { "--mask", "-m" }, () => "dd/MM/yyyy hh:mm:ss", "Máscara para o formatação da data");
            Option<bool> json = new(new string[] { "--json", "-j" }, () => false, "Formata a data para json");

            Command command = new("date", "Formate uma data de acordo com uma máscara")
            {
                date,
                mask,
                json
            };

            command.SetHandler((string date, string mask, bool json, IConsole console) =>
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    console.Error.WriteLine("O valor da data não pode ser vazio");
                    return;
                }

                if (DateTime.TryParse(date, out DateTime theDate))
                {
                    if (json)
                    {
                        string jsonDate = JsonSerializer.Serialize(theDate);
                        console.Out.WriteLine(jsonDate);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(mask))
                        {
                            console.Error.WriteLine("--mask não pode ser vazio");
                            return;
                        }

                        console.Out.WriteLine(theDate.ToString(mask));
                    }
                }
                else
                {
                    console.Error.WriteLine($"O valor {date} não foi reconhecido como uma data válida");
                    return;
                }
            }, date, mask, json);

            return command;
        }
    }
}