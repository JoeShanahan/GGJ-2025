using UnityEngine;

namespace GGJ2025.PouringGame
{
    public class DrinkMakeResult
    {
        // public bool WasSuccess => MixSuccess && FillSuccess;
        public bool WasSuccess => true;

        public string MixMessage;
        public bool MixSuccess;

        public string FillMessage;
        public bool FillSuccess;

        public CocktailData Cocktail;

        public Sprite Icon => WasSuccess ? Cocktail.Icon : Cocktail.FailedIcon;
    }
}