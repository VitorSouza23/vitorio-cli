# vitorio-cli

# O que é?

Devido algumas experiências diárias no trablaho, surgiu a necessidade de uma feramenta que facilitasse o processo de desnevovimento. 
Basicamente alguma coisa capaz de gerar e manipular alguns dados, pricipalmente para preenchimento de perfis de usuários.

Vitorio CLI (não me julguem, não sou bons com nomes) nasceu para supririr essa necessidade.

# Instalação

## Requisitos
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0) ou superior

## dotnet CLI
Use o CLI do dotnet para realizar a instalação da ferramente
- Exemplo de instalação global:
```
dotnet tool install -g Vitorio.CLI
```

# Como usar

## Help da CLI
```
vitorio --help
vitorio -h
vitorio -?
```

## Comando gen (Gerador)
Comando voltado para geração de dados
```
vitorio gen -h
```

### gen cpf
Gera um CPF válido
Opções:
 - **-f, --formated**: Gera CPF com pontuação [default: False]
 - **-c, --count <count>**: Número de CPFs a serem gerados [default: 1]
Exemplo:
```
-$ vitorio gen cpf
-> 36281768329

~$ vitorio gen cpf -f
-> 913.951.162-61

~$ vitorio gen cpf -c 3
-> 49591234600
-> 83644755337
-> 50000703486
```

### gen cnpj
Gera um CNPJ válido
Opções:
 - **-f, --formated**: Gera CNPJ com pontuação [default: False]
 - **-c, --count <count>**: Número de CNPJs a serem gerados [default: 1]
 
Exemplo:
```
-$ vitorio gen cnpj
-> 02903922000134

~$ vitorio gen cnpj -f
-> 65.528.525/0001-40

~$ vitorio gen cnpj -c 3
-> 12139647000188
-> 88877400000120
-> 46745896000139
```

## Comando format (Formatador)
Formata um dado através de uma máscara

### format cpf
Formata um CPF com a pontuação padrão

Argumentos:
- cpf: CPF a ser formatado

Opções:
 - **-r, --remove**: Remove a formatação do CPF [default: False]
 
Exemplo:
```
-$ vitorio format cpf 36281768329
-> 362.817.683-29

~$ vitorio format cpf 362.817.683-29 -r
-> 36281768329
```

### format cnpj
Formata um CNPJ com a pontuação padrão

Argumentos:
- cnpj: CNPJ a ser formatado

Opções:
 - **-r, --remove**: Remove a formatação do CNPJ [default: False]
 
Exemplo:
```
-$ vitorio format cnpj 02903922000134
-> 02.903.922/0001-34

~$ vitorio format cnpj 02.903.922/0001-34 -r
-> 02903922000134
```

### format date
Formate uma data de acordo com uma máscara

Argumentos:
- date: Valor da data a ser formatada

Opções:
 - **-m, --mask <mask>**: Máscara para o formatação da data [default: dd/MM/yyyy hh:mm:ss]
 - **-j, --json**: Formata a data para json [default: False]
 
Exemplo:
```
-$ vitorio format date "2000-01-01 01:00:00"
-> 01/01/2000 01:00:00

~$ vitorio format date "01/01/2000 1:00:00" -m "yyyy-MM-dd hh:mm.ss"
-> 2000-01-01 01:00.00

~$ vitorio format date "01/01/2000 1:00:00" -m "dddd"
-> sábado

~$ vitorio format date "01/01/2000 1:00:00" -j
-> "2000-01-01T01:00:00"
```

# Sobre a CLI
Todos os dados gerados e manipulados por ela são de total responsabilidade de quem as usa.

Toda ajuda e sugestões são bem-vidas.
