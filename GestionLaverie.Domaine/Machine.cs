namespace gestionLaverie.Domain
{
    public class Machine
    {
        public int IdMachine { get; set; } 
        public int IdLaverie { get; set; } 
        public string EtatMachine { get; set; } 
        public List<Cycle> Cycles { get; set; } 
    }

}