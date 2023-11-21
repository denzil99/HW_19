using Dapper;
using System.Data;
using System.Data.SQLite;

namespace HW19.DAL.Repositories
{
	public class BaseRepository
	{
		protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
		{
			using (var connection = CreateConnection())
			{
				connection.Open();
				return connection.QueryFirstOrDefault<T>(sql, parameters);
			}
		}

		protected List<T> Query<T>(string sql, object parameters = null)
		{
			using (var connection = CreateConnection())
			{
				connection.Open();
				return connection.Query<T>(sql, parameters).ToList();
			}
		}

		protected int Execute(string sql, object parameters = null)
		{
			using (var connection = CreateConnection())
			{
				connection.Open();
				return connection.Execute(sql, parameters);
			}
		}

		private IDbConnection CreateConnection()
		{
			return new SQLiteConnection("Data Source = C:\\Users\\Denzil\\source\\repos\\HW_19\\HW19.DAL\\DB\\social_network_bd.db; Version = 3");
		}
	}
}
