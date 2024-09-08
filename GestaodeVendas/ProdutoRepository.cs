public class ProdutoRepository
{
    public void AdicionarProduto(Produto produto)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Produtos (Nome, Preco) VALUES (@Nome, @Preco)";
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Preco", produto.Preco);
        command.ExecuteNonQuery();
    }

    public Produto ObterProduto(int codigo)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Codigo, Nome, Preco FROM Produtos WHERE Codigo = @Codigo";
        command.Parameters.AddWithValue("@Codigo", codigo);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Produto
            {
                Codigo = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Preco = reader.GetDecimal(2)
            };
        }
        return null;
    }

    public List<Produto> ObterProdutos()
    {
        var produtos = new List<Produto>();
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Codigo, Nome, Preco FROM Produtos";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            produtos.Add(new Produto
            {
                Codigo = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Preco = reader.GetDecimal(2)
            });
        }
        return produtos;
    }

    public void RemoverProduto(int codigo)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Produtos WHERE Codigo = @Codigo";
        command.Parameters.AddWithValue("@Codigo", codigo);
        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            throw new Exception($"Produto com código {codigo} não encontrado.");
        }
    }

    public void AtualizarProduto(Produto produto)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco WHERE Codigo = @Codigo";
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Preco", produto.Preco);
        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            throw new Exception($"Produto com código {produto.Codigo} não encontrado.");
        }
        else
        {
            Console.WriteLine("Produto atualizado com sucesso!");
        }
    }
}