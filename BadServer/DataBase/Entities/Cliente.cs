namespace BadServer.DataBase.Entities
{
    public class Cliente
    {

        public int ClienteID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Rol { get; set; }

        public Cesta Cesta { get; set; }

    }

}
