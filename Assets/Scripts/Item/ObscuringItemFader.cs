using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //物体alpha逐渐变小为Setting.targetAlpha
    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    //物体alpha逐渐回复1
    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    //淡出效果的协程实现
    private IEnumerator FadeOutRoutine()
    {
        float currentAlpha = spriteRenderer.color.a;
        float distance = currentAlpha - Settings.targetAlpha;

        while(currentAlpha - Settings.targetAlpha>0.01f)
        {
            currentAlpha = currentAlpha - distance/Settings.fadeOutSeconds*Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }

    //淡入效果的协程实现
    private IEnumerator FadeInRoutine()
    {
        float currentAlpha = spriteRenderer.color.a;
        float distance = 1f - currentAlpha;

        while(1f - currentAlpha > 0.01f)
        {
            currentAlpha = currentAlpha + distance / Settings.fadeInSeconds;
            spriteRenderer.color = new Color(1f,1f,1f,currentAlpha);
            yield return null;
        }
        spriteRenderer.color = new Color(1f,1f,1f,1f);
    }
}
