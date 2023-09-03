using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public float trashRange = 5.0f;
    public int score = 0;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, trashRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Critter"))
            {
                GameObject critter = collider.gameObject;
                critter.SetActive(false);
                score += 10;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trashRange);
    }
}
