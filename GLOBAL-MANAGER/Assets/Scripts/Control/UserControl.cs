using Assets.Scripts.Control;
using System.Collections.Generic;
using UnityEngine;

public class UserControl
{
    public static User actualUser;

    public static bool CreateNewUser(User newUser)
    {
        bool inserted = true;
        DBUsers dbUsers = new DBUsers();
        int result = dbUsers.AddUser(newUser);

        if(result == -1)
        {
            Debug.Log($"[CONTROL USER - ERROR] New user {newUser.Name} did not inserted into the database.");
            inserted = false;
        } 
        else if(result == 1) {
            Debug.Log($"[CONTROL USER - INFO] New user '{newUser.Name}' inserted into the database.");
            actualUser = newUser;
        }

        return inserted;
    }

    public static List<User> GetAllPlayers()
    {
        DBUsers dbUsers = new DBUsers();
        List<User> userList = dbUsers.GetPlayers(null);

        return userList;
    }

    public static bool UpdateScoreUserLevel()
    {
        bool inserted = true;
        DBUsers dbUsers = new DBUsers();
        int result = dbUsers.UpdateScoreUserLevel(actualUser);

        if (result == -1)
        {
            Debug.Log($"[CONTROL USER - ERROR] User '{actualUser.Name}' did not updated into the database.");
            inserted = false;
        }
        else if (result == 1)
        {
            Debug.Log($"[CONTROL USER - ERROR] User '{actualUser.Name}' updated into the database.");
        }

        return inserted;
    }
}

