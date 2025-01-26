using System;
using UnityEngine;

namespace GGJ2025.Screens
{
    public class PourDrinkScreen : MonoBehaviour
    {
        [SerializeField] private Transform _gameArea;

        private void OnEnable()
        {
            _gameArea.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _gameArea.gameObject.SetActive(false);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}