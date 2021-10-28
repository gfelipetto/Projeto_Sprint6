# Microsserviço
Projeto final desenvolvido no programa de bolsas Compasso UOL
- Api SisClientes
  - Responsável por gerenciar o cadastro de clientes.
  - Cadastro de cidades via cep.
  - Cadastro de múltiplos endereços
  
- Api SisProdutos
  - Responsável por gerenciar o cadastro de produtos.
  - Sistema de login por usuário/senha, gerando token.
  - Sistema de procura de produtos por nome, palavra-chave, descrição, categorias e ordenação por preço.
  - Sistema de compras, adicionando produtos ao carrinho e cobrando frete para endereços diferentes do usuário.
  
- Api Auditoria
  - Responsável por armazenar o log.
  - Registro de execeções lançadas pelo SisProdutos, por usuário
  - Registro de chamada de endpoint, por usuário.

## Tecnologias utilizadas
- .NET Framework 5.0
- Entity Framework Core
- SQL Server Local
- Identity Framework

## Como utilizar
### SisClientes
#### Clientes

```bash
# GET
- Retorna todos os resultados de clientes
```

```bash
# GET {id}
- Id: guid
- Retorna um cliente específico
```

```bash
# PUT {id}
- Id: guid
- Altera um cliente específico
- Requisitos: corpo e Cep válido

  Exemplo de esquema para corpo:
  {
    "nome": "string",
    "dataNascimento": "datetime",
    "cep": "string"
  }
```

```bash
# POST 
- Cadastra um novo cliente
- Requisitos: corpo e Cep válido 

  Exemplo de esquema para corpo:
  {
    "nome": "string",
    "dataNascimento": "datetime",
    "cep": "string"
  }
```

```bash
# POST {id}
- Id: guid
- Cadastra um novo cliente com um id específico
- Requisitos: corpo e Cep válido 

  Exemplo de esquema para corpo:
  {
    "nome": "string",
    "dataNascimento": "datetime",
    "cep": "string"
  }
```

```bash
# DELETE {id}
- Id: guid
- Remove um cliente específico
```

#### Cidades

```bash
# GET
- Retorna todos os resultados de cidades
```

```bash
# GET {id}
- Id: guid
- Retorna uma cidade específica
```

```bash
# PUT {id}
- Id: guid
- Altera uma cidade específica
- Requisitos: corpo

  Exemplo de esquema para corpo:
  {
    "nome": "string",
    "estado": "string"
  }
```

```bash
# POST {cep}
- cep: string
- Cadastra uma nova cidade através de um Cep válido
```

```bash
# DELETE {id}
- Id: guid
- Remove uma cidade específica
```

### Endereços

```bash
# GET
- Retorna todos os resultados de endereços
```

```bash
# GET {id}
- Id: guid
- Retorna um endereço específico
```

```bash
# PUT {id}
- Id: guid
- Altera um endereço específico
- Requisitos: corpo

  Exemplo de esquema para corpo:
  {
    "cep": "string",
    "logradouro": "string",
    "bairro": "string",
    "numeroCasa": "string",
    "complemento": "string",
    "ehPrincipal": true
  }
```

```bash
# POST {id}
- Id: guid
- Cadastra um endereço no cliente referente ao Id

  Exemplo de esquema para corpo:
  {
    "cep": "string",
    "logradouro": "string",
    "bairro": "string",
    "numeroCasa": "string",
    "complemento": "string",
    "ehPrincipal": true
  }
```

```bash
# DELETE {id}
- Id: guid
- Remove um endereço específico
```

### SisProdutos
#### Cadastrar 

```bash
# POST 
- Cadastra um novo usuário junto com um novo cliente

  Exemplo de esquema para corpo:
  {
    "userName": "string",
    "clientName": "string",
    "birthDate": "datetime",
    "cep": "string",
    "password": "string",
    "rePassword": "string"
  }
```

#### Logar

```bash

# POST 
- Realiza login de um usuário já cadastrado

  Exemplo de esquema para corpo:
  {
    "userName": "string",
    "password": "string"
  }
```

### Logout

```bash

# POST 
- Realiza o logout do atual usuário logado
```

### Produtos

```bash
# GET
- Retorna todos os resultados de produtos
```

```bash
# GET {id}
- Id: guid
- Retorna um produto específico
```

```bash
# POST 
- Cadastra um novo produto com suas especifícações

  Exemplo de esquema para corpo:
  {
    "nome": "string",
    "descricao": "string",
    "preco": 0,
    "cidade": [
    {
      "nome": "string",
      "estado": "string"
    }
    ],
      "palavrasChave": [
      {
        "nome": "string"
      }
    ],
      "categorias": [
      {
        "nome": "string"
    }
    ]
}
```

```bash
# DELETE {id}
- Id: guid
- Remove um produto específico
```

#### Compras

```bash
# GET
- Retorna todos os produtos no carrinho
```

```bash
# POST {id}
- Id: guid
- Adiciona um produto por Id no carrinho
```

```bash
# DELETE {id}
- Id: guid
- Remove um produto específico do carrinho
```

```bash
# GET - /finalizar
- Realiza a compra dos produtos no carrinho, aplicando o valor do frete
```

### Auditoria
#### Atividades

```bash
# GET
- Retorna todas as atividades registradas do usuário
```

```bash
# GET {id}
- Id: guid
- Retorna uma atividade específica registrada do usuário
```

```bash
# POST 
- Adiciona uma atividade

    Exemplo de esquema para corpo:
    {
      "usuarioId": "guid",
      "nome": "string",
      "descricao": "string",
      "preco": decimal
    }
```

#### Excecoes

```bash
# GET
- Retorna todas as excecoes registradas no SisProdutos
```

```bash
# GET {id}
- Id: guid
- Retorna uma excecao específica registrada no SisProdutos
```

```bash
# POST
- Adiciona uma excecao

    Exemplo de esquema para corpo:
    {
      "usuarioId": "guid",
      "excecao": "string"
    }
```
