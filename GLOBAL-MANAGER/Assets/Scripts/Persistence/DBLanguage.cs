using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DBLanguage
{
    public List<string> getAllLanguages()
    {
        List<string> languagesList = new List<string>();

        try
        {
            string sqlQuery = "SELECT Name FROM LANGUAGE;";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                languagesList.Add(data["Name"].ToString());
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return languagesList;
    }
}
