using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //[wang]�G�t�F�N�g�w��
    public GameObject particleObject;
    public GameObject particleObject2;
    public GameObject particleObject3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star") //[wang] Star�^�O�̕t�����Q�[���I�u�W�F�N�g�ƏՓ˂���������
        {
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //[wang] �p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            Debug.Log("Star�G�t�F�N�g����");
        }
    }
}