# Sistema de Gestão de Vendas
  
## Descrição do Projeto
  - Este é um projeto de um sistema básico para gestão de vendas, desenvolvido em C# utilizando um banco de dados SQLite. 
  - O sistema permite o gerenciamento de clientes, produtos e realização de vendas. Inclui a possibilidade de cadastrar, listar, atualizar e remover cadastros. Além disso, é possível realizar vendas com ou sem associação a um cliente.

## Funcionalidades
  - Gerenciamento de Clientes: Adicionar, listar, atualizar e remover clientes.
  - Gerenciamento de Produtos: Adicionar, listar, atualizar e remover produtos.
  - Realização de Vendas: Permite realizar vendas de produtos, informando ou não um cliente.

## Arquitetura
  - A projeto segue uma arquitetura simples com camadas de repositórios para cada entidade (Cliente, Produto, Venda). Facilitando a manutenção e o teste de cada parte do sistema.
  - O acesso ao banco de dados é centralizado através da classe DatabaseManager, que gerencia a conexão com o SQLite.
  - As operações de CRUD (Create, Read, Update, Delete) foram implementadas para todas as entidades (Clientes e Produtos).

## Organização dos Arquivos
  - Cliente.cs: Classe que representa um cliente.
  - Produto.cs: Classe que representa um produto.
  - Venda.cs: Classe que representa uma venda e seus itens.
  - ClienteRepository.cs: Responsável por todas as operações de banco de dados relacionadas a clientes.
  - ProdutoRepository.cs: Responsável pelas operações de banco de dados relacionadas a produtos.
  - VendaRepository.cs: Gerencia as vendas e os itens relacionados.

## Funcionalidades do Menu
  Ao iniciar o sistema, o menu principal será exibido com as seguintes opções:
  1. Adicionar Produto: Permite o cadastro de um novo produto no sistema.
  2. Listar Produtos: Exibe todos os produtos cadastrados.
  3. Remover Produto: Remove um produto específico pelo código.
  4. Atualizar Produto: Atualiza os dados do cadastro de um produto.
  5. Adicionar Cliente: Cadastra ou atualiza um cliente no sistema.
  6. Listar Clientes: Exibe todos os clientes cadastrados.
  7. Remover Cliente: Remove um cliente pelo documento.
  8. Atualizar Cliente: Atualiza os dados do cadastro de um cliente.
  9. Realizar Venda: Registra uma venda, é possível realizar informando um cliente ou continuar apenas com os produtos.
  10. Sair: Fecha o programa.

## Exemplo de Uso
  ### Cadastro de um Cliente
  - No menu, selecione a opção 1 para "Adicionar Cliente".
  - Insira o documento e o nome do cliente.
  - O cliente será cadastrado e poderá ser usado ao efetuar uma venda.

  ### Cadastro de um Produto
  - No menu, escolha "Adicionar Produto".
  - Insira o nome e o preço do produto.
  - O produto será adicionado à lista e estará disponível para vendas.

  ### Realização de Venda
  - Escolha a opção 9 "Realizar Venda" no menu.
  - Insira o documento do cliente ou deixe em branco para venda sem cliente.
  - Adicione os produtos à venda pelo código e quantidade.
  - Finalize a venda e o registro será salvo no banco de dados.
