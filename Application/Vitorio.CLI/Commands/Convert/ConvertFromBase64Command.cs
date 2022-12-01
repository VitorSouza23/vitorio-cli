using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public sealed class ConvertFromBae64Command : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", () => string.Empty, "Uma entreda de texto base64 para ser decodificada");
        Option<string> file = new(new string[] { "--file", "-f" }, "Caminho absoluto do arquivo base64 (com o nome e extensão do arquivo)");
        Option<string> outputFilePath = new(new string[] { "--output", "-o" }, () => string.Empty, "Especifica o caminho de um arquivo de destino para a saída do arquivo decodificado");

        Command command = new("fromBase64", "Converte uma entrada para decodificação Base64")
        {
            input,
            file,
            outputFilePath
        };

        command.SetHandler(async (string input, string file, string outputFilePath, IConsole console) =>
        {
            try
            {
                string content = string.Empty;
                if (string.IsNullOrWhiteSpace(file) is false)
                {
                    if (File.Exists(file) is false)
                    {
                        console.Error.WriteLine($"O arquivo {file} não existe ou o caminho é inválido");
                        return;
                    }

                    byte[] imageArray = await File.ReadAllBytesAsync(file, default);
                    content = Encoding.UTF8.GetString(imageArray);
                    var contentArray = FromBase64String(content);
                    content = Encoding.UTF8.GetString(contentArray);
                }
                else
                {
                    var contentArray = FromBase64String(input);
                    content = Encoding.UTF8.GetString(contentArray);
                }


                if (string.IsNullOrWhiteSpace(outputFilePath))
                    console.Out.WriteLine(content);
                else
                {
                    try
                    {
                        using var outputFile = File.Open(outputFilePath, FileMode.OpenOrCreate);
                        using var writer = new StreamWriter(outputFile);
                        await writer.WriteLineAsync(content);

                        console.Out.WriteLine($"Conteúdo escrito em: {outputFilePath}");
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
