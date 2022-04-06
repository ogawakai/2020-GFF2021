using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[ogawa]通常時の移動速度を指定します。
    [SerializeField]
    private float walkSpeed = 4;
    //[ogawa]現在の移動速度
    float speed = 4;

    //[ogawa]コンポーネントを事前に参照する変数
    Animator animator;
    //[ogawa]AnimatorのパラメーターID
    static readonly int walkId = Animator.StringToHash("Walk");

    // [ogawa]敵の状態を表します。
    enum EnemyState
    {
        // [ogawa]通常の走行状態
        Walk,
    }
    //[ogawa] 現在の敵の状態
    [SerializeField]
    EnemyState currentState = EnemyState.Walk;

    // Start is called before the first frame update
    void Start()
    {
        //[ogawa]コンポーネントの取得
        animator = GetComponent<Animator>();
    }

    //[ogawa] Walkステートに遷移させます。
    private void SetWalkState()
    {
        animator.SetTrigger(walkId);
        speed = walkSpeed;
        currentState = EnemyState.Walk;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
