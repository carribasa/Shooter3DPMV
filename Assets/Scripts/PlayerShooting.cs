using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera playerCamera;
    public float shootingRange = 100f;
    public Animator animator;
    public LayerMask playerLayer;  // Asigna la capa del jugador en el Inspector
    public GameObject aim;         // Asigna el GameObject del puntero en el Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        animator.SetBool("isShooting", false);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootingRange))
        {
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                aim.SetActive(false);
            }
            else
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material newMaterial = new Material(renderer.material);
                    newMaterial.color = Color.black;
                    renderer.material = newMaterial;
                }
            }
        }
    }
}
