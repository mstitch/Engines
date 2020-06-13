using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS : MonoBehaviour
{
    public float velocidadeMov;
    public float deslocamentoAltura;
    public LayerMask camadaChao;
    public Animator anim;


    Transform tr;
    Rigidbody rb;
    Transform trCam;

    SistemaSom sistemaDeSom;

    void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        trCam = GameObject.FindWithTag("Tripe").GetComponent<Transform> ();
        sistemaDeSom = GameObject.FindWithTag("MainCamera").GetComponent<SistemaSom>();

    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        bool apertouAtaque = Input.GetButtonDown("Fire1");
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        if (apertouAtaque) { anim.SetTrigger("atacou");
            sistemaDeSom.Emitir(SistemaSom.EfeitoSonoro.Golpe);
        }

        Vector3 mov = new Vector3(movH, 0, movV);

        tr.LookAt(tr.position + trCam.TransformDirection(mov) * 5);

        if (mov.magnitude > 1f)
            mov.Normalize();

        tr.Translate(0, 0, mov.magnitude * velocidadeMov * Time.deltaTime);

        anim.SetFloat("velocidade", mov.magnitude);
        //anim.SetBool("estaNoChao", estaNoChao);

        RaycastHit hit;
        bool rcBateyBiChao = Physics.Raycast(
            tr.position,
            Vector3.down,
            out hit,
            Mathf.Infinity,
            camadaChao);

        if (rcBateyBiChao)
        {
           Vector3 pos = tr.position;
            pos.y = hit.point.y + deslocamentoAltura;
            tr.position = pos;
        }
    }


}
   
