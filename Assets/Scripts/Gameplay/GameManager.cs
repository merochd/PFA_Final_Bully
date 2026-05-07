using UnityEngine;
using UnityEngine.SceneManagement;
using Bully.Core;

namespace Bully.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            // On s'abonne à l'événement de mort
            BullyEvents.OnAnchoringDepleted += HandleGameOver;
        }

        private void OnDisable()
        {
            // Toujours se désabonner pour éviter les fuites de mémoire
            BullyEvents.OnAnchoringDepleted -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            Debug.Log("<color=orange>GAME OVER : Ton ancrage a été rompu.</color>");
            // On attend 2 secondes avant de reset pour laisser le joueur réaliser sa défaite
            Invoke(nameof(RestartLevel), 2f);
        }

        private void RestartLevel()
        {
            // Recharge la scène actuelle
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}