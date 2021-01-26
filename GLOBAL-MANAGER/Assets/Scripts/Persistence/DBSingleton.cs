using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;
using System.Data;

public class DBSingleton
{
    private static DBSingleton instance = null;
    private readonly string dbConnectionString = "URI=file:" + Application.dataPath + "/" + "Global-ManagerDB.db";
    private string dbPath = Application.dataPath + "/" + "Global-ManagerDB.db";
    private static IDbConnection dbConnection;
    private IDbCommand dbCommand;
    private static bool created = false;

    private DBSingleton()
    {
        dbConnection = new SqliteConnection(dbConnectionString);
        dbConnection.Open();

        Debug.Log($"[DBSingleton - INFO] Database connection into the path: {dbPath}");

        //GenerateDatabase();
    }

    public static DBSingleton GetInstance()
    {
        if(instance == null)
        {
            instance = new DBSingleton();
        }

        return instance;
    }

    private void GenerateDatabase()
    {
        if (!created)
        {
            string sqlQuery = File.ReadAllText(@".\Assets\Scripts\Persistence\CreateDB.sql");
            //Debug.Log($"[DBSingleton - INFO] SQL QUERY = {sqlQuery}");

            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteNonQuery();

            sqlQuery = File.ReadAllText(@".\Assets\Scripts\Persistence\Prueba.sql");
            //Debug.Log($"[DBSingleton - INFO] SQL QUERY = {sqlQuery}");

            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteNonQuery();

            created = true;

            Debug.Log($"[DBSingleton - INFO] Database created into the path: {dbPath}");
        }
    }

    public IDataReader Read(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        //Debug.Log($"[DBSingleton - INFO] SQL QUERY = {sqlQuery}");

        IDataReader result = dbCommand.ExecuteReader();

        return result;
    }

    public int Insert(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        //Debug.Log($"[DBSingleton - INFO] SQL QUERY = {sqlQuery}");

        int result = dbCommand.ExecuteNonQuery();

        //CloseDB();

        return result;
    }

    public static void CloseDB()
    {
        dbConnection.Close();
    }
}
