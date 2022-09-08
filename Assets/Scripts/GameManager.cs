using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _matchCounter = 0;
    public static int matchCounter {
        get => _matchCounter;
        set
        {
            if (_matchCounter != value)
            {
                EventManager.MatchCounterChanged(value);
                _matchCounter = value;
            }
        }
    }
     
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Debug.LogError("There is GameManager in scene more than One!!, this one is destroyed ");
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        EventManager.OnGridGenerate += EventManager_OnGridGenerate;
    }

    private void OnDisable()
    {
        EventManager.OnGridGenerate -= EventManager_OnGridGenerate;
    }

    private void EventManager_OnGridGenerate(int obj)
    {
        matchCounter = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) { matchCounter++; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { matchCounter--; }
    }

}
