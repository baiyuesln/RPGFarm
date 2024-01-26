
using UnityEngine;

//���ص�Player�ϣ�ʵ�ָ�������ĵ��뵭��
public class TriggerObscuringItemFader : MonoBehaviour
{
    //palyer���봥����ʱ������Ч��
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



    //palyer���봥����ʱ������Ч��
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
