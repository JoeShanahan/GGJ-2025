using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class GameFlowController : MonoBehaviour
{
    [SerializeField]
    private Shift[] _allKnownShiftData;
    private Shift _currentShift;
    
    [SerializeField]
    private List<CharacterData> _stillHereCharacters;

    [SerializeField]
    private int _currentShiftIndex;

    [SerializeField]
    private int _currentCharacterIndex;
    
    [SerializeField]
    private GameState.HorrorLevel _currentHorrorLevel;

    private int _failedCustomerCount;
    
    private void Start()
    {
        OnGameStart();
    }

    private void OnGameStart()
    {
        OnShiftStart();
    }
    
    private void OnShiftStart()
    {
        _failedCustomerCount = 0;
        _currentCharacterIndex = 0;
        _currentShift = null;
        
        foreach (Shift s in _allKnownShiftData)
        {
            if (s.stage == _currentShiftIndex && s.horrorLevel == _currentHorrorLevel)
            {
                _currentShift = s;
                break;
            }
        }
        
        if (_currentShift == null)
            Debug.LogError($"COULD NOT FIND SHIFT {_currentShiftIndex} - {_currentHorrorLevel}");
    }
    
    private void OnOrderStart()
    {
        //Get Current order from current shift

        //Send intro dialogue to dialogue system
    }

    private void OnShiftEnd()
    {
        if (_failedCustomerCount == 0)
        {
            _currentHorrorLevel = (GameState.HorrorLevel)((int)_currentHorrorLevel + 1);
            Debug.Log("No customers failed. Horror level increased.");
        }
        
        _currentShiftIndex++;
        Debug.Log($"Shift increased to: {_currentShiftIndex}");

        if (_stillHereCharacters.Count == 0)
        {
            OnUnemployed();
        }
    }

    private void OnUnemployed()
    {
        
    }

    private void OnOrderEnd(bool success)
    {
        //Check score, if failed add customer to failed list

        //Send success/fail dialogue to dialogue system

        //If failed, incriment failed this shift
    }
}