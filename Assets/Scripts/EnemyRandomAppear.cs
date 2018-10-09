﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomAppear : MonoBehaviour {
    //敵の出現座標リスト
    [SerializeField] private List<Transform> EnemyPosition;
    //出現させる隊の敵のリスト
    [SerializeField] private List<GameObject> Enemy;
    //出現タイマー
    private float timer = 0.0f;
    //出現間隔
    [SerializeField] private float time = 3;
    //第１フェーズ終了までの時間
    [SerializeField] private const float ferse1Time = 20;
    //第2フェーズでの敵出現間隔減少量
    [SerializeField] private float timer_Dec = 0.2f;
    //第2フェーズでの敵出現最短間隔
    [SerializeField] private float min_AppearTime = 0.1f;
    //現在の敵の数
    private int enemyNum = 0;
    //敵の最大数
    [SerializeField] private int maxEnemyNum = 100;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FindEnemyNum();
        if(enemyNum <= maxEnemyNum )
        {
            RandomAppear();
        }
    }

    private void RandomAppear()
    {
        //タイマー
        timer += Time.deltaTime;
        //タイマーが第1フェーズの時間内なら
        if (timer <= ferse1Time)
        {
            //敵を出現させるタイミングか判断
            if (timer % time <= 0.016f)
            {
                //敵を出現させる
                //第１フェーズでは体力の少ない敵2種類から生成
                Instantiate(Enemy[Random.Range(0, 10) % 2], EnemyPosition[Random.Range(0, EnemyPosition.Count)].position, transform.rotation);
                //カウンタを加算
                enemyNum++;
            }
        }
        //第2フェーズへ
        else
        {
            //敵を出現させるタイミングか判断
            if (timer % time <= 0.016f)
            {
                //敵を出現させる
                //第2フェーズでは体力も多い敵も含めて4種類から生成
                Instantiate(Enemy[Random.Range(0, Enemy.Count)], EnemyPosition[Random.Range(0, EnemyPosition.Count)].position, transform.rotation);
                //少しずつ出現間隔を短くする
                time -= timer_Dec;
                //出現の最短間隔以下なら
                if (time <= min_AppearTime)
                {
                    //最短間隔に指定
                    time = min_AppearTime;
                }
                //カウンタを加算
                enemyNum++;
            }
        }
    }

    //ほかのスクリプトから現在の敵の数を1減らす
    public void Destroy_Enemy()
    {
        
        //1減らす
        enemyNum--;
    }
    //現在出現してる敵の数を数える
    private void FindEnemyNum()
    {
        GameObject[] enemyNum_ = GameObject.FindGameObjectsWithTag("Enemy");
        enemyNum = enemyNum_.Length;
    }
}
