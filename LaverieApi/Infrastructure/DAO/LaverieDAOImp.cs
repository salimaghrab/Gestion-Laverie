using LaverieApi.Models.IDAO;
using LaverieDomain;
using MySql.Data.MySqlClient;

namespace LaverieApi.Infrastructure.DAO
{
    public class LaverieDAOImp : ILaverieDAO
    {
        private readonly MySqlConnection _connection;

        public LaverieDAOImp()
        {
            _connection = ConnectionDb.GetInstance().GetConnection();
        }
        public void AddLaverie(Laverie laverie)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO laveries (nom, adresse, proprietaire_id) VALUES (@nom, @adresse, @proprietaire_id)";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@nom", laverie.Nom);
                cmd.Parameters.AddWithValue("@adresse", laverie.Adresse);
                cmd.Parameters.AddWithValue("@proprietaire_id", laverie.ProprietaireId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<Laverie> GetAllLaveries()
        {
            List<Laverie> laveries = new List<Laverie>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM laveries";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    laveries.Add(new Laverie
                    {
                        Id = reader.GetInt32("id"),
                        Nom = reader.GetString("nom"),
                        Adresse = reader.GetString("adresse"),
                        ProprietaireId = reader.GetInt32("proprietaire_id")
                    });
                }
                reader.Close();
            }
            finally
            {
                _connection.Close();
            }
            return laveries;
        }

        public List<Laverie> GetLaveriesByProprietaireId(int proprietaireId)
        {
            var laveries = new List<Laverie>();

            try
            {
                _connection.Open(); // Assurez-vous que la connexion est ouverte
                string query = "SELECT * FROM laveries WHERE proprietaire_id = @ProprietaireId";
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@ProprietaireId", proprietaireId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            laveries.Add(new Laverie
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("nom"),
                                Adresse = reader.GetString("adresse"),
                                ProprietaireId = reader.GetInt32("proprietaire_id") 
                            });
                        }
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return laveries; // Retourne la liste des laveries, même si elle est vide
        }


    }
}
