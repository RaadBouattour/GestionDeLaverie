namespace gestionLaverie.Domain
{
    public class Laverie
    {
        public int IdLaverie { get; set; } 
        public string Localisation { get; set; } 
        public int IdUser { get; set; } 
        public List<Machine> Machines { get; set; }
    }

}