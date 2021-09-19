using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer; 
    public int nextMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if(rayHit.collider == null){
           Turn();
        }
    }

    void Think(){
        nextMove = Random.Range(-1, 2);

        anim.SetInteger("walkSpeed", nextMove);
        if(nextMove != 0){
           spriteRenderer.flipX = nextMove == 1;
        }
        
        float thinkMoveTime = Random.Range(2f, 5f);
        Invoke("Think", thinkMoveTime);   
    }

    void Turn(){
        nextMove = nextMove * -1; 
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
