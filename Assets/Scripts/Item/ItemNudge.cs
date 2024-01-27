using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于在物体与player发生碰撞时旋转的脚本
public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds pause;
    private bool isAnimating = false;

    private void Awake()
    {
        // 设置 WaitForSeconds 对象，用于暂停指定时间
        pause = new WaitForSeconds(0.04f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAnimating == false)
        {
            // 根据物体位置选择旋转方向，并开始相应方向的旋转动画
            if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAnimating == false)
        {
            if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    //逐渐向左倾斜，然后回弹动画
    private IEnumerator RotateAntiClock()
    {
        isAnimating = true;

        for(int i = 0; i < 4; i++) 
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }

        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);

        yield return pause;

        isAnimating = false;
    }

    //逐渐向右倾斜，然后回弹动画
    private IEnumerator RotateClock()
    {
        isAnimating = true;

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }

        gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);

        yield return pause;

        isAnimating = false;
    }
}
