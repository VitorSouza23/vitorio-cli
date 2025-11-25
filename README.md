# vitorio-cli

# About

Due to some daily experiences at work, the need arose for a tool that would facilitate the development process.

Basically, something capable of generating and manipulating some data, mainly for filling out user profiles.

Vitorio CLI was created to meet this need.

# Installation

## Requirements
- [.NET 10](https://dotnet.microsoft.com/en-us/)

> [!NOTE]
> Before version 2.0.0, the CLI was developed and supported by .NET 5, 6, and 7
> Before version 3.0.0, the CLI was developed and supported by .NET 8 and .NET 9

## dotnet CLI
Use the dotnet CLI to install the tool
- Global installation example:
```
dotnet tool install -g Vitorio.CLI
```

# How to use

## CLI Help
```
vitorio --help
vitorio -h
vitorio -?
```

## Gen Command (Generator)
Command focused on data generation
```
vitorio gen -h
```

### gen cpf
Generates a valid CPF
Options:
 - **-f, --formatted**: Generate CPF with punctuation [default: False]
 - **-c, --count <count>**: Number of CPFs to be generated [default: 1]

Example:
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
Generates a valid CNPJ
Options:
 - **-f, --formatted**: Generate CNPJ with punctuation [default: False]
 - **-c, --count <count>**: Number of CNPJs to be generated [default: 1]
 
Example:
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
Generates a GUID
Options:
 - **-f, --formatted**: GUID output format [default: D]
   - Possible values:
     - D -> 00000000-0000-0000-0000-000000000000
     - N -> 00000000000000000000000000000000
     - B -> {00000000-0000-0000-0000-000000000000}
     - P -> (00000000-0000-0000-0000-000000000000)
     - X -> {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} 
 - **-c, --count <count>**: Number of GUIDs to be generated [default: 1]
 
Example:
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
Generates an email not necessarily valid
Options:
 - **-p, --provider <provider>**: Custom email provider
 - **-d, --domain <domain>**: Email domain (Ex: com, com.br, etc.) [default: com]
 - **-c, --count <count>**: Number of emails to be generated [default: 1]
 
Example:
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
Generates a password with random characters
Options:
 - **-l, --length <length>**: Number of characters in the password (Min: 3, Max: 16) [default: 8]
 - **-c, --count <count>**: Number of passwords to be generated [default: 1]
 
Example:
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
Generates a phone number
Options:
 - **cc, --code-country <code-country>**: Country code [default: 0]
 - **d, --ddd <ddd>**: Area code of the destination location [default: 0]
 - **nd, --number-of-digits <number-of-digits>**: Number of digits in the number (Min = 3, Max = 9) [default: 9]
 - **nf, --not-formatted**: Do not format the phone number [default: False]
 - **-c, --count <count>**: Number of phone numbers to be generated [default: 1]
 
Example:
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
Generates a random name
Options:
 - **-g, --gender <gender>**: Generates a female (F), male (M), or random (A) name [default: A]
 - **-c, --count <count>**: Number of names to be generated [default: 1]
 
Example:
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
Generates ZIP codes (valid or not)
Options:
 - **-f, --formatted**: Generates ZIP code with punctuation [default: False]
 - **-c, --count <count>**: Number of ZIP codes to be generated [default: 1]
 
Example:
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
Generates a birthdate respective to an age

Arguments:
- age: Age from which the birthdate will be extracted
 
Example:
```
~$ vitorio gen birthdate 30
-> 24/07/1991
```

## Command format (Formatter)
Formats data through a mask

### format cpf
Formats a CPF with standard punctuation

Arguments:
- cpf: CPF to be formatted

Options:
 - **-r, --remove**: Removes the CPF formatting [default: False]
 
Example:
```
~$ vitorio format cpf 36281768329
-> 362.817.683-29

~$ vitorio format cpf 362.817.683-29 -r
-> 36281768329
```

### format cnpj
Formats a CNPJ with standard punctuation

Arguments:
- cnpj: CNPJ to be formatted

Options:
 - **-r, --remove**: Removes the CNPJ formatting [default: False]
 
Example:
```
~$ vitorio format cnpj 02903922000134
-> 02.903.922/0001-34

~$ vitorio format cnpj 02.903.922/0001-34 -r
-> 02903922000134
```

### format date
Formats a date according to a mask

Arguments:
- date: Date value to be formatted ["now" to use the current system date, "utc" to use the current UTC date] [default: now]

Options:
 - **-m, --mask <mask>**: Mask for date formatting [default: dd/MM/yyyy hh:mm:ss]
 - **-j, --json**: Formats the date to JSON [default: False]
 
Example:
```
~$ vitorio format date "2000-01-01 01:00:00"
-> 01/01/2000 01:00:00

~$ vitorio format date "01/01/2000 1:00:00" -m "yyyy-MM-dd hh:mm.ss"
-> 2000-01-01 01:00.00

~$ vitorio format date "01/01/2000 1:00:00" -m "dddd"
-> Saturday

~$ vitorio format date "01/01/2000 1:00:00" -j
-> "2000-01-01T01:00:00"
```

### format string 
Formats strings according to a pattern

Arguments:
- input: The string to be formatted

Options:
 - **-u, --upper**: Formats the string in uppercase [default: False]
 - **-l, --lower**: Formats the string in lowercase [default: False]
 
Example:
```
~$ vitorio format string "This is a test" -u
-> THIS IS A TEST

~$ vitorio format string "This is a test" -l
-> this is a test
```

### format string git-branch
Receives a text input and formats it following the Git branch naming pattern

Arguments:
- input: An input that will be formatted with the Git branch name pattern

Options:
 - **-p, --prefix**: Prefix to be added to the branch name separated by '/' [default: empty]
 
Example:
```
~$ vitorio format string git-branch "This is a test"
-> this-is-a-test

~$ vitorio format string git-branch "This is a test" -p "This is a prefix"
-> this-is-a-prefix/this-is-a-test
```

### format string list
Formats the items of a list of strings according to a pattern

Arguments:
- input: A list of strings to be formatted

Options:
 - **sp, --separator**: The separator of each item in the list [default: "\n"]
 - **-p, --prefix**: Prefix to be placed on all items in the list [default: ""]
 - **s, --suffix**: Suffix to be placed on all items in the list [default: ""]
 
Example:
```
~$ vitorio format string list -sp "," -s " " "AAA,BBB,CCC"
-> AAA 
-> BBB 
-> CCC 

~$ vitorio format string list -p "\"" -s "\"," "AAA
> BBB
> CCC"
-> "AAA",
-> "BBB",
-> "CCC",
```

### format guid
Formats a GUID according to the specified format

Arguments:
- guid: The GUID to be formatted

Options:
- **-f, --format**:  The format specifier (N, D, B, P or X) [default: D]

Example:
```
~$ vitorio format guid 56971b9fcac74fe8a15f1a06b645f428
-> 56971b9f-cac7-4fe8-a15f-1a06b645f428

~$ vitorio format guid 56971b9fcac74fe8a15f1a06b645f428 -f X
-> {0x56971b9f,0xcac7,0x4fe8,{0xa1,0x5f,0x1a,0x06,0xb6,0x45,0xf4,0x28}}
```

## Command convert (Converter)
Converts data from one type to another

### convert toBase64
Converts an input to Base64 encoding

Arguments:
- input: A text input to be encoded in base64

Options:
 - **-o, --output**: Specifies the path of a destination file for the base64 output [default: empty]
 - **-f, --file**: Absolute path of the file (with the file name and extension)
 
Example:
```
~$ vitorio convert toBase64 "Test"
-> VGVzdA==

~$ vitorio convert toBase64 -f /home/image.png
-> /9j/4AAQSkZJRgABAQAAAQABAAD/...

~$ vitorio convert toBase64 -f /home/image.png -o /home/test.txt
-> Base64 written to: /home/test.txt
```

### convert fromBase64
Converts an input from Base64 decoding

Arguments:
- input: A base64 text input to be decoded

Options:
 - **-o, --output**: Specifies the path of a destination file for the decoded file output [default: empty]
 - **-f, --file**: Absolute path of the base64 file (with the file name and extension)
 
Example:
```
~$ vitorio convert fromBase64 "VGVzdA=="
-> Test

~$ vitorio convert fromBase64 -f /home/testBase64.txt
-> Test

~$ vitorio convert fromBase64 -f /home/testeBase64.txt -o /home/test.txt
-> Content written to: /home/test.txt
```

## Command date (Date)
Performs operations with dates

### date add
Adds a time interval to a date

Arguments:
- start: Start date ["now" to use the current system date, "utc" to use the current UTC date]
- time: Time interval to be added to the start date

Example:
```
~$ vitorio date add "2021-01-01 00:00:00" "01:00:00"
-> 01/01/2021 01:00:00

~$ vitorio date add now "10:00:00"
-> *current date + 10 hours*

~$ vitorio date add utc "10:00:00"
-> *current UTC date + 10 hours*
```

### date difference
Calculates the difference between two dates

Arguments:
- start: Start date ["now" to use the current system date, "utc" to use the current UTC date]
- end: End date ["now" to use the current system date, "utc" to use the current UTC date]

Example:
```
~$ vitorio date difference "2021-01-01 00:00:00" "2021-01-01 01:00:00"
-> 01:00:00

~$ vitorio date difference now "2021-01-01 01:00:00"
-> *current date - 01/01/2021 01:00:00*

~$ vitorio date difference utc "2021-01-01 01:00:00"
-> *current UTC date - 01/01/2021 01:00:00*

~$ vitorio date difference now utc
-> *current date - current UTC date*
```

# About the CLI
All data generated and manipulated by it are the sole responsibility of the user.

All help and suggestions are welcome.
