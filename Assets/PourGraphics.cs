using UnityEngine;

namespace GGJ2025.PouringGame
{
    public class PourGraphics : MonoBehaviour
    {
        [SerializeField] 
        private SkinnedMeshRenderer _pourMesh;

        public void SetPourRate(float rate)
        {
            _pourMesh.SetBlendShapeWeight(0, (1 - rate) * 100);
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