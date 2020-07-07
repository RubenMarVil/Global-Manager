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
            string sqlQuery = "SELECT * FROM Players WHERE Username = '" + username + "';";
            if (String.IsNullOrWhiteSpace(username))
            {
                sqlQuery = "SELECT * FROM Players";
            }

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);

            while(data.Read())
            {
                User user = new User
                {
                    Name = data["Username"].ToString(),
                    Age = Int32.Parse(data["Age"].ToString())
                };
                users.Add(user);
            }
        } 
        catch(SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return users;
    }

    public int AddUser(User newUser)
    {
        int result = -1;

        try
        {
            string sqlQuery = "INSERT INTO PLAYER(Username, Age, UserLevel, Score, NumProjects) VALUES ('" + newUser.Name + "', " + 
                newUser.Age.ToString() + ", '" + newUser.UserLevel.ToString() + "', " + newUser.Score.ToString() + ", " + newUser.NumProjects + ");";
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
