using UnityEngine;

public class ToadInteraction : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("isPlayerNear");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
