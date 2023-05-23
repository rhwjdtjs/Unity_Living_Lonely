using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : AttackMonster
{
    protected override void Update()
    {
        base.Update();
        if (theFieldOfViewAngle.Sight() && !isDead && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(CHASETARGETCO());
        }
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
    private void Wait()  // 대기
    {
        currentTime = waitTime;
    }

    private void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 3); // 대기, 풀뜯기, 두리번, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Shout();
        else if (_random == 2)
            TryWalk();
    }


    private void Shout()  // 두리번
    {
        currentTime = waitTime;
        anim.SetTrigger("Shout");
    }
}
