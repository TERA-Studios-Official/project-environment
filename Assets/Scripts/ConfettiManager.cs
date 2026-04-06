using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    GameObject obj;

    void Start()
    {
        obj = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Activate()
    {
        obj = gameObject;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.linearVelocityX = Random.Range(-5, 5);
        rb.linearVelocityY = 5;
    }
}
