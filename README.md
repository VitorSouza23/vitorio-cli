# vitorio-cli

# O que é?

Devido algumas experiências diárias no trabalho, surgiu a necessidade de uma feramenta que facilitasse o processo de desnevovimento. 
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
~$ vitorio gen cpf
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
~$ vitorio gen cnpj
-> 02903922000134

~$ vitorio gen cnpj -f
-> 65.528.525/0001-40

~$ vitorio gen cnpj -c 3
-> 12139647000188
-> 88877400000120
-> 46745896000139
```

### gen guid
Gera um GUID
Opções:
 - **-f, --formated**: Formato de sáida do GUID [default: D]
   - Valores possíveis:
     - D -> 00000000-0000-0000-0000-000000000000
     - N -> 00000000000000000000000000000000
     - B -> {00000000-0000-0000-0000-000000000000}
     - P -> (00000000-0000-0000-0000-000000000000)
     - X -> {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} 
 - **-c, --count <count>**: Número de GUIDs a serem gerados [default: 1]
 
Exemplo:
```
~$ vitorio gen guid
-> b731d16b-5365-4f9e-9cde-ca592342a025

~$ vitorio gen guid -f N
-> 708ac373fb0340a49c08b0275a12d3e9

~$ vitorio gen guid -c 3
-> 06e8badf-4ace-4fff-a4f5-1feadaffe6d7
-> c80099b7-2ed9-4816-84df-80d4a7a65bc0
-> 18bdbbc3-9b74-4f3b-819a-b05c03c323f9
```

### gen email
Gera um e-mail não necessariamente válido
Opções:
 - **-p, --provider <provider>**: Provedor de e-mail personalizado
 - **-d, --domain <domain>**: Domínio do e-mail (Ex: com, com.br, etc.) [default: com]
 - **-c, --count <count>**: Número de e-mails a serem gerados [default: 1]
 
Exemplo:
```
~$ vitorio gen email
-> xrzoc@uwwk9.com

~$ vitorio gen email -p gmail
-> 3nd82@gmail.com

~$ vitorio gen email -d com.br
-> q4egu@c4a66.com.br

~$ vitorio gen email -c 3
-> jmhzn@1s7j4.com
-> 06mow@0wf2r.com
-> ovvt0@7lu64.com
```

### gen password
Gera senha com caracteres aleatórios
Opções:
 - **-l, --length <length>**: Gera CNPJ com pontuação [default: False]Número de caracteres da senha (Min: 3, Max: 16) [default: 8]
 - **-c, --count <count>**: Número de senhas a serem geradas [default: 1]
 
Exemplo:
```
~$ vitorio gen password
-> k6oM@wIZ

~$ vitorio gen password -f 15
-> 4ooSOBnsJQsfF$2

~$ vitorio gen password -c 3
-> y&6CO2Ip
-> vMnl8jF*
-> xY4b0Y#X
```

### gen phone
Gera número de telefone
Opções:
 - **cc, --code-country <code-country>**: Código do país [default: 0]
 - **d, --ddd <ddd>**: Código DDD da localidade de destino [default: 0]
 - **nd, --number-of-digits <number-of-digits>**: Quantidade de dígitos no número (Min = 3, Max = 9) [default: 9]
 - **nf, --not-formated**: Não formata o número telefônico [default: False]
 - **-c, --count <count>**: Número de senhas a serem geradas [default: 1]
 
Exemplo:
```
~$ vitorio gen phone
-> 83153-4648

~$ vitorio gen phone -d 22 -cc 55
-> +55 (22) 31028-6798

~$ vitorio gen phone -d 22 -cc 55 -nf
-> 55 22 431587523

~$ vitorio gen phone -nd 3
-> 711

~$ vitorio gen phone -c 3
-> 45152-2342
-> 28072-6289
-> 88120-9248
```

### gen name
Gera nome aleatório
Opções:
 - **-g, --gender <gender>**: Gera nome feminino (F), mascilino (M) ou aleatório (A) [default: A]
 - **-c, --count <count>**: Número de nomes a serem gerados [default: 1]
 
Exemplo:
```
~$ vitorio gen name
-> Luiz Felipe Souza

~$ vitorio gen name -g F
-> Maitê Carvalho

~$ vitorio gen name -c 3
-> Alícia Santos
-> Ana Luiza Carneiro
-> Luna Silveira
```

### gen cep
Gera CEPs (válidos ou não)
Opções:
 - **-f, --formated**: Gera CEP com pontuação [default: False]
 - **-c, --count <count>**: Número de CEPs a serem gerados [default: 1]
 
Exemplo:
```
~$ vitorio gen cep
-> 05778328

~$ vitorio gen cep -f
-> 63787-540

~$ vitorio gen cep -c 3
-> 22336775
-> 90895609
-> 92083776
```

### gen birthdate
Gera a data de aniversário respecitiva a uma idade

Argumentos:
- age: Idade da qual será estraída a data de aniversário
 
Exemplo:
```
~$ vitorio gen birthdate 30
-> 24/07/1991
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
~$ vitorio format cpf 36281768329
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
~$ vitorio format cnpj 02903922000134
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
~$ vitorio format date "2000-01-01 01:00:00"
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
