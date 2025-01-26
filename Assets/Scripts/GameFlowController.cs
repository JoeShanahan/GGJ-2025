using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GGJ2025.Screens;
using UnityEngine.Serialization;

public class GameFlowController : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    
    [SerializeField]
    private Shift[] _allKnownShiftData;
    private Shift _currentShift;
    
    [SerializeField]
    private List<CharacterData> _stillHereCharacters;

    private Queue<OrderInfo> _ordersRemaining;
    
    [SerializeField]
    private int _currentShiftIndex;

    [SerializeField]
    private GameState.HorrorLevel _currentHorrorLevel;

    private int _failedCustomerCount;
    private ScreenManager _screenMan;

    private bool _hasMadeDrink;

    private void OnDialogueDone()
    {
        if (_hasMadeDrink)
        {
            // ORDER COMPLETE
        }
        else
        {
            _screenMan.ShowIngredientPickingUI();
        }
    }
    
    private void Start()
    {
        _screenMan = FindFirstObjectByType<ScreenManager>();
        _dialogueManager.OnDialogueEnded += OnDialogueDone;
        OnGameStart();
    }

    private void OnGameStart()
    {
        OnShiftStart();
    }

    private void OnShiftStart()
    {
        _failedCustomerCount = 0;
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
        {
            Debug.LogError($"COULD NOT FIND SHIFT {_currentShiftIndex} - {_currentHorrorLevel}");
            return;
        }

        _ordersRemaining = new Queue<OrderInfo>();

        foreach (var orderInfo in _currentShift.orders)
        {
            if (_stillHereCharacters.Contains(orderInfo.customer))
                _ordersRemaining.Enqueue(orderInfo);
        }
        
        _screenMan.SetShiftStarted(_currentShift);
        Debug.Log($"At the start of shift {_currentShiftIndex} we have {_ordersRemaining.Count} orders to do");

        if (_ordersRemaining.Count == 0)
        {
            OnGameOver();
            return;
        }

        OrderInfo order = _ordersRemaining.Dequeue();
        OnOrderStart(order);
    }

    private void OnGameOver()
    {
        
    }
    
    private void OnOrderStart(OrderInfo info)
    {
        _hasMadeDrink = false;
        _screenMan.SetOrderStarted(info);
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