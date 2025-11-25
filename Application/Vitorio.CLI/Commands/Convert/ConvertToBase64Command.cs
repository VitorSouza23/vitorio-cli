using System.IO;
using System.Text;
using static System.Convert;

namespace Vitorio.CLI.Commands.Convert;

public class ConvertToBae64Command : ICommandFactory
{
    public Command Create()
    {

        Argument<string> input = new("input")
        {
            Description = "The string to be converted to Base64",
            DefaultValueFactory = _ => string.Empty
        };
        
        Option<string> file = new("--file", "-f")
        {
            DefaultValueFactory = _ => string.Empty,
            Description = "Path to a file to be converted to Base64"
        };
        Option<string> outputFilePath = new("--output", "-o")
        {
            DefaultValueFactory = _ => string.Empty,
            Description = "Specifies the path of a destination file for base64 output"
        };

        Command command = new("toBase64", "Converts an input to Base64 encoding")
        {
            input,
            file,
            outputFilePath
        };

        command.SetAction(async parseResult =>
        {
            try
            {
                string base64 = string.Empty;
                var fileValue = parseResult.GetValue(file);
                var inputValue = parseResult.GetValue(input);
                var outputFilePathValue = parseResult.GetValue(outputFilePath);
                if (string.IsNullOrWhiteSpace(fileValue) is false)
                {
                    if (File.Exists(fileValue) is false)
                    {
                        Console.Error.WriteLine($"The file {fileValue} does not exist or the path is invalid");
                        return;
                    }

                    byte[] contentArray = await File.ReadAllBytesAsync(fileValue, default);
                    base64 = ToBase64String(contentArray);
                }
                else
                {
                    base64 = ToBase64String(Encoding.UTF8.GetBytes(inputValue));
                }


                if (string.IsNullOrWhiteSpace(outputFilePathValue))
                    Console.WriteLine(base64);
                else
                {
                    try
                    {
                        using var outputFile = File.Open(outputFilePathValue, FileMode.OpenOrCreate);
                        using var writer = new StreamWriter(outputFile);
                        await writer.WriteLineAsync(base64);

                        Console.WriteLine($"Base64 written in: {outputFilePath}");
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