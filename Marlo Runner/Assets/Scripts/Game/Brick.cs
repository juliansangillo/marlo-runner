using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private bool hasCoin = false;
    [SerializeField] private GameObject coinPrefab = null;

    private void OnKill() {

        Player player = playerObj.GetComponent<Player>();

        if(hasCoin)
            dropCoin(player);

        player.OnDestroyBrick();

    }

    private void dropCoin(Player player) {

        GameObject coinObj = GameObject.Instantiate(coinPrefab);
        coinObj.GetComponent<Collider>().enabled = false;
        coinObj.transform.position = transform.position + new Vector3(0, 0.7f, 0);

        Coin coin = coinObj.GetComponent<Coin>();
        coin.Vanish();

        player.OnCollectCoin();

    }

}
