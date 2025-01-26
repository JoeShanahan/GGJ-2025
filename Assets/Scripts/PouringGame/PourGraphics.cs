using UnityEngine;

namespace GGJ2025.PouringGame
{
    public class PourGraphics : MonoBehaviour
    {
        [SerializeField] 
        private SkinnedMeshRenderer _pourMesh;

        [SerializeField] 
        private Material _pourMaterial;
        
        private float _internalMultilpier;
        private float _pourRate;
        private bool _isPouring;
        private Vector2 _scrollOffset;
        
        public void OnPourStart(IngredientData liquid)
        {
            _isPouring = true;
            _pourMesh.material.SetColor("_Color", liquid.Tint);
        }

        public void OnPourEnd()
        {
            _isPouring = false;
        }
        
        public void SetPourRate(float rate)
        {
            _pourRate = rate;
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Application.targetFrameRate = 60;
            SetPourRate(0);
        }

        // Update is called once per frame
        void Update()
        {
            _internalMultilpier = Mathf.Lerp(_internalMultilpier, _isPouring ? 1 : 0, Time.deltaTime * 24);
            _pourMesh.SetBlendShapeWeight(0, (1 - (_pourRate * _internalMultilpier)) * 100);

            float shakeMagnitude = Mathf.Sin(Time.time * 9f) * _pourRate * 6;
            _pourMesh.SetBlendShapeWeight(1, shakeMagnitude);

            _scrollOffset.y -= Time.deltaTime * 4;
            _pourMesh.material.SetVector("_Scroll", _scrollOffset);
        }
    }
}