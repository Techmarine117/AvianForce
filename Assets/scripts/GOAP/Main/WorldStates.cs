using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldState
{
    public string Key;
    public int value;

}

public class WorldStates
{
    public Dictionary<string, int> states;
    public WorldStates()
    {
        states = new Dictionary<string, int>();

    }

    public bool HasState(string Key)
    {
        return states.ContainsKey(Key);
    }

    void AddState(string Key, int value)
    {
        states.Add(Key, value);
    }

    public void ModifyState(string Key, int value)
    {
        if (states.ContainsKey(Key))
        {
            states[Key] += value;
            if (states[Key] <= 0)
                RemoveState(Key);
        }
        else
            states.Add(Key, value);
    }

    public void RemoveState(string Key)
    {
        if (states.ContainsKey(Key))
            states.Remove(Key);
    }

    public void SetState(string Key ,  int value)
    {
        if (states.ContainsKey(Key))
            states[Key] = value;
        else
            states.Add(Key, value);
    }
    public Dictionary<string, int> GetStates()
    {
        return states;
    }
}
