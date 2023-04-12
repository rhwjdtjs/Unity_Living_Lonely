using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunTextEditor : MonoBehaviour
{
    private Gun thegun;
    [SerializeField] Text pistol1;
    [SerializeField] Text pistol2;
    [SerializeField] Text rifle1;
    [SerializeField] Text rifle2;
    [SerializeField] Text rifle3;
    [SerializeField] Text tommygun;
    [SerializeField] Text Shotgun;
    // Start is called before the first frame update
    void Start()
    {
        thegun = FindObjectOfType<Gun>();
    }
    private void gunammoappear()
    {
        pistol1.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        pistol2.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        rifle1.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        rifle2.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        rifle3.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        tommygun.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
        Shotgun.text = thegun.currentBulletCount + "/" + thegun.carryBulletCount;
    }
    // Update is called once per frame
    void Update()
    {
        gunammoappear();
    }
}
