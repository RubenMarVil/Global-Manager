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
                           ", " + newGame.NumSites + ", '" + newGame.ProjectDifficulty + "', " + newGame.InitialBudget.ToString().Replace(",", ".") + ", " + newGame.InitialDuration.ToString().Replace(",", ".") + ");";

                result = DBSingleton.GetInstance().Insert(sqlQuery);

            }
            catch (SqliteException e)
            {
                Debug.Log($"[DATABASE - ERROR] SQLiteException to insert game configuration with the code #{e}.;");
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
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get last id with the code #{e}.;");
        }

        return codGame;
    }

    public int InsertResultGame(int codGame, float stressValue, float progressValue, float budgetValue, float durationValue, int totalNegativeEvents, int correctNegativeEvents)
    {
        int result = -1;

        try
        {
            string sqlQuery = "UPDATE GAME SET StressValue = " + stressValue.ToString().Replace(",", ".") + ", ProgressValue = " + progressValue.ToString().Replace(",", ".") + 
                ", BudgetValue = " + budgetValue.ToString().Replace(",", ".") + ", DurationValue = " + durationValue.ToString().Replace(",", ".") + ", " +
            "TotalNegativeEvents = " + totalNegativeEvents + ", CorrectNegativeEvents = " + correctNegativeEvents + " WHERE CodGame = " + codGame + ";";

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to insert result game with the code #{e}.;");
        }

        return result;
    }

    public List<GameConfiguration> GetAllGamesOfPlayer(string username)
    {
        List<GameConfiguration> listGames = new List<GameConfiguration>();

        try
        {
            string sqlQuery = "SELECT StressValue, ProgressValue, BudgetValue, DurationValue, TotalNegativeEvents, CorrectNegativeEvents, ProjectDifficulty FROM GAME WHERE Player = '" + username + "';";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                if (data["StressValue"].ToString() != null)
                {
                    GameConfiguration game = new GameConfiguration
                    {
                        StressValue = Convert.ToSingle(data["StressValue"].ToString()),
                        ProgressValue = Convert.ToSingle(data["ProgressValue"].ToString()),
                        BudgetValue = Convert.ToSingle(data["BudgetValue"].ToString()),
                        DurationValue = Convert.ToSingle(data["DurationValue"].ToString()),
                        TotalNegativeEvents = Int32.Parse(data["TotalNegativeEvents"].ToString()),
                        CorrectNegativeEvents = Int32.Parse(data["CorrectNegativeEvents"].ToString())
                    };
                    game.setProjectDifficulty(data["ProjectDifficulty"].ToString());

                    listGames.Add(game);
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get games of one player with the code #{e}.;");
        }

        return listGames;
    }
}