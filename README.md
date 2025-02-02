# ValidatorCPF

O projeto `validatorcpf` é uma Azure Function que valida números de CPF (Cadastro de Pessoas Físicas) enviados via requisições HTTP POST. A função principal é definida na classe estática `fnvalidacpf` dentro do namespace `validatorcpf`.

## Estrutura do Projeto

- `validatorcpf.csproj`: Arquivo de configuração do projeto.
- `fnvalidacpf.cs`: Contém a função Azure que valida o CPF.
- `host.json` e `local.settings.json`: Arquivos de configuração para o host e definições locais.
- `bin/` e `obj/`: Diretórios de build e output.

## Código Principal

- `Run`: Método que é acionado por uma requisição HTTP POST. Ele lê o corpo da requisição, deserializa o JSON para obter o CPF, e chama o método `ValidaCPF` para verificar a validade do CPF.
- `ValidaCPF`: Método que contém a lógica para validar o número do CPF.