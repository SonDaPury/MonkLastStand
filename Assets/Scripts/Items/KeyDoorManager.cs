using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyDoorManager : MonoBehaviour
{
    public Dictionary<int, int> keyDoorPairs = new Dictionary<int, int>();
    public List<int> keys = new List<int>();
    public static KeyDoorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKeyDoor(GameObject key, GameObject door)
    {
        keyDoorPairs.Add(key.GetInstanceID(), door.GetInstanceID());
    }

    public void RemoveKeyDoor(GameObject key)
    {
        keyDoorPairs.Remove(key.GetInstanceID());
    }

    public bool CompareKeyDoor(GameObject key, Door door)
    {
        if (
            keyDoorPairs.ContainsKey(key.GetInstanceID())
            && keyDoorPairs[key.GetInstanceID()] == door.doorId
        )
        {
            return true;
        }
        return false;
    }
}
