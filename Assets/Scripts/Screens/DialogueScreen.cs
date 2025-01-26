using UnityEngine;

namespace GGJ2025.Screens
{
    public class DialogueScreen : MonoBehaviour
    {
        [SerializeField] private DialogueManager _dman;
        
        public void SetIntroduction(OrderInfo order, GameState.HorrorLevel horrorLevel)
        {
            _dman.StartDialogue(order.introText);
            _dman.customerName = order.customer.characterName;
            _dman.personSprite.sprite = order.customer.GetSprite(horrorLevel);
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
