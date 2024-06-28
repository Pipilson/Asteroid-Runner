using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosionPrefab, life, shield, fuel;

    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.AsteroidSom();

        System.Random rnd = new System.Random();
        int item = rnd.Next(0, 11);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (item >= 3 && item <= 4)
        {
            Instantiate(shield, transform.position, Quaternion.identity);
        }

        if (item >= 5 && item <= 8)
        {
            Instantiate(fuel, transform.position, Quaternion.identity);
        }

        if (item >= 9)
        {
            Instantiate(life, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        GameManager.Instance.SetScore();
    }
}