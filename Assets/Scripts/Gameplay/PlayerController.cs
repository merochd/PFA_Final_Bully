using UnityEngine;
using UnityEngine.InputSystem;
using Bully.Core;

namespace Bully.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO stat;
        
        private Rigidbody2D rb;
        private Vector2 movementInput;
        private bool isCowering;

        public bool IsCowering => isCowering; // Pour que le HealthHandler puisse le lire

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleInputs();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void HandleInputs()
        {
            Vector2 move = Vector2.zero;
            if (Keyboard.current != null)
            {
                // Mouvement
                if (Keyboard.current.wKey.isPressed) move.y = 1;
                if (Keyboard.current.sKey.isPressed) move.y = -1;
                if (Keyboard.current.aKey.isPressed) move.x = -1;
                if (Keyboard.current.dKey.isPressed) move.x = 1;

                // Cowering (Espace pour se recroqueviller)
                isCowering = Keyboard.current.spaceKey.isPressed;
            }
            movementInput = move.normalized;
        }

        private void MovePlayer()
        {
            // Si on se recroqueville, on est immobilisé
            if (isCowering)
            {
                rb.linearVelocity = Vector2.zero;
                return; // On sort de la fonction, pas de mouvement possible
            }

            // Mouvement normal
            rb.linearVelocity = movementInput * stat.baseMoveSpeed;
        }
    }
}