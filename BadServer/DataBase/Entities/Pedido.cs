﻿using System.Data.SqlTypes;

namespace BadServer.DataBase.Entities
{
    public class Pedido
    {


        public int Id { get; set; }
        public int ClienteID { get; set; }
        public string MetodoPago { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public decimal PrecioEuro { get; set; }
        public double PrecioEtherium { get; set; }
        public string? HashTransaccion { get; set; }
        public string? WalletCliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public string DireccionCliente { get; set; }
        public ICollection<PedidoProducto> PedidoProductos { get; set; }
        public Cliente Cliente { get; set; }
    }
}
