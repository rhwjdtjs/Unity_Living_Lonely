using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompassBar : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform 컴포넌트
    public Transform northReference; // 일자 기준 벡터를 가리키는 Transform 컴포넌트

    public RectTransform compassBar; // 일자 나침반 이미지의 RectTransform
    public Text southText; // 남쪽 텍스트
    public Text westText; // 서쪽 텍스트
    public Text eastText; // 동쪽 텍스트
    public Text northText; // 북쪽 텍스트

    public float textRotationRange = 45f; // 텍스트의 회전 범위

    void Update()
    {
        // 플레이어의 방향 벡터 계산
        Vector3 playerDirection = player.forward;
        playerDirection.y = 0f; // Y 축 회전 제한 (2D로 가정)

        // 일자 기준 벡터 계산
        Vector3 northDirection = northReference.position - player.position;
        northDirection.y = 0f; // Y 축 회전 제한 (2D로 가정)

        // 플레이어 방향과 일자 기준 벡터 사이의 각도 계산
        float angle = Vector3.SignedAngle(northDirection, playerDirection, Vector3.up);

        // 일자 나침반 이미지의 회전을 초기화
        compassBar.localRotation = Quaternion.identity;

        // 방향 텍스트 업데이트
        UpdateDirectionText(angle);
    }

    void UpdateDirectionText(float angle)
    {
        float textRotationRatio = Mathf.Clamp(angle / textRotationRange, -1f, 1f);

        // 방향 텍스트 업데이트
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
