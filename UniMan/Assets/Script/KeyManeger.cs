using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class KeyManeger
{
    private Dictionary<string, List<KeyCode>> config = new Dictionary<string, List<KeyCode>>();
    private readonly string FilePath;

    public KeyManeger(String FilePath)
    {
        this.FilePath = FilePath;
    }

    private bool InputKey(string KeyName, Func<KeyCode, bool> predicate)
    {
        bool ret = false;
        foreach (var keyCode in config[KeyName])
            if (predicate(keyCode))
                return true;
        return ret;
    }

    public bool GetKey(string keyName)
    {
        return InputKey(keyName, Input.GetKey);
    }

    public bool GetKeyDown(string keyName)
    {
        return InputKey(keyName, Input.GetKeyDown);
    }

    public bool GetKeyUp(string keyName)
    {
        return InputKey(keyName, Input.GetKeyUp);
    }

    public List<KeyCode> GetKeyCode(string keyName)
    {
        if (config.ContainsKey(keyName))
            return new List<KeyCode>(config[keyName]);
        return new List<KeyCode>();
    }

    public string CheckConfig()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var keyValuePair in config)
        {
            sb.AppendLine("Key : " + keyValuePair.Key);
            foreach (var value in keyValuePair.Value)
                sb.AppendLine("  |_ Value : " + value);
        }
        return sb.ToString();
    }

    public void LoadConfigFile()
    {
        //TODO:復号処理
        using (TextReader tr = new StreamReader(FilePath, Encoding.UTF8))
            config = JsonMapper.ToObject<Dictionary<string, List<KeyCode>>>(tr);
    }

    public void SaveConfigFile()
    {
        //TODO:暗号化処理
        var jsonText = JsonMapper.ToJson(config);
        using (TextWriter tw = new StreamWriter(FilePath, false, Encoding.UTF8))
            tw.Write(jsonText);
    }
}
