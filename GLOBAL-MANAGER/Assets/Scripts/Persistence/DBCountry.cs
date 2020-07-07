using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DBCountry
{
    public List<string> getAllCountries()
    {
        List<string> countriesList = new List<string>();

        try
        {
            string sqlQuery = "SELECT Name FROM COUNTRY;";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                countriesList.Add(data["Name"].ToString());
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return countriesList;
    }
}