using LaverieApi.Models.IDAO;
using LaverieDomain;
using MySql.Data.MySqlClient;
using BCrypt.Net;
namespace LaverieApi.Infrastructure.DAO
{
    public class ProprietaireDAOImp : IProprietaireDAO 
    {
        private readonly MySqlConnection _connection;
        public ProprietaireDAOImp()
        {
            _connection = ConnectionDb.GetInstance().GetConnection();
        }
        public void AddProprietaire(Proprietaire proprietaire)
        {
            try
            {
                _connection.Open();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(proprietaire.Password);
                string query = "INSERT INTO proprietaires (nom, prenom, email, telephone, adresse, password) VALUES (@nom, @prenom, @email, @telephone, @adresse, @password)";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@nom", proprietaire.Nom);
                cmd.Parameters.AddWithValue("@prenom", proprietaire.Prenom);
                cmd.Parameters.AddWithValue("@email", proprietaire.Email);
                cmd.Parameters.AddWithValue("@telephone", proprietaire.Telephone);
                cmd.Parameters.AddWithValue("@adresse", proprietaire.Adresse);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

     

        public List<Proprietaire> GetAllProprietaire()
        {
            List<Proprietaire> proprietaires = new List<Proprietaire>();
            string query = "SELECT * FROM proprietaires";
            using (var command = new MySqlCommand(query, _connection))
            {
                _connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proprietaires.Add(new Proprietaire
                        {
                            Id = reader.GetInt32("Id"),
                            Nom = reader.GetString("nom"),
                            Prenom = reader.GetString("prenom"),
                            Email = reader.GetString("email"),
                            Telephone = reader.GetString("telephone"),
                            Adresse = reader.GetString("adresse")

                        });
                    }
                }
                _connection.Close();
            }
            return proprietaires;
        }
        public Proprietaire GetProprietaireByEmail(string email)
        {
            try
            {
               _connection.Open(); // Assurez-vous que la connexion est ouverte
                string query = "SELECT * FROM proprietaires WHERE email = @Email";
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Proprietaire
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("nom"),
                                Prenom = reader.GetString("prenom"),
                                Email = reader.GetString("email"),
                                Telephone = reader.GetString("telephone"),
                                Adresse = reader.GetString("adresse"),
                                Password = reader.GetString("password") // Assurez-vous que le mot de passe est bien récupéré
                            };
                        }
                    }
                }
            }
            finally
            {
                _connection.Close(); 
            }

            return null; 
        }
    }
}
