public class ClienteRepository
{
    public void AdicionarOuAtualizarCliente(Cliente cliente)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = "SELECT COUNT(*) FROM Clientes WHERE Documento = @Documento";
        checkCommand.Parameters.AddWithValue("@Documento", cliente.Documento);
        var count = (long)checkCommand.ExecuteScalar();

        if (count > 0)
        {
            var updateCommand = connection.CreateCommand();
            updateCommand.CommandText = "UPDATE Clientes SET Nome = @Nome WHERE Documento = @Documento";
            updateCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
            updateCommand.Parameters.AddWithValue("@Documento", cliente.Documento);
            updateCommand.ExecuteNonQuery();
        }
        else
        {
            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = "INSERT INTO Clientes (Documento, Nome) VALUES (@Documento, @Nome)";
            insertCommand.Parameters.AddWithValue("@Documento", cliente.Documento);
            insertCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
            insertCommand.ExecuteNonQuery();
        }
    }

    public Cliente ObterCliente(string documento)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Documento, Nome FROM Clientes WHERE Documento = @Documento";
        command.Parameters.AddWithValue("@Documento", documento);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Cliente
            {
                Documento = reader.GetString(0),
                Nome = reader.GetString(1)
            };
        }
        return null;
    }

    public List<Cliente> ObterClientes()
    {
        var clientes = new List<Cliente>();
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Documento, Nome FROM Clientes";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            clientes.Add(new Cliente
            {
                Documento = reader.GetString(0),
                Nome = reader.GetString(1)
            });
        }
        return clientes;
    }

    public void RemoverCliente(string documento)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Clientes WHERE Documento = @Documento";
        command.Parameters.AddWithValue("@Documento", documento);
        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            throw new Exception($"Cliente com documento {documento} não encontrado.");
        }
    }

    // Método de atualização de cliente
    public void AtualizarCliente(Cliente cliente)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Clientes SET Nome = @Nome WHERE Documento = @Documento";
        command.Parameters.AddWithValue("@Nome", cliente.Nome);
        command.Parameters.AddWithValue("@Documento", cliente.Documento);
        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            throw new Exception($"Cliente com documento {cliente.Documento} não encontrado.");
        }
        else
        {
            Console.WriteLine("Cliente atualizado com sucesso!");
        }
    }
}