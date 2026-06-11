using Vendinha.Enums;
using Vendinha.Models;
using Vendinha.Services;
using System.Linq;

var dividaService = new DividaSerivce();
var clienteService = new ClienteService();

while (true)
{
    Console.Clear();

    Console.WriteLine("=== VENDINHA ===");
    Console.WriteLine("1. Cliente");
    Console.WriteLine("2. Dívida");
    Console.WriteLine("0. Sair");

    var menuPrincipal = Console.ReadLine();

    string opcao = "";

    if (menuPrincipal == "1")
    {
        Console.Clear();

        Console.WriteLine("=== CLIENTE ===");
        Console.WriteLine("1. Criar");
        Console.WriteLine("2. Listar");
        Console.WriteLine("3. Buscar");
        Console.WriteLine("4. Atualizar");
        Console.WriteLine("5. Remover");

        opcao = "C" + Console.ReadLine();
    }

    else if (menuPrincipal == "2")
    {
        Console.Clear();

        Console.WriteLine("=== DÍVIDA ===");
        Console.WriteLine("1. Criar");
        Console.WriteLine("2. Listar");
        Console.WriteLine("3. Buscar");
        Console.WriteLine("4. Atualizar");
        Console.WriteLine("5. Remover");

        opcao = "D" + Console.ReadLine();
    }

    else if (menuPrincipal == "0")
    {
        break;
    }
    else
    {
        continue;
    }

    if (opcao == "0")
    {
        break;
    }

    else if (opcao == "C1")
    {
        var cliente = new Cliente();

        Console.WriteLine("Digite o nome do cliente:");
        cliente.Nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(cliente.Nome))
        {
            Console.WriteLine("Nome obrigatório.");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine("Digite o cpf do cliente:");
        cliente.Cpf = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(cliente.Cpf))
        {
            Console.WriteLine("CPF obrigatório.");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine("Digite a data de nascimento do cliente (dd/MM/yyyy):");
        cliente.DataNascimento = DateTime.Parse(Console.ReadLine());

        Console.WriteLine($"Idade calculada: {cliente.Idade}");

        if (cliente.DataNascimento > DateTime.Now)
        {
            Console.WriteLine("Data de nascimento inválida.");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine("Digite o email do cliente:");
        cliente.Email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(cliente.Email))
        {
            Console.WriteLine("Email obrigatório.");
            Console.ReadKey();
            continue;
        }

        var sucesso = clienteService.Criar(cliente, out var listaErros);

        if (sucesso)
        {
            Console.WriteLine("Cliente criado com sucesso!");
        }
        else
        {
            foreach (var erro in listaErros)
            {
                Console.WriteLine(erro.ErrorMessage);
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "C2")
    {
        var clientes = clienteService.Listar();

        clientes = clientes.OrderByDescending(cliente =>dividaService.TotalDividas(cliente.Cpf)).ToList();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            foreach (var cliente in clientes)
            {
                decimal totalDividas = dividaService.TotalDividas(cliente.Cpf);

                Console.WriteLine("====================");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"Email: {cliente.Email}");
                Console.WriteLine($"Idade: {cliente.Idade}");
                Console.WriteLine($"Total de Dívidas: R$ {totalDividas}");
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "C3")
    {
        Console.WriteLine("1 - Buscar por CPF");
        Console.WriteLine("2 - Buscar por Nome");

        var tipoBusca = Console.ReadLine();

        if (tipoBusca == "1")
        {
            Console.Write("Digite o CPF do cliente: ");

            var cpf = Console.ReadLine();

            var cliente = clienteService.BuscarPorCpf(cpf);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            else
            {
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"Email: {cliente.Email}");
                Console.WriteLine($"Idade: {cliente.Idade}");
            }
        }
        else if (tipoBusca == "2")
        {
            Console.Write("Digite o nome do cliente: ");

            var nome = Console.ReadLine();

            var clientes = clienteService.BuscarPorNome(nome);

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine("====================");
                    Console.WriteLine($"Nome: {cliente.Nome}");
                    Console.WriteLine($"CPF: {cliente.Cpf}");
                    Console.WriteLine($"Email: {cliente.Email}");
                    Console.WriteLine($"Idade: {cliente.Idade}");
                }
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }

        Console.ReadKey();
    }

    else if (opcao == "C4")
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

            var sucesso = clienteService.Atualizar(cliente, out var listaErros);

            if (sucesso)
            {
                Console.WriteLine("Cliente alterado com sucesso.");
            }
            else
            {
                foreach (var erro in listaErros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }

            Console.ReadKey();
        }
    }

    else if (opcao == "C5")
    {
        Console.WriteLine("Digite o CPF do cliente:");

        var cpf = Console.ReadLine();

        var sucesso = clienteService.Remover(cpf,out var listaErros);

        if (sucesso)
        {
            Console.WriteLine("Cliente removido com sucesso.");
        }
        else
        {
            foreach (var erro in listaErros)
            {
                Console.WriteLine(erro.ErrorMessage);
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "D1")
    {
        var divida = new Divida();

        Console.WriteLine("Valor:");
        divida.Valor = decimal.Parse(Console.ReadLine());

        if (divida.Valor <= 0)
        {
            Console.WriteLine("O valor da dívida deve ser maior que zero.");
            Console.ReadKey();
            continue;
        }

        divida.DataCriacao = DateTime.Now;
        divida.Situacao = SituacaoDivida.Aberta;

        Console.WriteLine("CPF do cliente:");
        divida.CpfCliente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(divida.CpfCliente))
        {
            Console.WriteLine("CPF obrigatório.");
            Console.ReadKey();
            continue;
        }

        var cliente = clienteService.BuscarPorCpf(divida.CpfCliente);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
        }
        else
        {
            var sucesso = dividaService.Criar(divida,out var listaErros);

            if (sucesso)
            {
                Console.WriteLine("Dívida cadastrada com sucesso.");
            }
            else
            {
                foreach (var erro in listaErros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "D2")
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
                var cliente = clienteService
                    .BuscarPorCpf(divida.CpfCliente);

                Console.WriteLine("====================");
                Console.WriteLine($"Id: {divida.Id}");

                if (cliente != null)
                {
                    Console.WriteLine($"Cliente: {cliente.Nome}");
                }

                Console.WriteLine($"Valor: {divida.Valor:C}");
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "D3")
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
    else if (opcao == "D4")
    {
        var dividas = dividaService.Listar();

        if (dividas.Count == 0)
        {
            Console.WriteLine("Nenhuma dívida cadastrada.");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine("DÍVIDAS CADASTRADAS");
        Console.WriteLine("====================");

        foreach (var dividaItem in dividas)
        {
            var cliente = clienteService
                .BuscarPorCpf(dividaItem.CpfCliente);

            Console.WriteLine($"Id: {dividaItem.Id}");

            if (cliente != null)
            {
                Console.WriteLine(
                    $"Cliente: {cliente.Nome}");
            }

            Console.WriteLine(
                $"Valor: {dividaItem.Valor:C}");

            Console.WriteLine(
                $"Situação: {dividaItem.Situacao}");

            Console.WriteLine("--------------------");
        }

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

            divida.CpfCliente = dividaExistente.CpfCliente;
            divida.DataCriacao = dividaExistente.DataCriacao;

            divida.Situacao = dividaExistente.Situacao;
            divida.DataPagamento = dividaExistente.DataPagamento;

            var sucesso = dividaService.Atualizar(divida,out var listaErros);

            if (sucesso)
            {
                Console.WriteLine("Dívida atualizada com sucesso.");
            }
            else
            {
                foreach (var erro in listaErros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }
        }

        Console.ReadKey();
    }

    else if (opcao == "D5")
    {
        var dividas = dividaService.Listar();

        if (dividas.Count == 0)
        {
            Console.WriteLine("Nenhuma dívida cadastrada.");
        }
        else
        {
            Console.WriteLine("DÍVIDAS CADASTRADAS");
            Console.WriteLine("====================");

            foreach (var divida in dividas)
            {
                var cliente = clienteService.BuscarPorCpf(divida.CpfCliente);

                Console.WriteLine($"Id: {divida.Id}");

                if (cliente != null)
                {
                    Console.WriteLine(
                        $"Cliente: {cliente.Nome}");
                }

                Console.WriteLine(
                    $"Valor: {divida.Valor:C}");

                Console.WriteLine(
                    "--------------------");
            }

            Console.WriteLine(
                "Digite o Id da dívida que deseja remover:");

            var id = int.Parse(Console.ReadLine());

            var sucesso =
                dividaService.Remover(id, out var listaErros);

            if (sucesso)
            {
                Console.WriteLine("Dívida removida com sucesso.");
            }
            else
            {
                foreach (var erro in listaErros)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }
        }

        Console.ReadKey();
    }
}
