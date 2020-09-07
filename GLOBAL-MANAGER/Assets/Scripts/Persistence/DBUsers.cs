using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using Assets.Scripts.Control;
using System.Data;

public class DBUsers
{
    public List<User> GetPlayers(string username)
    {
        List<User> users = new List<User>();

        try
        {
            string sqlQuery = "SELECT * FROM PLAYER WHERE Username = '" + username + "';";
            if (String.IsNullOrWhiteSpace(username))
            {
                sqlQuery = "SELECT * FROM PLAYER";
            }

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);

            while(data.Read())
            {
                User user = new User
                {
                    Name = data["Username"].ToString(),
                    Age = Int32.Parse(data["Age"].ToString()),
                    Score = Int32.Parse(data["Score"].ToString()),
                    NumProjects = Int32.Parse(data["NumProjects"].ToString()),
                    IsMan = Int32.Parse(data["IsMan"].ToString()) != 0
                };
                user.SetUserLevel(data["UserLevel"].ToString());

                users.Add(user);
            }
        } 
        catch(SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return users;
    }

    public int UpdateScoreUserLevel(User user)
    {
        int result = -1;

        try
        {
            string sqlQuery = "UPDATE PLAYER SET UserLevel = '" + user.UserLevel.ToString() + "', Score = " + user.Score + ", NumProjects = " + user.NumProjects +
                " WHERE Username = '" + user.Name + "';";

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to update user with the code #{e}.;");
        }

        return result;
    }

    public int AddUser(User newUser)
    {
        int result = -1;

        try
        {
            string sqlQuery = "INSERT INTO PLAYER(Username, Age, UserLevel, Score, NumProjects, IsMan) VALUES ('" + newUser.Name + "', " + 
                newUser.Age.ToString() + ", '" + newUser.UserLevel.ToString() + "', " + newUser.Score.ToString() + ", " + newUser.NumProjects + ", " + Convert.ToInt32(newUser.IsMan).ToString() + ");";
            Debug.Log($"[DBUsers - INFO] SQL QUERY = {sqlQuery}");

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to insert user with the code #{e}.;");
        }

        return result;
    }
}
