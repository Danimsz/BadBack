﻿using System.Data.SqlTypes;

namespace BadServer.DataBase.Entities
{
    public class Pedido
    {


        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public string MetodoPago { get; set; }
        public SqlMoney Total { get; set; }
        public int Estado { get; set; } //Guardar el estado en 0 1 2
        public SqlMoney PrecioEuro { get; set; }
        public SqlMoney PrecioEtherium { get; set; }
        //hash de la transacion que necesitas saber si se a completado o no la tranccion del cliente
        public string HashTransaccion { get; set; }
        public string WalletCliente { get; set; }


    }
}
