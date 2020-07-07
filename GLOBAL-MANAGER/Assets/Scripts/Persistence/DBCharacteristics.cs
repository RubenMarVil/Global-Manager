using Assets.Scripts.Control;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DBCharacteristics
{
    public int AddProjectCharacteristics(List<ProjectCharacteristic> CharacteristicsList)
    {
        int result = -1;

        try
        {
            string firstPartSqlQuery = "INSERT INTO CHARACTERISTICS(";
            string secondPartSqlQuery = "VALUES(";
            foreach (ProjectCharacteristic characteristic in CharacteristicsList)
            {
                firstPartSqlQuery += characteristic.Name + ", ";
                secondPartSqlQuery += "'" + characteristic.Level + "', ";
            }

            string sqlQuery = firstPartSqlQuery.Substring(0, firstPartSqlQuery.Length - 2) + ") " + secondPartSqlQuery.Substring(0, secondPartSqlQuery.Length - 2) + ");";
            Debug.Log($"[DBCharacteristics - INFO] SQL QUERY = {sqlQuery}");

            result = DBSingleton.GetInstance().Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log($"[DATABASE - ERROR] SQLiteException to insert user with the code #{e}.;");
        }

        return result;
    }
}