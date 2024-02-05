using System;
using System.Data.Common; 
 
namespace BlazorXT.Services
{
	public interface ISQLite
	{
		//SqliteConnection GetConnection();
		DbConnection GetConnectionSqlite(string dbname);
	}
}

