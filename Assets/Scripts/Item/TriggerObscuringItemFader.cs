
using UnityEngine;

//挂载到Player上，实现覆盖物体的淡入淡出
public class TriggerObscuringItemFader : MonoBehaviour
{
    //palyer进入触发器时，淡出效果
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        if(obscuringItemFader.Length > 0)
        {
            for(int i = 0;i<obscuringItemFader.Length;i++)
            {
                obscuringItemFader[i].FadeOut();
            }
        }
    }



    //palyer进入触发器时，淡出效果
    private void OnTriggerExit2D(Collider2D collision)
    {
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        if (obscuringItemFader.Length > 0)
        {
            for (int i = 0; i < obscuringItemFader.Length; i++)
            {
                obscuringItemFader[i].FadeIn();
            }
        }
    }

}
