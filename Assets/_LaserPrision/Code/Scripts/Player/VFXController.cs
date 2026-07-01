using System.Collections;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float damageDuration = 2f;
    [SerializeField] private Color damageColor = Color.red;

    [Header("References")]
    [SerializeField] private Renderer playerMaterial;

    public void DamageVFX()
    {
        StartCoroutine(ChangeMatColorCoroutine(damageColor));
    }

    private IEnumerator ChangeMatColorCoroutine(Color color)
    {
        Color originColor = playerMaterial.material.color;

        playerMaterial.material.color = color;
        yield return new WaitForSeconds(damageDuration);

        playerMaterial.material.color = originColor;

    }
}
