using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : AttackMonster
{
    protected override void Update()
    {
        base.Update();
        if (theFieldOfViewAngle.Sight() && !isDead && !isAttacking)//좀비의 시야각 이내에 있고 죽지않는 상태이고 공격중이 아니라면
        {
            StopAllCoroutines(); //일단 실행중인 모든 코루틴을 중지하고
            StartCoroutine(CHASETARGETCO()); //추격코르틴을 시작함
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

        int _random = Random.Range(0, 3); // 대기, 소리치기, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Shout();
        else if (_random == 2)
            TryWalk();
    }


    private void Shout()  // 소리침
    {
        currentTime = waitTime;
        anim.SetTrigger("Shout");
    }
}
