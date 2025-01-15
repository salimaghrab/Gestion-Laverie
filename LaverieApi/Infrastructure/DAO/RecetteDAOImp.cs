using LaverieApi.Models.IDAO;
using MySql.Data.MySqlClient;

namespace LaverieApi.Infrastructure.DAO
{
    public class RecetteDAOImp : IRecetteDAO
    {
        private readonly MySqlConnection _connection;

        public RecetteDAOImp()
        {
            _connection = ConnectionDb.GetInstance().GetConnection();
        }

        public async Task AjouterOuMettreAJourRecetteAsync(int machineId, double montant, DateTime date)
        {
            try
            {
                _connection.Open();

                string queryCheck = @"
                    SELECT COUNT(*) 
                    FROM recette 
                    WHERE MachineId = @machineId AND DATE(Date) = @date";

                int count = 0;

                // Vérifiez si une ligne existe avec le même MachineId et la même date
                using (var checkCommand = new MySqlCommand(queryCheck, _connection))
                {
                    checkCommand.Parameters.AddWithValue("@machineId", machineId);
                    checkCommand.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd")); // Format pour comparer uniquement la date
                    count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                }

                // Si une ligne existe, mettre à jour le montant
                if (count > 0)
                {
                    string queryUpdate = @"
                        UPDATE recette 
                        SET Montant = Montant + @montant 
                        WHERE MachineId = @machineId AND DATE(Date) = @date";

                    using (var updateCommand = new MySqlCommand(queryUpdate, _connection))
                    {
                        updateCommand.Parameters.AddWithValue("@machineId", machineId);
                        updateCommand.Parameters.AddWithValue("@montant", montant);
                        updateCommand.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    // Sinon, insérez une nouvelle ligne
                    string queryInsert = @"
                        INSERT INTO recette (MachineId, Montant, Date) 
                        VALUES (@machineId, @montant, @date)";

                    using (var insertCommand = new MySqlCommand(queryInsert, _connection))
                    {
                        insertCommand.Parameters.AddWithValue("@machineId", machineId);
                        insertCommand.Parameters.AddWithValue("@montant", montant);
                        insertCommand.Parameters.AddWithValue("@date", date);
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérez les exceptions si nécessaire
                throw new Exception($"Erreur lors de l'ajout ou de la mise à jour de la recette : {ex.Message}");
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        public async Task<double> ObtenirRecetteParMachineEtDateAsync(int machineId, DateTime date)
        {
            double recette = 0;

            try
            {
                _connection.Open();

                string query = @"
                    SELECT Montant 
                    FROM recette 
                    WHERE MachineId = @machineId AND DATE(Date) = @date";

                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@machineId", machineId);
                    command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));

                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        recette = Convert.ToDouble(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération de la recette : {ex.Message}");
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return recette;
        }
    }
}
