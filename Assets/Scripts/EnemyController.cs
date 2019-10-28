using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool vertical = true;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody;
    float timer;
    int direction = 1;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody.position;
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody.MovePosition(position);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null && player.health > 0)
        {
             player.ChangeHealth(-1);
        }
    }
}
