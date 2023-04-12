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
