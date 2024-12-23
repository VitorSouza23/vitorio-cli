using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public class ConvertToBae64Command : ICommandFactory
{
    public Command Create()
    {

        Argument<string> input = new("input", () => string.Empty, "A text input to be base64 encoded");
        Option<string> file = new(["--file", "-f"], "Absolute path to the file (with file name and extension)");
        Option<string> outputFilePath = new(["--output", "-o"], () => string.Empty, "Specifies the path of a destination file for base64 output");

        Command command = new("toBase64", "Converts an input to Base64 encoding")
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
                        console.Error.WriteLine($"The file {file} does not exist or the path is invalid");
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

                        console.Out.WriteLine($"Base64 written in: {outputFilePath}");
                    }
                    catch
                    {
                        console.Error.WriteLine($"Error writing to output file: {outputFilePath}");
                        return;
                    }
                }
            }
            catch
            {
                console.Error.WriteLine("Error trying to convert image");
                return;
            }

        }, input, file, outputFilePath);

        return command;
    }
}