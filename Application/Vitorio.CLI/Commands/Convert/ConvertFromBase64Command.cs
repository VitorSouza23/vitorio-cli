using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public sealed class ConvertFromBae64Command : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", () => string.Empty, "A base64 text input to be decoded");
        Option<string> file = new(["--file", "-f"], "Absolute path to base64 file (with file name and extension)");
        Option<string> outputFilePath = new(["--output", "-o"], () => string.Empty, "Specifies the path of a destination file for the decoded file output");

        Command command = new("fromBase64", "Converts an input to Base64 decoding")
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
                        console.Error.WriteLine($"The file {file} does not exist or the path is invalid");
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

                        console.Out.WriteLine($"Content written in: {outputFilePath}");
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
