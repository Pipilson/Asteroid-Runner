using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour
{
    public Rigidbody2D rdb;
    public SpriteRenderer sprnd1;
    public GameObject explosionPrefab;
    public ParticleSystem weapon;
    public AudioSource fireAS, fuelR, fuelY, playerEx, itemAS, boostAS;
    public float enginePower = 2;
    bool trust, boost = false;
    float turn;
    bool fire;

    [SerializeField]
    float fuel = 1;
    [SerializeField]
    Text fuelTexto;

    void Update()
    {
        bool fireSom = false, fuelAlarmY = false, fuelAlarmR = false;

        trust = Input.GetButton("Vertical");
        turn = -Input.GetAxis("Horizontal");
        fire = Input.GetButtonDown("Jump");

        if (fire)
        {
            weapon.Emit(1);
            fireSom = true;

            var main = weapon.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }

        if (fireSom)
        {
            fireAS.Play();
        }

        if (fuel >= 0.8f)
        {
            fuelAlarmY = false;
            fuelAlarmR = false;
            fuelY.Stop();
            fuelR.Stop();
            fuelTexto.color = Color.green;
            fuelTexto.text = "Fuel: ■■■■■";
        }

        if (fuel >= 0.6f && fuel < 0.8f)
        {
            fuelAlarmY = true;
            fuelTexto.color = Color.green;
            fuelTexto.text = "Fuel: ■■■■";
        }

        if (fuel >= 0.4f && fuel < 0.6f)
        {
            fuelTexto.color = Color.yellow;
            fuelTexto.text = "Fuel: ■■■";
        }

        if (fuel >= 0.2f && fuel < 0.4f)
        {
            fuelAlarmR = true;
            fuelTexto.color = Color.yellow;
            fuelTexto.text = "Fuel: ■■";
        }

        if (fuel != 0 && fuel < 0.2f)
        {
            fuelAlarmY = false;
            fuelY.Stop();
            fuelTexto.color = Color.red;
            fuelTexto.text = "Fuel: ■";
        }

        if (fuel <= 0)
        {
            fuelTexto.color = Color.red;
            fuelTexto.text = "Fuel:";
        }

        if (fuelAlarmY)
        {
            fuelY.Play();
        }

        if (fuelAlarmR)
        {
            fuelR.Play();
        }
    }

    private void FixedUpdate()
    {
        if (trust && fuel > 0)
        {
            fuel -= 0.0005f;
            rdb.AddRelativeForce(Vector3.up * enginePower);
            sprnd1.enabled = true;
            boost = true;
        }

        else
        {
            sprnd1.enabled = false;
        }

        if (!trust)
        {
            boost = false;
        }

        if (!boost)
        {
            boostAS.Play();
        }

        if (transform.position.y > 22)
        {
            Vector3 newPos = transform.position;
            newPos.y = -22;
            transform.position = newPos;
        }

        if (transform.position.y < -22)
        {
            Vector3 newPos = transform.position;
            newPos.y = 22;
            transform.position = newPos;
        }

        if (transform.position.x < -44)
        {
            Vector3 newPos = transform.position;
            newPos.x = 44;
            transform.position = newPos;
        }

        if (transform.position.x > 44)
        {
            Vector3 newPos = transform.position;
            newPos.x = -44;
            transform.position = newPos;
        }

        rdb.AddTorque(turn);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.Spawn();

            if (collision.collider.CompareTag("Rock"))
            {
                Destroy(collision.gameObject);
            }

            playerEx.Play();
            rdb.velocity = new Vector2(0, 0);
            rdb.transform.rotation = new Quaternion(0, 0, 0, 0);
            rdb.drag = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fuel"))
        {
            fuel = 1;
            Destroy(col.gameObject);
        }

        if (col.CompareTag("Vida"))
        {
            GameManager.Instance.SetLife();
            Destroy(col.gameObject);
        }

        if (col.CompareTag("Shield"))
        {
            GameManager.Instance.SetShield();
            Destroy(col.gameObject);
        }

        itemAS.Play();
    }
}