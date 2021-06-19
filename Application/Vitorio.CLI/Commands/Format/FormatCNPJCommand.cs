
using System.CommandLine;
using System.CommandLine.Invocation;
using Vitorio.CLI.Extensions;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatCNPJCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cnpj", "Formata um CNPJ com a pontuação padrão")
            {
                new Argument<string>("cnpj", "CNPJ a ser formatado"),
                new Option<bool>(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CNPJ")
            };

            command.Handler = CommandHandler.Create((string cnpj, bool remove, IConsole console) =>
            {
                if (string.IsNullOrWhiteSpace(cnpj))
                {
                    console.Error.Write("cnpj não pode ser vazio\n");
                    return 1;
                }
                if (remove)
                    console.Out.Write(cnpj.RemoveCNPJFormatation() + "\n");
                else
                {
                    if (!cnpj.IsCNPJ())
                    {
                        console.Error.Write("cnpj precisa ser uma sequência de 14 digitos\n");
                        return 1;
                    }
                    else
                        console.Out.Write(cnpj.FormatCNPJ() + "\n");
                }
                return 0;
            });

            return command;
        }
    }
}