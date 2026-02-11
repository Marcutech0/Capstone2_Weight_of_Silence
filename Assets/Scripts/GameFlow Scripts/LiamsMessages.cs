using System.Collections.Generic;
using UnityEngine;

public class LiamsMessages : MonoBehaviour
{
    public List<string> _LiamMessages = new List<string>();

    public void AddMessage(string _Message)
    {
        _LiamMessages.Add(_Message);
        Debug.Log("Liams message saved was: " + _Message);
    }
}
