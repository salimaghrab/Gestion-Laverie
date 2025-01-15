using LaverieApi.Models.IDAO;
using MySql.Data.MySqlClient;
using LaverieDomain;

namespace LaverieApi.Infrastructure.DAO
{
    public class MachineDAOImp : IMachineDAO
    {
        private readonly MySqlConnection _connection;

        public MachineDAOImp()
        {
            _connection = ConnectionDb.GetInstance().GetConnection();
        }

        public void AddMachine(Machine machine)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO machines (nom, modele, etat, laverie_id) VALUES (@nom, @modele, @etat, @laverie_id)";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@nom", machine.Nom);
                cmd.Parameters.AddWithValue("@modele", machine.Modele);
                cmd.Parameters.AddWithValue("@etat", machine.Etat);
                cmd.Parameters.AddWithValue("@laverie_id", machine.LaverieId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Machine> GetAllMachines()
        {
            List<Machine> machines = new List<Machine>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM machines";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    machines.Add(new Machine
                    {
                        Id = reader.GetInt32("id"),
                        Nom = reader.GetString("nom"),
                        Modele = reader.GetString("modele"),
                        Etat = reader.GetBoolean("etat"),
                        LaverieId = reader.GetInt32("laverie_id")
                    });
                }
                reader.Close();
            }
            finally
            {
                _connection.Close();
            }
            return machines;
        }

        public void DemarrerMachine(int machineId, int cycleId)
        {
            try
            {
                _connection.Open();


                string VerifierEtatMachine = @"
                    SELECT etat 
                    FROM machines 
                    WHERE id = @machineId;
                ";

                using (var query = new MySqlCommand(VerifierEtatMachine, _connection))
                {
                    query.Parameters.AddWithValue("@machineId", machineId);
                    var etat = Convert.ToBoolean(query.ExecuteScalar());

                    if (etat)
                        throw new Exception("La machine est déjà démarrée.");
                }
                string ChangerEtatMachine = @"
                    UPDATE machines
                    SET etat = 1
                    WHERE id = @machineId;
                ";

                using (var UpdateCommand = new MySqlCommand(ChangerEtatMachine, _connection))
                {
                    UpdateCommand.Parameters.AddWithValue("@machineId", machineId);
                    UpdateCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        public void ArreterMachine(int machineId)
        {
            try
            {
                _connection.Open();

                // Vérifier si la machine existe et est déjà arrêtée
                string VerifierMachine = @"
                    SELECT etat 
                    FROM machines 
                    WHERE id = @machineId;
                ";

                using (var Cmd = new MySqlCommand(VerifierMachine, _connection))
                {
                    Cmd.Parameters.AddWithValue("@machineId", machineId);
                    var etat = Convert.ToBoolean(Cmd.ExecuteScalar());

                    if (!etat)
                        throw new Exception("La machine est déjà arrêtée.");
                }


                string ChangerEtatMachine = @"
                    UPDATE machines
                    SET etat = 0
                    WHERE id = @machineId;
                ";

                using (var updateCmd = new MySqlCommand(ChangerEtatMachine, _connection))
                {
                    updateCmd.Parameters.AddWithValue("@machineId", machineId);
                    updateCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<Machine> GetMachinesByLaverieId(int LaverieId)
        {
            var machines = new List<Machine>();

            try
            {
                _connection.Open(); // Assurez-vous que la connexion est ouverte
                string query = "SELECT * FROM machines WHERE laverie_id = @LaverieId";
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@LaverieId", LaverieId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            machines.Add(new Machine
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("nom"),
                                Modele = reader.GetString("modele"),
                                LaverieId = reader.GetInt32("laverie_id")
                            });
                        }
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return machines;

        }


    }
}
