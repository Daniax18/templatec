#pragma warning disable 


using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Template.Models;

namespace Template.Utils
{
    public class Utilities
    {

        public static int GetNextSequenceAsync(string nameSequence, StoreContext context)
        {
            var connection = context.Database.GetDbConnection();
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT NEXT VALUE FOR {nameSequence}";
                var result = command.ExecuteScalar();
                var nextSequenceValue = Convert.ToInt32(result);
                return nextSequenceValue;
            }
        }


        public static string[] ExtractEntityProperties<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var properties = typeof(T).GetProperties();
            var result = new List<string>();

            foreach (var property in properties)
            {
                // Ajouter le nom de la propriété
                result.Add(property.Name);

                // Ajouter la valeur de la propriété (convertie en chaîne, ou "null" si aucune valeur)
                result.Add(property.GetValue(entity)?.ToString() ?? "null");
            }

            return result.ToArray();
        }

        public static string GetTableName<T>(DbContext context) where T : class
        {
            // Récupère l'entité associée au type T
            var entityType = context.Model.FindEntityType(typeof(T));
            if (entityType == null)
                throw new InvalidOperationException($"La table pour {typeof(T).Name} n'a pas été trouvée dans le contexte.");

            // Retourne le nom de la table
            return entityType.GetTableName();
        }

    }
}
