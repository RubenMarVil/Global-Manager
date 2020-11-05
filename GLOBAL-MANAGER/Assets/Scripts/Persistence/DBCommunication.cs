using Assets.Scripts.Control;
using Mono.Data.Sqlite;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}