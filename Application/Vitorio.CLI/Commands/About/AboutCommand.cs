namespace Vitorio.CLI.Commands.About;

public class AboutCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("about", "Sobre essa CLI");

        command.Handler = CommandHandler.Create((IConsole console) =>
        {
            console.Out.WriteLine(@"Essa CLI foi baseada em minhas necessidades para geração e manipulação de dados durante o trabalho.
Todos os dados gerados e manipulados por ela são de total responsabilidade de quem as usa.
Toda ajuda e sugestão são bem-vidas.
(っ▀¯▀)つ");
        });

        return command;
    }
}
