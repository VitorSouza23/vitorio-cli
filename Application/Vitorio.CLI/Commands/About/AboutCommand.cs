namespace Vitorio.CLI.Commands.About;

public class AboutCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("about", "About this CLI");

        command.SetAction(_ =>
        {
            Console.WriteLine("""
This CLI was based on my needs for generating and manipulating data during work.

All data generated and manipulated by it are the sole responsibility of those who use it.

All help and suggestions are welcome.

(っ▀¯▀)つ
""");
        });

        return command;
    }
}
