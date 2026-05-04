using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public RectTransform panel1;
    public RectTransform panel2;

    public float duracion = 2.5f;
    public float duracionMov = 1.5f;

    Vector2 izq;
    Vector2 centro = Vector2.zero;

    void Start()
    {
        RectTransform canvas = panel1.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        izq = new Vector2(-canvas.rect.width, 0);

        StartCoroutine(Sequence());
    }
    IEnumerator Sequence()
    {
        panel1.gameObject.SetActive(true);
        panel2.gameObject.SetActive(true);
        
        panel1.anchoredPosition = izq;
        panel2.anchoredPosition = izq;

        yield return StartCoroutine(PlayPanel(panel1));
        yield return StartCoroutine(PlayPanel(panel2));
    }
    IEnumerator PlayPanel(RectTransform panel)
    {
        panel.anchoredPosition = izq;
        yield return StartCoroutine(MovePanel(panel, centro));
        
        yield return new WaitForSeconds(duracion);
        
        panel.anchoredPosition = centro;
        yield return StartCoroutine(MovePanel(panel, izq));
    }
    IEnumerator MovePanel(RectTransform panel, Vector2 targetPos)
    {
        Vector2 startPos = panel.anchoredPosition;
        float tiempo = 0;

        while (tiempo < duracionMov)
        {
            tiempo += Time.deltaTime;
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, tiempo / duracionMov);
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }
}