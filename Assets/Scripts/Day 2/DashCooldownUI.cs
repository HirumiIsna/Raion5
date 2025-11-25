using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    public Movements player;
    public Image[] cooldownCircles;

    void Update()
    {
        if (!player) return;

        for (int i = 0; i < cooldownCircles.Length; i++)
        {
            Charge c = player.GetCharge(i);

            if (c == null) continue;

            cooldownCircles[i].fillAmount = c.IsReady ? 1f : c.CooldownPercent;
        }
    }
}
