using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public bool hasCoin;
    public GameObject coinPrefab;

    void OnDestroy() {
        if(hasCoin) {
            GameObject coinObj = GameObject.Instantiate(coinPrefab);
            coinObj.transform.position = transform.position + new Vector3(0, 0.7f, 0);

            Coin coin = coinObj.GetComponent<Coin>();
            coin.Vanish();

            GameObject.Find("Player").GetComponent<Player>().onCollectCoin();
        }
    }

}
