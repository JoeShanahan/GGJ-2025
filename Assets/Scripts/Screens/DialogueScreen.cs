using UnityEngine;

namespace GGJ2025.Screens
{
    public class DialogueScreen : MonoBehaviour
    {
        [SerializeField] private DialogueManager _dman;
        
        public void SetIntroduction(OrderInfo order)
        {
            _dman.StartDialogue(order.introText);
        }

        public void SetComplete(OrderInfo info, bool success)
        {
            if (success)
                _dman.StartDialogue(info.successText);
            else
                _dman.StartDialogue(info.failText);
        }
    }
}
