# Vendinha

## Descrição

Sistema desenvolvido em C# utilizando .NET e Entity Framework para gerenciamento de clientes e dívidas de uma vendinha.

O sistema permite cadastrar clientes, registrar dívidas, consultar informações e controlar os valores devidos pelos clientes.

---

## Tecnologias Utilizadas

* C#
* .NET 10
* Entity Framework Core
* SQLite

---

## Funcionalidades

### Clientes

* Cadastrar cliente
* Listar clientes
* Buscar cliente por CPF
* Buscar cliente por nome
* Atualizar cliente
* Remover cliente

### Dívidas

* Cadastrar dívida
* Listar dívidas
* Buscar dívida por ID
* Atualizar dívida
* Remover dívida

---

## Regras Implementadas

* CPF único por cliente
* Nome obrigatório
* CPF obrigatório
* Data de nascimento obrigatória
* E-mail validado
* Idade calculada automaticamente
* Soma total das dívidas por cliente
* Ordenação dos clientes do maior devedor para o menor devedor

---

## Banco de Dados

O projeto utiliza SQLite para persistência dos dados.

Arquivo do banco:

vendinha.db

---

## Como Executar

1. Abrir a solução no Visual Studio.
2. Restaurar os pacotes NuGet.
3. Executar a migration:

```powershell
Update-Database
```

4. Executar o projeto.

---

## Autor

Diego Fabretti Koyama Leite