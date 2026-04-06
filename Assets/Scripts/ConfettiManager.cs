using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    GameObject obj;

    void Start()
    {
        obj = gameObject;
    }
    public void Activate()
    {
        obj = gameObject;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        rb.WakeUp();
        rb.linearVelocityX = Random.Range(-5, 5);
        rb.linearVelocityY = 5;
    }
}
