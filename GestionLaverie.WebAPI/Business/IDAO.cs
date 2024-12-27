namespace gestionLaverie.Domain
{
    public interface IDAO
    {
        User GetUser();
        List<Laverie> GetLaveriesByUserId(int userId);
        List<Machine> GetMachinesByLaverieId(int laverieId);
        List<Cycle> GetCyclesByMachineId(int machineId);
    }
}
