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
    Console.WriteLine("8. Buscar dívida por Id");
    Console.WriteLine("9. Atualizar dívida");
    Console.WriteLine("10. Remover dívida");
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

        Console.WriteLine("CPF do cliente:");
        divida.CpfCliente = Console.ReadLine();

        var cliente = clienteService.BuscarPorCpf(divida.CpfCliente);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
        }
        else
        {
            var sucesso = dividaService.Criar(divida);

            if (sucesso)
            {
                Console.WriteLine("Dívida cadastrada com sucesso.");
            }
            else
            {
                Console.WriteLine("O cliente já possui uma dívida em aberto.");
            }
        }

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
                Console.WriteLine($"Cliente Id: {divida.CpfCliente}");
            }
        }

        Console.ReadKey();
    }
    else if (opcao == "8")
    {
        Console.WriteLine("Digite o Id da dívida:");

        var id = int.Parse(Console.ReadLine());

        var divida = dividaService.BuscarPorId(id);

        if (divida == null)
        {
            Console.WriteLine("Dívida não encontrada.");
        }
        else
        {
            Console.WriteLine($"Id: {divida.Id}");
            Console.WriteLine($"Valor: {divida.Valor:C}");
            Console.WriteLine($"Situação: {divida.Situacao}");
            Console.WriteLine($"CPF Cliente: {divida.CpfCliente}");
        }

        Console.ReadKey();
    }
    else if (opcao == "9")
    {
        Console.WriteLine("Digite o Id da dívida:");

        var id = int.Parse(Console.ReadLine());

        var dividaExistente = dividaService.BuscarPorId(id);

        if (dividaExistente == null)
        {
            Console.WriteLine("Dívida não encontrada.");
        }
        else
        {
            var divida = new Divida();

            divida.Id = id;

            Console.WriteLine("Novo valor:");
            divida.Valor = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Situação da dívida:");
            Console.WriteLine("0 - Aberta");
            Console.WriteLine("1 - Paga");

            divida.Situacao =
                (SituacaoDivida)int.Parse(Console.ReadLine());

            divida.CpfCliente = dividaExistente.CpfCliente;

            divida.DataCriacao = dividaExistente.DataCriacao;

            if (divida.Situacao == SituacaoDivida.Paga)
            {
                divida.DataPagamento = DateTime.Now;
            }

            var sucesso = dividaService.Atualizar(divida);

            if (sucesso)
            {
                Console.WriteLine("Dívida atualizada com sucesso.");
            }
            else
            {
                Console.WriteLine("Erro ao atualizar dívida.");
            }
        }

        Console.ReadKey();
    }
    else if (opcao == "10")
    {
        Console.WriteLine("Digite o Id da dívida:");

        var id = int.Parse(Console.ReadLine());

        var sucesso = dividaService.Remover(id);

        if (sucesso)
        {
            Console.WriteLine("Dívida removida com sucesso.");
        }
        else
        {
            Console.WriteLine("Dívida não encontrada.");
        }

        Console.ReadKey();
    }
}
