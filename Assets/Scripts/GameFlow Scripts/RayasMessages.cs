using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RayasMessages : MonoBehaviour
{
    public List<string> _RayasMessages = new List<string>();

    public void AddMessage(string _Message)
    {
        _RayasMessages.Add(_Message);
        Debug.Log("Liams message saved was: " + _Message);
    }
}
