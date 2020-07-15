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

    public List<Country> getAllDataAllCountries()
    {
        List<Country> countriesList = new List<Country>();

        try
        {
            string sqlQuery = "SELECT * FROM COUNTRY;";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                countriesList.Add(new Country(data["Name"].ToString(), Convert.ToInt32(data["PowerDistance"].ToString()), Convert.ToInt32(data["Individualism"].ToString()),
                    Convert.ToInt32(data["Masculinity"].ToString()), Convert.ToInt32(data["UncertantyAvoidance"].ToString()), Convert.ToInt32(data["LongTermOrientation"].ToString()),
                    Convert.ToInt32(data["Indulgence"].ToString()), Convert.ToSingle(data["TimeZone"].ToString()), Convert.ToSingle(data["Salary"].ToString()),
                    Convert.ToInt32(data["Instability"].ToString()), Convert.ToDouble(data["Latitude"].ToString()), Convert.ToDouble(data["Longitude"].ToString())));
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return countriesList;
    }

    public Country getAllDataOfCountry(string countryName)
    {
        Country country = null;

        try
        {
            string sqlQuery = "SELECT * FROM COUNTRY WHERE Name = '" + countryName + "';";

            IDataReader data = DBSingleton.GetInstance().Read(sqlQuery);
            while (data.Read())
            {
                country = new Country(data["Name"].ToString(), Convert.ToInt32(data["PowerDistance"].ToString()), Convert.ToInt32(data["Individualism"].ToString()),
                    Convert.ToInt32(data["Masculinity"].ToString()), Convert.ToInt32(data["UncertantyAvoidance"].ToString()), Convert.ToInt32(data["LongTermOrientation"].ToString()),
                    Convert.ToInt32(data["Indulgence"].ToString()), Convert.ToSingle(data["TimeZone"].ToString()), Convert.ToSingle(data["Salary"].ToString()),
                    Convert.ToInt32(data["Instability"].ToString()), Convert.ToDouble(data["Latitude"].ToString()), Convert.ToDouble(data["Longitude"].ToString()));
            }
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to get users with the code #{e}.");
        }

        return country;
    }
}