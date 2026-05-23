using Vendinha.Enums;
using Vendinha.Models;
using Vendinha.Services;

var dividaService = new DividaSerivce();
var clienteService = new ClienteService();

while (true)
{
    Console.Clear();

    Console.WriteLine("=== VENDINHA ===");
    Console.WriteLine("1. Criar cliente");
    Console.WriteLine("2. Listar clientes");
    Console.WriteLine("3. Buscar por CPF");
    Console.WriteLine("4. Atualizar cliente");
    Console.WriteLine("5. Remover cliente");
    Console.WriteLine("6. Criar dívida");
    Console.WriteLine("7. Listar dívidas");
    Console.WriteLine("0. Sair");

    var opcao = Console.ReadLine();

    if (opcao == "0")
    {
        break;
    }

    else if (opcao == "1")
    {
        var cliente = new Cliente();

        Console.WriteLine("Digite o nome do cliente:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Digite o cpf do cliente:");
        cliente.Cpf = Console.ReadLine();

        Console.WriteLine("Digite a data de nascimento do cliente (dd/MM/yyyy):");
        cliente.DataNascimento = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Digite o email do cliente:");
        cliente.Email = Console.ReadLine();

        var sucesso = clienteService.Criar(cliente);

        if (sucesso)
        {
            Console.WriteLine("Cliente criado com sucesso!");
        }
        else
        {
            Console.WriteLine("Já existe um cliente com esse cpf.");
        }
        Console.ReadKey();
    }

    else if (opcao == "2")
    {
        var clientes = clienteService.Listar();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine("====================");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"Email: {cliente.Email}");
            }
        }

        Console.ReadKey();
    }
    else if (opcao == "3")
    {
        Console.Write("Digite o cpf do cliente:");

        var cpf = Console.ReadLine();

        var cliente = clienteService.BuscarPorCpf(cpf);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
        }
        else
        {
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"Cpf: {cliente.Cpf}");
            Console.WriteLine($"Email: {cliente.Email}");
            Console.WriteLine($"Idade: {cliente.Idade}");
        }

        Console.ReadKey();
    }
    else if (opcao == "4")
    {
        Console.WriteLine("Digite o CPF do cliente:");

        var cpf = Console.ReadLine();

        var clienteExistente = clienteService.BuscarPorCpf(cpf);

        if (clienteExistente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
        }
        else
        {
            var cliente = new Cliente();

            cliente.Cpf = cpf;

            Console.WriteLine("Novo nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Nova data de nascimento (dd/MM/yyyy):");
            cliente.DataNascimento = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Novo email:");
            cliente.Email = Console.ReadLine();

            var sucesso = clienteService.Atualizar(cliente);

            if (sucesso)
            {
                Console.WriteLine("Cliente alterado com sucesso.");
            }
            else
            {
                Console.WriteLine("Erro ao alterar cliente.");
            }
        }

        Console.ReadKey();
    }
    
    else if (opcao == "5")
    {
        Console.WriteLine("Digite o CPF do cliente:");

        var cpf = Console.ReadLine();

        var sucesso = clienteService.Remover(cpf);

        if (sucesso)
        {
            Console.WriteLine("Cliente removido com sucesso.");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }

        Console.ReadKey();
    }

    else if (opcao == "6")
    {
        var divida = new Divida();

        Console.WriteLine("Id da dívida:");
        divida.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Valor:");
        divida.Valor = decimal.Parse(Console.ReadLine());

        divida.DataCriacao = DateTime.Now;
        divida.Situacao = SituacaoDivida.Aberta;

        Console.WriteLine("Id do cliente:");
        divida.ClienteId = int.Parse(Console.ReadLine());

        dividaService.Criar(divida);

        Console.WriteLine("Dívida cadastrada com sucesso.");
        Console.ReadKey();
    }

    else if (opcao == "7")
    {
        var dividas = dividaService.Listar();

        if (dividas.Count == 0)
        {
            Console.WriteLine("Nenhuma dívida cadastrada.");
        }
        else
        {
            foreach (var divida in dividas)
            {
                Console.WriteLine("====================");
                Console.WriteLine($"Id: {divida.Id}");
                Console.WriteLine($"Valor: {divida.Valor:C}");
                Console.WriteLine($"Situação: {divida.Situacao}");
                Console.WriteLine($"Cliente Id: {divida.ClienteId}");
            }
        }

        Console.ReadKey();
    }
}
