using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public sealed class ConvertFromBae64Command : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input")
        {
            Description = "The input string to be decoded from Base64",
            DefaultValueFactory = _ => string.Empty
        };
        Option<string> file = new("--file", "-f")
        {
            DefaultValueFactory = _ => string.Empty,
            Description = "Absolute path to base64 file (with file name and extension)"
        };
        Option<string> outputFilePath = new("--output", "-o")
        {
            Description = "Specifies the path of a destination file for the decoded string output",
            DefaultValueFactory = _ => string.Empty
        };

        Command command = new("fromBase64", "Converts an input to Base64 decoding")
        {
            input,
            file,
            outputFilePath
        };

        command.SetAction(async parseResult =>
        {
            try
            {
                string content = string.Empty;
                var inputValue = parseResult.GetValue(input);
                var fileValue = parseResult.GetValue(file);
                var outputFilePathValue = parseResult.GetValue(outputFilePath);

                if (string.IsNullOrWhiteSpace(fileValue) is false)
                {
                    if (File.Exists(fileValue) is false)
                    {
                        Console.Error.WriteLine($"The file {fileValue} does not exist or the path is invalid");
                        return;
                    }

                    byte[] imageArray = await File.ReadAllBytesAsync(fileValue, default);
                    content = Encoding.UTF8.GetString(imageArray);
                    var contentArray = FromBase64String(content);
                    content = Encoding.UTF8.GetString(contentArray);
                }
                else
                {
                    var contentArray = FromBase64String(inputValue);
                    content = Encoding.UTF8.GetString(contentArray);
                }


                if (string.IsNullOrWhiteSpace(outputFilePathValue))
                    Console.WriteLine(content);
                else
                {
                    try
                    {
                        using var outputFile = File.Open(outputFilePathValue, FileMode.OpenOrCreate);
                        using var writer = new StreamWriter(outputFile);
                        await writer.WriteLineAsync(content);

                        Console.WriteLine($"Content written in: {outputFilePath}");
                    }
                    catch
                    {
                        Console.Error.WriteLine($"Error writing to output file: {outputFilePath}");
                        return;
                    }
                }
            }
            catch
            {
                Console.Error.WriteLine("Error trying to convert image");
                return;
            }

        });

        return command;
    }
}
