using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float m_ForceModifier = 5000f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Explode();
            player.Collision(m_ForceModifier);
        }
    }

    private void Explode()
    {
        GetComponent<Animator>().SetBool("Exploded", true);
    }
}
