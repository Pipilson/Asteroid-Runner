using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogoAnimacao : MonoBehaviour
{
    public SpriteRenderer spriterenderer;
    public Sprite[] fogo;
    float spritecount = 0;

    void Update()
    {
        spritecount += Time.deltaTime * 8;

        if (spritecount >= fogo.Length)
            spritecount = 0;

        spriterenderer.sprite = fogo[(int)spritecount];
    }
}