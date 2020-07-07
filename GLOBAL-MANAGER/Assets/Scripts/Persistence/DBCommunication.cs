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

        try
        {
            string sqlQuery;

            foreach(CommunicationConfiguration communicate in game.CommunicationsList)
            {
                foreach(CommunicationTool tool in communicate.Communication)
                {
                    sqlQuery = "INSERT INTO Communicate(Game, Site1, Site2, Communication) VALUES(" + game.CodGame + ", " + communicate.CodSite1 + ", " +
                               communicate.CodSite2 + ", '" + tool + "');";

                    result = DBSingleton.GetInstance().Insert(sqlQuery);
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return result;
    }
}