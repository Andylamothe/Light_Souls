using UnityEngine;

public class ArmHitDetector : MonoBehaviour
{
    private bool canHit = false;
    
    public void EnableHit()
    {
        canHit = true;
    }

    public void DisableHit()
    {
        canHit = false;
    }

private float hitCooldown = 1.5f;
private float lastHitTime = 0f;

private void OnTriggerStay(Collider other)
{
    if (!canHit) return;
    if (Time.time - lastHitTime < hitCooldown) return; // cooldown

    if (other.CompareTag("Target"))
    {
        CrystalHit crystal = other.GetComponent<CrystalHit>();
        if (crystal != null)
        {
            crystal.OnHit();
            lastHitTime = Time.time; // reset cooldown
        }
    }
}

}
