using System.IO;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public class ConvertImageToBae64Command : ICommandFactory
{
    public Command Create()
    {
        Argument<string> imagePath = new("imagePath", "Caminho absoluto do arquivo de imagem (com o nome e extensão do arquivo)");
        Option<string> outputFilePath = new(new string[] { "--output", "-o" }, () => string.Empty, "Especifica o caminho de um arquivo de destino para a saída do base64");

        Command command = new("image-to-base64", "Converte uma arquivo de imagem para Base64")
        {
            imagePath,
            outputFilePath
        };

        command.SetHandler(async (string imagePath, string outputFilePath, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                console.Error.WriteLine("O caminho absoluto da imagem não pode ser vazio");
                return;
            }

            if (File.Exists(imagePath) is false)
            {
                console.Error.WriteLine($"O arquivo {imagePath} não existe ou o caminho é inválido");
                return;
            }

            try
            {
                byte[] imageArray = await File.ReadAllBytesAsync(imagePath, default);
                string base64 = ToBase64String(imageArray);

                if (string.IsNullOrWhiteSpace(outputFilePath))
                    console.Out.WriteLine(base64);
                else
                {
                    try
                    {
                        using var file = File.Open(outputFilePath, FileMode.OpenOrCreate);
                        using var writer = new StreamWriter(file);
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

        }, imagePath, outputFilePath);

        return command;
    }
}