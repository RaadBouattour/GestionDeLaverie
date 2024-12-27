namespace gestionLaverie.Domain
{
    public class User
    {
        public int IdUser { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; } 

        public List<Laverie> Laveries { get; set; } 
    }

}
