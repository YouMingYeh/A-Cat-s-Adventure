using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AnimatedImageUI : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;
    public bool loop = false;

    private void Start()
    {
        Func_PlayUIAnim();
    }
    public void Func_PlayUIAnim()
    {
        IsDone = false;
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
    public void Func_StopUIAnim()
    {
        IsDone = true;
        StopCoroutine(m_CorotineAnim);
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            if (loop)
            {
                m_IndexSprite = 0;
            }
            else
            {
                Func_StopUIAnim();
                m_IndexSprite = m_SpriteArray.Length - 1;
            }
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_Image.rectTransform.sizeDelta = new Vector2(m_Image.sprite.rect.width, m_Image.sprite.rect.height);
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());

    }
}
