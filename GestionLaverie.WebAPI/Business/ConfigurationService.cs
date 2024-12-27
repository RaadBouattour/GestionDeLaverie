using gestionLaverie.Domain;

namespace gestionLaverie.WebAPI.Business
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IDAO _repo;

        public ConfigurationService(IDAO repo)
        {
            _repo = repo;
        }

        public object getConfig()
        {
            var user = _repo.GetUser();
            return new
            {
                user.Username,
                Laveries = user.Laveries.Select(l => new
                {
                    l.IdLaverie,
                    l.Localisation,
                    Machines = l.Machines.Select(m => new
                    {
                        m.IdMachine,
                        m.EtatMachine,
                        Cycles = m.Cycles.Select(c => new
                        {
                            c.Id,
                            c.Duration,
                            c.Cost
                        })
                    })
                })
            };
        }
    }
}
