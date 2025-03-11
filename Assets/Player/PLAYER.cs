using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float focaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;
    public bool Jump = false; // Novo parâmetro para pular

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    void Update()
    {
        andando = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = true;
            Debug.Log("LeftArrow");

            if (noChao)
            {
                andando = true;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            _spriteRenderer.flipX = false;
            Debug.Log("RightArrow");

            if (noChao)
            {
                andando = true;
            }
        }

        // Pular ao pressionar espaço ou se Jump estiver verdadeiro
        if ((Input.GetKeyDown(KeyCode.Space) || Jump) && noChao)
        {
            _rigidbody2D.AddForce(Vector2.up * focaPulo, ForceMode2D.Impulse);
            Debug.Log("Jump");
            Jump = false; // Reseta o parâmetro após o pulo
        }

        _animator.SetBool("Andando", andando);
    }
}
