using System.IO;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public class ConvertImageToBae64Command : ICommandFactory
{
    public Command Create()
    {
        Argument<string> imagePath = new("imagePath", "Caminho absoluto do arquivo de imagem (com o nome e extensão do arquivo)");

        Command command = new("image-to-base64", "Converte uma arquivo de imagem para Base64")
        {
            imagePath
        };

        command.SetHandler(async (string imagePath, IConsole console) =>
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
                console.Out.WriteLine(base64);
            }
            catch
            {
                console.Error.WriteLine("Unspected error during image convertion.");
                return;
            }

        }, imagePath);

        return command;
    }
}