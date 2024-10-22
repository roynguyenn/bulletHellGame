using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShakeHealth : MonoBehaviour
{
    public HungerBar healthBar;

    public float durationShake = 0.25f;
    Vector3 originalLocalPosition;
    public GameObject blackHeart;
    void Start()
    {
        transform.localPosition = new Vector3(-229.72f, 198.7f, 0f);
        originalLocalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.CurrentHunger <= 20)
        {
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationShake)
        {
            float xOffSet = Random.Range(-1f, 1f) * 7.5f;
            float yOffSet = Random.Range(-1f, 1f) * 7.5f;

            // Shake the red bar relative to the black heart using localPosition
            transform.localPosition = Vector3.Lerp(
                originalLocalPosition, 
                originalLocalPosition + new Vector3(xOffSet, yOffSet, 0), 
                0.5f
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the position of the red bar to its original local position relative to the black heart
        transform.localPosition = originalLocalPosition;
    }
}
