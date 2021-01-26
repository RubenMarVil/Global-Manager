using Assets.Scripts.Control;
using Mono.Data.Sqlite;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

public class DBCommunication
{
    public int AddCommunicate(GameConfiguration game)
    {
        int result = -1;
        List<string> tools = game.CommunicationTools.Communication;

        try
        {
            string sqlQuery = "INSERT INTO Communicate(Game, Tool1, Tool2, Tool3) VALUES(" + game.CodGame + ", '" + tools[0] + "', '" +
                tools[1] + "', '" + tools[2] + "');";

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }

    public string getTypeCommunication(string tool)
    {
        string type = "";

        try
        {
            string sqlQuery = "SELECT Type FROM COMMUNICATION WHERE Name = '" + tool + "';";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                type = data["Type"].ToString();
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return type;
    }

    public List<string> getCommunications(string type)
    {
        List<string> tools = new List<string>();

        try
        {
            string sqlQuery = "SELECT Name FROM COMMUNICATION WHERE Type = '" + type + "';";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                tools.Add(data["Name"].ToString());
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return tools;
    }

    public int DeleteCommunicate(int codGame)
    {
        int result = -1;

        try
        {
            string sqlQuery = "DELETE FROM Communicate WHERE Game=" + codGame + ";";

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }
}