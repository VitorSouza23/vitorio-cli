using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatDateCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("date", "Formate uma data de acordo com uma máscara")
            {
                new Argument<string>("date", "Valor da data a ser formatada"),
                new Option<string>(new string[] { "--mask", "-m" }, () => "dd/MM/yyyy hh:mm:ss", "Máscara para o formatação da data"),
                new Option<bool>(new string[] { "--json", "-j" }, "Formata a data para json")
            };

            command.Handler = CommandHandler.Create((string date, string mask, bool json, IConsole console) =>
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    console.Error.Write("O valor da data não pode ser vazio\n");
                    return 1;
                }

                if (DateTime.TryParse(date, out DateTime theDate))
                {
                    if (json)
                    {
                        string jsonDate = JsonSerializer.Serialize(theDate);
                        console.Out.Write(jsonDate + "\n");
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(mask))
                        {
                            console.Error.Write("--mask não pode ser vazio\n");
                            return 1;
                        }

                        console.Out.Write(theDate.ToString(mask) + "\n");
                    }
                }
                else
                {
                    console.Error.Write($"O valor {date} não foi reconhecido como uma data válida\n");
                    return 1;
                }
                return 0;
            });

            return command;
        }
    }
}