using System;
using System.Collections.Generic;

public class Venda
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public Cliente Cliente { get; set; }
    public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
}

public class ItemVenda
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
}