using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.PouringGame
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField]
        private float _xMin;
    
        [SerializeField]
        private AnimationCurve _turnCurve;

        [SerializeField, Range(1, 32)] 
        private int _dragResponsiveness = 16;
        
        private bool _isMouseDown;
        private Vector3 _mouseOffset;
        private Camera _mainCam;
        
        private void OnMouseDown()
        {
            _mouseOffset = _mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _mouseOffset.z = 0;

            _isMouseDown = true;
        }

        private void OnMouseUp()
        {
            _isMouseDown = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            _mainCam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMouseDown)
            {
                _mouseOffset = Vector3.Lerp(_mouseOffset, Vector3.zero, Time.deltaTime * 8);
                var mpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCam.nearClipPlane);
                Vector3 pos = _mainCam.ScreenToWorldPoint(mpos);
                
                pos.z = transform.position.z;
                Vector3 endPos = pos - _mouseOffset;
                transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * _dragResponsiveness);
            }
        }
    }
}