using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[ogawa]�ʏ펞�̈ړ����x���w�肵�܂��B
    [SerializeField]
    private float walkSpeed = 4;
    //[ogawa]���݂̈ړ����x
    float speed = 4;

    //[ogawa]�R���|�[�l���g�����O�ɎQ�Ƃ���ϐ�
    Animator animator;
    //[ogawa]Animator�̃p�����[�^�[ID
    static readonly int walkId = Animator.StringToHash("Walk");

    // [ogawa]�G�̏�Ԃ�\���܂��B
    enum EnemyState
    {
        // [ogawa]�ʏ�̑��s���
        Walk,
    }
    //[ogawa] ���݂̓G�̏��
    [SerializeField]
    EnemyState currentState = EnemyState.Walk;

    // Start is called before the first frame update
    void Start()
    {
        //[ogawa]�R���|�[�l���g�̎擾
        animator = GetComponent<Animator>();
    }

    //[ogawa] Walk�X�e�[�g�ɑJ�ڂ����܂��B
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
