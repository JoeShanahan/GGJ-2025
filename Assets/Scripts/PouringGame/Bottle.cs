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

        [SerializeField] private IngredientData _ingredient;
        
        private bool _isMouseDown;
        private Vector3 _mouseOffset;
        private Camera _mainCam;
        private PourGraphics _pourGraphics;
        private Rigidbody2D _rb;
        private ShakerGraphics _shakerGraphics;
        
        private void OnMouseDown()
        {
            _mouseOffset = _mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _mouseOffset.z = 0;

            _isMouseDown = true;
            _rb.freezeRotation = true;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.linearVelocity = Vector2.zero;
            _pourGraphics.OnPourStart(_ingredient);
        }

        private void OnMouseUp()
        {
            _isMouseDown = false;
            _rb.freezeRotation = false;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _pourGraphics.OnPourEnd();
        }

        // Start is called before the first frame update
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _mainCam = Camera.main;

            StartCoroutine(FindPourGraphics());
            StartCoroutine(FindShakerGraphics());
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMouseDown)
            {
                HandleDragging();
            }
        }

        private IEnumerator FindPourGraphics() {
            while (_pourGraphics == null) {
                _pourGraphics = FindFirstObjectByType<PourGraphics>();

                if (_pourGraphics == null) {
                    Debug.LogWarning("Waiting for PourGraphics to be instantiated...");
                    yield return new WaitForSeconds(0.1f);
                }
            }

            Debug.Log("PourGraphics found!");
        }

        private IEnumerator FindShakerGraphics() {
            while (_shakerGraphics == null) {
                _shakerGraphics = FindFirstObjectByType<ShakerGraphics>();

                if (_shakerGraphics == null) {
                    Debug.LogWarning("Waiting for ShakerGraphics to be instantiated...");
                    yield return new WaitForSeconds(0.1f);
                }
            }

            Debug.Log("ShakerGraphics found!");
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

            Quaternion endRot = Quaternion.Euler(new Vector3(0, 0, rotate));
            transform.rotation = Quaternion.Lerp(transform.rotation, endRot, Time.deltaTime * _rotationResponsiveness);

            float pourRate = Mathf.InverseLerp(_startPouringRotate, _endPouringRotate, transform.eulerAngles.z);

            if (xDistance <= 0)
                pourRate = 0;
            
            _pourGraphics.SetPourRate(pourRate);
            _shakerGraphics.AddLiquid(_ingredient, pourRate * _maxPourRate);
        }
    }
}