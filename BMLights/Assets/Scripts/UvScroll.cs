using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvScroll : MonoBehaviour
{
    public Material TextureToScroll;
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 1.0f;
    private Vector2 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = TextureToScroll.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        currentOffset += direction * speed * Time.deltaTime;
        TextureToScroll.SetTextureOffset("_MainTex", currentOffset);
    }
}
