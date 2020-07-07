using Assets.Scripts.Control;
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
            Debug.Log($"[CONTROL USER - ERROR] New user '{newUser.Name}' inserted into the database.");
            actualUser = newUser;
        }

        return inserted;
    }
}

