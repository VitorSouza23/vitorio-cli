using System.CommandLine;
using System.CommandLine.Invocation;
using Vitorio.CLI.Extensions;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatCPFCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cpf", "Formata um CPF com a pontuação padrão")
            {
                new Argument<string>("cpf", "CPF a ser formatado"),
                new Option<bool>(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CPF")
            };

            command.Handler = CommandHandler.Create((string cpf, bool remove, IConsole console) =>
            {
                if (string.IsNullOrWhiteSpace(cpf))
                {
                    console.Error.Write("cpf não pode ser vazio\n");
                    return 1;
                }
                if (remove)
                    console.Out.Write(cpf.RemoveCPFFormatation() + "\n");
                else
                {
                    if (!cpf.IsCPF())
                    {
                        console.Error.Write("cpf precisa ser uma sequência de 11 digitos\n");
                        return 1;
                    }
                    else
                        console.Out.Write(cpf.FormatCPF() + "\n");
                }
                return 0;
            });

            return command;
        }
    }
}