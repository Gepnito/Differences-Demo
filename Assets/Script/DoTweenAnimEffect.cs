using UnityEngine;
using DG.Tweening;
using System.Collections;
public class DoTweenAnimEffect : MonoBehaviour
{
    [Tooltip("����� ������� ����������� ��� ���������� �������")]
    public Options indexEffectDetect;
    [Tooltip("�������� ��������")]
    public float speed = 0.3f;
    [Tooltip("������� ���������� ��������")]
    public float scale = 1.3f;
    [Tooltip("�������� �������� ��������")]
    public float speedRotate = 0.3f;
    private Vector3 vecStart;
    private Vector3 vecEnd;
    public enum Options
    {
        EffectFlash,
        EffectFlash2,
        EffectRotate,
        EffectPulse
    }
    private void OnEnable()
    {
        vecStart = new Vector3(transform.localScale.x * 0.3f, transform.localScale.y * 0.3f, 1);
        vecEnd = transform.localScale;
        StartAnim();
    }
    public void StartAnim()
    {
        if (indexEffectDetect == Options.EffectFlash)
        {
            transform.localScale = vecStart; //������ �������
            transform.DOScale(vecEnd, speed).SetEase(Ease.Flash);
        }
        else if (indexEffectDetect == Options.EffectFlash2)
        {
            StartCoroutine(Effect_1()); //������ �������
        }
        else if (indexEffectDetect == Options.EffectRotate)
        {
            //������ �������
            transform.DOLocalRotate(new Vector3(0, 0, -360), 7).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative(true);
        }
        else if (indexEffectDetect == Options.EffectPulse) 
        {
            StartCoroutine(Effect_4()); //��������� �������
        }
    }
    IEnumerator Effect_1() 
    {
        transform.localScale = vecStart;
        transform.DOScale(new Vector3(vecEnd.x * scale, vecEnd.y * scale, 1), speed);
        yield return new WaitForSeconds(speed);
        transform.DOScale(vecEnd, speed).SetUpdate(true);
    }
    IEnumerator Effect_4()
    {
        var delay = new WaitForSecondsRealtime(0.8f);
        for (; ; )
        {
            transform.DOScale(scale, 1.0f).SetUpdate(true);
            yield return delay;
            transform.DOScale(Vector3.one, 1.0f).SetUpdate(true);
            yield return delay;
        }

    }
    void OnDestroy()
    {
        transform.DOKill();
    }

}




