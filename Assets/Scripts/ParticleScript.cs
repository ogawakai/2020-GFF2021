using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //[wang]エフェクト指定
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
        if (collision.gameObject.tag == "Star") //[wang] Starタグの付いたゲームオブジェクトと衝突したか判別
        {
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //[wang] パーティクル用ゲームオブジェクト生成
            Debug.Log("Starエフェクト発生");
        }
    }
}