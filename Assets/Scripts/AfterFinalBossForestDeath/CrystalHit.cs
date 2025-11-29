using UnityEngine;

public class CrystalHit : MonoBehaviour
{
    [Header("Nombre maximum de touches avant désactivation")]
    private int maxHits = 3;
    [SerializeField] private GameObject portal;
    [SerializeField] private AudioSource audiosourceBreak;
    [SerializeField] private AudioSource audiosourceHit;
    private int hitCount = 0;

    // Appelé depuis un trigger ou une attaque
    public void OnHit()
    {
        hitCount++;
        Debug.Log($"Cristal touché {hitCount} fois");
        audiosourceHit.Play();
        if (hitCount >= maxHits)
        {
            audiosourceBreak.Play();
            portal.SetActive(true);
            Debug.Log("Cristal désactivé !");
            gameObject.SetActive(false); // Désactive le cristal
        }
    }
}
