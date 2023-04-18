using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : AttackMonster
{
    protected override void Update()
    {
        base.Update();
        if (theFieldOfViewAngle.Sight() && !isDead && !isAttacking)//������ �þ߰� �̳��� �ְ� �����ʴ� �����̰� �������� �ƴ϶��
        {
            StopAllCoroutines(); //�ϴ� �������� ��� �ڷ�ƾ�� �����ϰ�
            StartCoroutine(CHASETARGETCO()); //�߰��ڸ�ƾ�� ������
        }
    }
    protected override void RESETACTION()
    {
        base.RESETACTION();
        RandomAction();
    }
    private void Wait()  // ���
    {
        currentTime = waitTime;
    }

    private void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 3); // ���, �Ҹ�ġ��, �ȱ�

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Shout();
        else if (_random == 2)
            TryWalk();
    }


    private void Shout()  // �Ҹ�ħ
    {
        currentTime = waitTime;
        anim.SetTrigger("Shout");
    }
}
