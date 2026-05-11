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
        public bool isCowering { get; private set; }
        public bool IsRestricted { get; set; }

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
            if (IsRestricted)
            {
                // On force la vitesse à zéro pour stopper net l'inertie
                rb.linearVelocity = Vector2.zero;
                return;
            }
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