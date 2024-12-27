using MySql.Data.MySqlClient;
using gestionLaverie.Domain;

namespace gestionLaverie.WebAPI.Infrastructure
{
    public class RepoImpl : IDAO
    {
        private readonly string _connectionString;

        public RepoImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQLConnection");
        }

        public User GetUser()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT IdUser, Username, Password FROM Users WHERE IdUser = 1", connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new User
                        {
                            IdUser = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Laveries = GetLaveriesByUserId(reader.GetInt32(0))
                        };

                        foreach (var laverie in user.Laveries)
                        {
                            laverie.Machines = GetMachinesByLaverieId(laverie.IdLaverie);

                            foreach (var machine in laverie.Machines)
                            {
                                machine.Cycles = GetCyclesByMachineId(machine.IdMachine);
                            }
                        }

                        return user;
                    }
                }
            }

            return null;
        }

        public List<Laverie> GetLaveriesByUserId(int userId)
        {
            var laveries = new List<Laverie>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT IdLaverie, Localisation FROM Laveries WHERE IdUser = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        laveries.Add(new Laverie
                        {
                            IdLaverie = reader.GetInt32(0),
                            Localisation = reader.GetString(1)
                        });
                    }
                }
            }

            return laveries;
        }

        public List<Machine> GetMachinesByLaverieId(int laverieId)
        {
            var machines = new List<Machine>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT IdMachine, EtatMachine FROM Machines WHERE IdLaverie = @LaverieId", connection);
                command.Parameters.AddWithValue("@LaverieId", laverieId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        machines.Add(new Machine
                        {
                            IdMachine = reader.GetInt32(0),
                            EtatMachine = reader.GetString(1)
                        });
                    }
                }
            }

            return machines;
        }

        public List<Cycle> GetCyclesByMachineId(int machineId)
        {
            var cycles = new List<Cycle>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT Id, Duration, Cost FROM Cycles WHERE IdMachine = @MachineId", connection);
                command.Parameters.AddWithValue("@MachineId", machineId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cycles.Add(new Cycle
                        {
                            Id = reader.GetInt32(0),
                            Duration = reader.GetInt32(1),
                            Cost = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return cycles;
        }
    }
}
