﻿using System.CommandLine;
using Vitorio.CLI.Commands.About;
using Vitorio.CLI.Commands.Gen;

RootCommand rootCommand = new("CLI com ferramentas que podem (ou não) serem úteis para o dia a dia do desenvolvedor");
rootCommand.AddCommand(new AboutCommand().Create());
rootCommand.AddCommand(new GenCommand().Create());

return rootCommand.InvokeAsync(args).Result;