using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnouncerBox : MonoBehaviour
{
    public static AnnouncerBox current;
    public TextMeshProUGUI announcerText;
    public float displayTime = 3f;
    public float zoomTime = 0.5f;
    private float m_timer = 0f;
    private bool m_isDisplaying = false;
    // Start is called before the first frame update

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        announcerText = announcerText != null ? announcerText : GetComponentInChildren<TextMeshProUGUI>();
        transform.localScale = Vector3.forward;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText(string text)
    {
        announcerText.text = text;
        m_timer = displayTime;
        if (!enabled)
        {
            
            StartCoroutine(DisplayCoroutine());
        }
    }
    private IEnumerator DisplayCoroutine()
    {
        enabled = true;
        yield return StartCoroutine(ZoomInCoroutine());
        while(m_timer > 0f)
        {
            m_timer -= Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(ZoomOutCoroutine());

        enabled = false;
    }
    private IEnumerator ZoomInCoroutine()
    {
        float timer = zoomTime;
        while (timer > 0f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, (zoomTime-timer)/zoomTime);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ZoomOutCoroutine()
    {
        float timer = zoomTime;
        while (timer > 0f)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, (zoomTime-timer)/zoomTime);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

}
