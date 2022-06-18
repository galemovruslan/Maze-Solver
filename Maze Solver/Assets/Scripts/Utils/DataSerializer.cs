using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataSerializer
{

    private static readonly string FileName = "data";

    public static void WriteTestData()
    {
        string writePAth = Application.dataPath + "/" + FileName;
        var data = new LevelDescription(2, 4, CellType.Triangle, CarverAlgorithm.Prim, Difficulty.Walking);
        Write(writePAth, data);
        Debug.Log($"Writen: {data._mazeWidth}, {data._mazeHeight}, {data._cellType}, {data._carverAlgorithm}, {data._difficulty}");

        var readData = Read<LevelDescription>(writePAth);
        Debug.Log($"Read: {readData._mazeWidth}, {readData._mazeHeight}, {readData._cellType}, {readData._carverAlgorithm}, {readData._difficulty}");
    }

    public static void Write<T>(string path, T data)
    {

        using (var writer = new StreamWriter(new FileStream(path, FileMode.Create)))
        {
            var jsomText = JsonUtility.ToJson(data);
            writer.Write(jsomText);
        }
    }

    public static T Read<T>(string fileName)
    {
        T returnedObject;

        using (var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
        {
            var readData = reader.ReadToEnd();
            returnedObject = JsonUtility.FromJson<T>(readData);
        }

        return returnedObject;
    }


    private struct TestStruct
    {
        public int INT;
        public float FLOAT;
        public bool BOOL;

    }

}