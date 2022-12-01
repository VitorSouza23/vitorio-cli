using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public class ConvertToBae64Command : ICommandFactory
{
    public Command Create()
    {

        Argument<string> input = new("input", () => string.Empty, "Uma entreda de texto para ser codificada em base64");
        Option<string> file = new(new string[] { "--file", "-f" }, "Caminho absoluto do arquivo (com o nome e extensão do arquivo)");
        Option<string> outputFilePath = new(new string[] { "--output", "-o" }, () => string.Empty, "Especifica o caminho de um arquivo de destino para a saída do base64");

        Command command = new("toBase64", "Converte uma entrada para codificação Base64")
        {
            input,
            file,
            outputFilePath
        };

        command.SetHandler(async (string input, string file, string outputFilePath, IConsole console) =>
        {
            try
            {
                string base64 = string.Empty;
                if (string.IsNullOrWhiteSpace(file) is false)
                {
                    if (File.Exists(file) is false)
                    {
                        console.Error.WriteLine($"O arquivo {file} não existe ou o caminho é inválido");
                        return;
                    }

                    byte[] contentArray = await File.ReadAllBytesAsync(file, default);
                    base64 = ToBase64String(contentArray);
                }
                else
                {
                    base64 = ToBase64String(Encoding.UTF8.GetBytes(input));
                }


                if (string.IsNullOrWhiteSpace(outputFilePath))
                    console.Out.WriteLine(base64);
                else
                {
                    try
                    {
                        using var outputFile = File.Open(outputFilePath, FileMode.OpenOrCreate);
                        using var writer = new StreamWriter(outputFile);
                        await writer.WriteLineAsync(base64);

                        console.Out.WriteLine($"Base64 escrito em: {outputFilePath}");
                    }
                    catch
                    {
                        console.Error.WriteLine($"Erro ao escrever no arquivo de saída: {outputFilePath}");
                        return;
                    }
                }
            }
            catch
            {
                console.Error.WriteLine("Erro ao tentar converter a imagem");
                return;
            }

        }, input, file, outputFilePath);

        return command;
    }
}