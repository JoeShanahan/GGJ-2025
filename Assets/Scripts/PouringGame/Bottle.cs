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
        private Transform _pourPoint;
        
        [SerializeField]
        private AnimationCurve _turnCurve;

        [SerializeField, Range(1, 32)] 
        private int _dragResponsiveness = 16;
        
        [SerializeField, Range(1, 32)] 
        private int _rotationResponsiveness = 16;

        [SerializeField, Range(0, 180)] private int _startPouringRotate;
        [SerializeField, Range(0, 180)] private int _endPouringRotate;
        [SerializeField, Range(0, 10)] private float _maxPourRate;
        
        private bool _isMouseDown;
        private Vector3 _mouseOffset;
        private Camera _mainCam;
        private PourGraphics _pourGraphics;
        
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
            _pourGraphics = FindFirstObjectByType<PourGraphics>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMouseDown)
            {
                HandleDragging();
            }
        }

        private void HandleDragging()
        {
            _mouseOffset = Vector3.Lerp(_mouseOffset, Vector3.zero, Time.deltaTime * 8);
            var mpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCam.nearClipPlane);
            Vector3 pos = _mainCam.ScreenToWorldPoint(mpos);
                
            pos.z = transform.position.z;
            Vector3 endPos = pos - _mouseOffset;
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * _dragResponsiveness);
            _pourGraphics.transform.position = _pourPoint.transform.position;

            float xDistance = Mathf.Clamp(_xMin - transform.position.x, 0, 100);
            float rotate = _turnCurve.Evaluate(xDistance);

            Vector3 endRot = new Vector3(0, 0, rotate);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, endRot, Time.deltaTime * _rotationResponsiveness);

            float pourRate = Mathf.InverseLerp(_startPouringRotate, _endPouringRotate, transform.eulerAngles.z);
            _pourGraphics.SetPourRate(pourRate);
        }
    }
}