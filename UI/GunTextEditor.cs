using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTextEditor : MonoBehaviour
{
    private Gun theGun; // Gun 스크립트 참조
    [SerializeField] private Text pistol1; // 피스톨 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text pistol2; // 피스톨 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text rifle1; // 라이플 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text rifle2; // 라이플 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text rifle3; // 라이플 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text tommygun; // 토미건 탄약을 표시하는 텍스트 요소
    [SerializeField] private Text shotgun; // 샷건 탄약을 표시하는 텍스트 요소

    // Start is called before the first frame update
    void Start()
    {
        theGun = FindObjectOfType<Gun>(); // Gun 스크립트를 찾아서 할당합니다.
    }

    // 총기 탄약 정보를 표시합니다.
    private void UpdateGunAmmo()
    {
        pistol1.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 피스톨 탄약 정보 업데이트
        pistol2.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 피스톨 탄약 정보 업데이트
        rifle1.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 라이플 탄약 정보 업데이트
        rifle2.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 라이플 탄약 정보 업데이트
        rifle3.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 라이플 탄약 정보 업데이트
        tommygun.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 토미건 탄약 정보 업데이트
        shotgun.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // 샷건 탄약 정보 업데이트
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunAmmo(); // 총기 탄약 정보 업데이트
    }
}
