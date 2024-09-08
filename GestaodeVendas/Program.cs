using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var produtoRepo = new ProdutoRepository();
        var clienteRepo = new ClienteRepository();
        var vendaRepo = new VendaRepository();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Remover Produto");
            Console.WriteLine("4. Atualizar Produto");
            Console.WriteLine("5. Adicionar Cliente");
            Console.WriteLine("6. Listar Clientes");
            Console.WriteLine("7. Remover Cliente");
            Console.WriteLine("8. Atualizar Cliente");
            Console.WriteLine("9. Realizar Venda");
            Console.WriteLine("10. Sair");
            Console.Write("Escolha uma opção: ");

            var escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    AdicionarProduto(produtoRepo);
                    break;
                case "2":
                    ListarProdutos(produtoRepo);
                    break;
                case "3":
                    RemoverProduto(produtoRepo);
                    break;
                case "4":
                    AtualizarProduto(produtoRepo);
                    break;
                case "5":
                    AdicionarCliente(clienteRepo);
                    break;
                case "6":
                    ListarClientes(clienteRepo);
                    break;
                case "7":
                    RemoverCliente(clienteRepo);
                    break;
                case "8":
                    AtualizarCliente(clienteRepo);
                    break;
                case "9":
                    RealizarVenda(produtoRepo, clienteRepo, vendaRepo);
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AdicionarProduto(ProdutoRepository produtoRepo)
    {
        Console.Write("Digite o nome do produto: ");
        var nome = Console.ReadLine();
        Console.Write("Digite o preço do produto: ");
        if (decimal.TryParse(Console.ReadLine(), out var preco))
        {
            var produto = new Produto { Nome = nome, Preco = preco };
            produtoRepo.AdicionarProduto(produto);
            Console.WriteLine("Produto adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Preço inválido.");
        }
        Console.ReadKey();
    }

    static void ListarProdutos(ProdutoRepository produtoRepo)
    {
        var produtos = produtoRepo.ObterProdutos();
        Console.WriteLine("Produtos:");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Preço: {produto.Preco}");
        }
        Console.ReadKey();
    }

    static void RemoverProduto(ProdutoRepository produtoRepo)
    {
        Console.Write("Digite o código do produto a ser removido: ");
        if (int.TryParse(Console.ReadLine(), out var codigo))
        {
            produtoRepo.RemoverProduto(codigo);
            Console.WriteLine("Produto removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Código inválido.");
        }
        Console.ReadKey();
    }

    static void AtualizarProduto(ProdutoRepository produtoRepo)
    {
        Console.Write("Digite o código do produto a ser atualizado: ");
        if (int.TryParse(Console.ReadLine(), out var codigo))
        {
            var produto = produtoRepo.ObterProduto(codigo);
            if (produto != null)
            {
                Console.Write("Digite o novo nome do produto (ou deixe em branco para manter o atual): ");
                var novoNome = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoNome))
                {
                    produto.Nome = novoNome;
                }

                Console.Write("Digite o novo preço do produto (ou deixe em branco para manter o atual): ");
                if (decimal.TryParse(Console.ReadLine(), out var novoPreco))
                {
                    produto.Preco = novoPreco;
                }

                produtoRepo.AtualizarProduto(produto);
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Código inválido.");
        }
        Console.ReadKey();
    }

    static void AdicionarCliente(ClienteRepository clienteRepo)
    {
        Console.Write("Digite o documento do cliente: ");
        var documento = Console.ReadLine();
        Console.Write("Digite o nome do cliente: ");
        var nome = Console.ReadLine();

        var cliente = new Cliente { Documento = documento, Nome = nome };
        clienteRepo.AdicionarOuAtualizarCliente(cliente);
        Console.WriteLine("Cliente adicionado com sucesso!");
        Console.ReadKey();
    }

    static void ListarClientes(ClienteRepository clienteRepo)
    {
        var clientes = clienteRepo.ObterClientes();
        Console.WriteLine("Clientes:");
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Documento: {cliente.Documento}, Nome: {cliente.Nome}");
        }
        Console.ReadKey();
    }

    static void RemoverCliente(ClienteRepository clienteRepo)
    {
        Console.Write("Digite o documento do cliente a ser removido: ");
        var documento = Console.ReadLine();
        clienteRepo.RemoverCliente(documento);
        Console.WriteLine("Cliente removido com sucesso!");
        Console.ReadKey();
    }

    static void AtualizarCliente(ClienteRepository clienteRepo)
    {
        Console.Write("Digite o documento do cliente a ser atualizado: ");
        var documento = Console.ReadLine();
        var cliente = clienteRepo.ObterCliente(documento);

        if (cliente != null)
        {
            Console.Write("Digite o novo nome do cliente (ou deixe em branco para manter o atual): ");
            var novoNome = Console.ReadLine();
            if (!string.IsNullOrEmpty(novoNome))
            {
                cliente.Nome = novoNome;
            }

            clienteRepo.AtualizarCliente(cliente);
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
        Console.ReadKey();
    }

    static void RealizarVenda(ProdutoRepository produtoRepo, ClienteRepository clienteRepo, VendaRepository vendaRepo)
    {
        Console.Write("Digite o documento do cliente: ");
        var documentoCliente = Console.ReadLine();
        var cliente = clienteRepo.ObterCliente(documentoCliente);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
            Console.ReadKey();
            return;
        }

        var venda = new Venda
        {
            Cliente = cliente,
            Data = DateTime.Now
        };

        while (true)
        {
            Console.Write("Digite o código do produto (ou 0 para finalizar): ");
            if (int.TryParse(Console.ReadLine(), out var codigoProduto) && codigoProduto != 0)
            {
                var produto = produtoRepo.ObterProduto(codigoProduto);
                if (produto != null)
                {
                    Console.Write("Digite a quantidade: ");
                    if (int.TryParse(Console.ReadLine(), out var quantidade))
                    {
                        venda.Itens.Add(new ItemVenda { Produto = produto, Quantidade = quantidade });
                    }
                    else
                    {
                        Console.WriteLine("Quantidade inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Produto não encontrado.");
                }
            }
            else
            {
                break;
            }
        }

        vendaRepo.AdicionarVenda(venda);
        Console.WriteLine("Venda realizada com sucesso!");
        Console.ReadKey();
    }
}