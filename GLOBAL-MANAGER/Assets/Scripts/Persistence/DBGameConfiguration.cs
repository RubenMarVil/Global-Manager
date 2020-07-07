using Assets.Scripts.Control;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DBGameConfiguration
{
    public int AddGameConfiguration(GameConfiguration newGame)
    {
        DBCharacteristics dBCharacteristics = new DBCharacteristics();
        int resultAddCharacteristics = dBCharacteristics.AddProjectCharacteristics(newGame.ProjectCharacteristicsList);
        int result = -1;

        if (resultAddCharacteristics == 1)
        {
            try
            {
                string sqlQuery = "SELECT last_insert_rowid() FROM CHARACTERISTICS;";
                int projectCharacteristicsID = 0;

                IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
                while (data.Read())
                {
                    projectCharacteristicsID = Int32.Parse(data["last_insert_rowid()"].ToString());
                }

                sqlQuery = "INSERT INTO GAME(Player, Language, CustomerCountry, Characteristics, NumSites, ProjectDifficulty, InitialBudget, InitialDuration) " +
                           "VALUES('" + newGame.Player + "', '" + newGame.CommonLanguage + "', '" + newGame.ClientCountry + "', " + projectCharacteristicsID +
                           ", " + newGame.NumSites + ", '" + newGame.ProjectDifficulty + "', " + newGame.InitialBudget + ", " + newGame.InitialDuration + ");";

                result = DBSingleton.GetInstance().Insert(sqlQuery);

            }
            catch (SqliteException e)
            {
                Debug.Log($"[DATABASE - ERROR] SQLiteException to insert user with the code #{e}.;");
            }
        }

        return result;
    }

    public int GetLastID()
    {
        int codGame = 0;

        try
        {
            string sqlQuery = "SELECT last_insert_rowid() FROM GAME;";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                codGame = Int32.Parse(data["last_insert_rowid()"].ToString());
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to insert user with the code #{e}.;");
        }

        return codGame;
    }
}