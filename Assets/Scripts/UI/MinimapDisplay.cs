using UnityEngine;
using UnityEngine.UI;

namespace Bully.UI
{
    public class MinimapDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject minimapUI; // La Raw Image + le contour
        private bool _hasMap = true;

        public void SetMapActive(bool state)
        {
            _hasMap = state;
            minimapUI.SetActive(state);
        }

        // On pourra appeler cette fonction depuis le Stalker quand il t'attrape
        public void LoseMap() => SetMapActive(false);
    }
}