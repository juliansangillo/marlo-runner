using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public bool hasCoin;
    public GameObject coinPrefab;

    void OnKill() {

        Player player = GameObject.Find("Player").GetComponent<Player>();

        if(hasCoin) {
            GameObject coinObj = GameObject.Instantiate(coinPrefab);
            coinObj.transform.position = transform.position + new Vector3(0, 0.7f, 0);

            Coin coin = coinObj.GetComponent<Coin>();
            coin.Vanish();

            player.onCollectCoin();
        }

        player.OnDestroyBrick();
    }

}
