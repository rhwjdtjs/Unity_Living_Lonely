using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : AttackMonster
{
    protected override void Update()
    {
        base.Update();

        // 시야에 플레이어가 있고, 죽지 않았으며, 공격 중이 아닐 때 추격을 시작합니다.
        if (theFieldOfViewAngle.Sight() && !isDead && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(CHASETARGETCO());
        }

        // 생존 시간이 361초에서 610초 사이일 때
        if (TotalGameManager.survivaltimesecond >= 361 && TotalGameManager.survivaltimesecond <= 610)
        {
            if (theday.isNight)
            {
                walkSpeed = 7f;
                runSpeed = 9f;
                attackDamage = 20;
            }
            else
            {
                walkSpeed = 5f;
                runSpeed = 8f;
                attackDamage = 15;
            }
        }

        // 생존 시간이 611초 이상일 때
        if (TotalGameManager.survivaltimesecond >= 611)
        {
            if (theday.isNight)
            {
                walkSpeed = 7f;
                runSpeed = 9f;
                attackDamage = 30;
            }
            else
            {
                walkSpeed = 5f;
                runSpeed = 8f;
                attackDamage = 25;
            }
        }
    }

    protected override void RESETACTION()
    {
        base.RESETACTION();
        RandomAction();
    }

    private void Wait() // 대기
    {
        currentTime = waitTime;
    }

    private void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 3); // 대기, 소리치기,걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Shout();
        else if (_random == 2)
            TryWalk();
    }

    private void Shout() // 소리치기
    {
        currentTime = waitTime;
        anim.SetTrigger("Shout");
    }
}
