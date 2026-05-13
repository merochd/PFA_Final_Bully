using UnityEngine;
using Bully.Core;

public class MinimapUIController : MonoBehaviour
{
    private void OnEnable() => BullyEvents.OnPhoneStateChanged += ToggleMinimap;
    private void OnDisable() => BullyEvents.OnPhoneStateChanged -= ToggleMinimap;

    private void ToggleMinimap(bool isAvailable)
    {
        gameObject.SetActive(isAvailable);
    }
}