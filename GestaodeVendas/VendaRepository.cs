public class VendaRepository
{
    public void AdicionarVenda(Venda venda)
    {
        using var connection = DatabaseManager.GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Vendas (Data, ClienteDocumento) VALUES (@Data, @ClienteDocumento)";
        command.Parameters.AddWithValue("@Data", venda.Data);
        command.Parameters.AddWithValue("@ClienteDocumento", venda.Cliente?.Documento);
        command.ExecuteNonQuery();

        command.CommandText = "SELECT last_insert_rowid()";
        venda.Id = (int)(long)command.ExecuteScalar();

        foreach (var item in venda.Itens)
        {
            var itemCommand = connection.CreateCommand();
            itemCommand.CommandText = "INSERT INTO ItensVenda (VendaId, ProdutoCodigo, Quantidade) VALUES (@VendaId, @ProdutoCodigo, @Quantidade)";
            itemCommand.Parameters.AddWithValue("@VendaId", venda.Id);
            itemCommand.Parameters.AddWithValue("@ProdutoCodigo", item.Produto.Codigo);
            itemCommand.Parameters.AddWithValue("@Quantidade", item.Quantidade);
            itemCommand.ExecuteNonQuery();
        }
    }
}