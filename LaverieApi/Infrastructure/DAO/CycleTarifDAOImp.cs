using LaverieApi.Models.IDAO;
using MySql.Data.MySqlClient;
using LaverieDomain;
using System.Reflection.PortableExecutable;

namespace LaverieApi.Infrastructure.DAO
{
    public class CycleTarifDAOImp : ICycleTarif
    {
        private readonly MySqlConnection _connection;

        public CycleTarifDAOImp()
        {
            _connection = ConnectionDb.GetInstance().GetConnection();
        }
        public void AddCycle(CycleTarif cycle)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO cyclestarif (duree, date_debut, date_fin, tarif, machine_id) VALUES (@duree, @date_debut, @date_fin, @tarif, @machine_id)";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@duree", cycle.duree);
                cmd.Parameters.AddWithValue("@date_debut", cycle.DateDebut);
                cmd.Parameters.AddWithValue("@date_fin", cycle.DateFin);
                cmd.Parameters.AddWithValue("@tarif", cycle.Tarif);
                cmd.Parameters.AddWithValue("@machineId", cycle.MachineId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<CycleTarif> GetAll()
        {
            List<CycleTarif> cycles = new List<CycleTarif>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM cyclestarif";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cycles.Add(new CycleTarif
                    {
                        Id = reader.GetInt32("id"),
                        duree = reader.GetInt32("duree"),
                        DateDebut = reader.GetDateTime("date_debut"),
                        DateFin = reader.GetDateTime("date_fin"),
                        Tarif = reader.GetDouble("tarif"),
                        MachineId = reader.GetInt32("machine_id")
                    });
                }
                reader.Close();
            }
            finally
            {
                _connection.Close();
            }
            return cycles;
        }
        public List<CycleTarif> GetCycleByMachine(int MachineId)
        {
            var cycles = new List<CycleTarif>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM cyclestarif WHERE machine_id = @MachineId";
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@MachineId", MachineId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cycles.Add(new CycleTarif
                            {
                                Id = reader.GetInt32("id"),
                                duree = reader.GetInt32("duree"),
                                DateDebut = reader.GetDateTime("date_debut"),
                                DateFin = reader.GetDateTime("date_fin"),
                                Tarif = reader.GetDouble("tarif"),
                                MachineId = reader.GetInt32("machine_id")
                            });
                        }
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return cycles;
        }

    }
}
