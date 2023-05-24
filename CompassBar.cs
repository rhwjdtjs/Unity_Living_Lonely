using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompassBar : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform ������Ʈ
    public Transform northReference; // ���� ���� ���͸� ����Ű�� Transform ������Ʈ

    public RectTransform compassBar; // ���� ��ħ�� �̹����� RectTransform
    public Text southText; // ���� �ؽ�Ʈ
    public Text westText; // ���� �ؽ�Ʈ
    public Text eastText; // ���� �ؽ�Ʈ
    public Text northText; // ���� �ؽ�Ʈ

    public float textRotationRange = 45f; // �ؽ�Ʈ�� ȸ�� ����

    void Update()
    {
        // �÷��̾��� ���� ���� ���
        Vector3 playerDirection = player.forward;
        playerDirection.y = 0f; // Y �� ȸ�� ���� (2D�� ����)

        // ���� ���� ���� ���
        Vector3 northDirection = northReference.position - player.position;
        northDirection.y = 0f; // Y �� ȸ�� ���� (2D�� ����)

        // �÷��̾� ����� ���� ���� ���� ������ ���� ���
        float angle = Vector3.SignedAngle(northDirection, playerDirection, Vector3.up);

        // ���� ��ħ�� �̹����� ȸ���� �ʱ�ȭ
        compassBar.localRotation = Quaternion.identity;

        // ���� �ؽ�Ʈ ������Ʈ
        UpdateDirectionText(angle);
    }

    void UpdateDirectionText(float angle)
    {
        float textRotationRatio = Mathf.Clamp(angle / textRotationRange, -1f, 1f);

        // ���� �ؽ�Ʈ ������Ʈ
        if (angle >= -textRotationRange && angle < textRotationRange)
        {
            northText.gameObject.SetActive(true);
            southText.gameObject.SetActive(false);
            westText.gameObject.SetActive(false);
            eastText.gameObject.SetActive(false);

          //  northText.rectTransform.anchoredPosition = Vector2.zero;
        }
        else if (angle >= textRotationRange && angle < 180f - textRotationRange)
        {
            northText.gameObject.SetActive(false);
            southText.gameObject.SetActive(false);
            westText.gameObject.SetActive(false);
            eastText.gameObject.SetActive(true);

            float offsetX = Mathf.Lerp(0f, compassBar.rect.width / 2f, textRotationRatio);
         //   eastText.rectTransform.anchoredPosition = new Vector2(0f, 0f);
        }
        else if (angle >= 180f - textRotationRange || angle < -180f + textRotationRange)
        {
            northText.gameObject.SetActive(false);
            southText.gameObject.SetActive(true);
            westText.gameObject.SetActive(false);
            eastText.gameObject.SetActive(false);

           // southText.rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            northText.gameObject.SetActive(false);
            southText.gameObject.SetActive(false);
            westText.gameObject.SetActive(true);
            eastText.gameObject.SetActive(false);

            float offsetX = Mathf.Lerp(compassBar.rect.width / 2f, 0f, Mathf.Abs(textRotationRatio));
          //  westText.rectTransform.anchoredPosition = new Vector2(offsetX, 0f);
        }
    }
}
