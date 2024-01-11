using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Kart {
    public class NetworkStartUI : MonoBehaviour {
        [SerializeField] Button startHostButton;
        [SerializeField] Button startClientButton;
        [SerializeField] Button startRaceButton;
        
        void Start() {
            startHostButton.onClick.AddListener(StartHost);
            startClientButton.onClick.AddListener(StartClient);
        }
        
        void StartHost() {
            Debug.Log("Starting host");
            NetworkManager.Singleton.StartHost();
            //startRaceButton.gameObject.SetActive(true);
            Hide();
        }

        void StartClient() {
            Debug.Log("Starting client");
            NetworkManager.Singleton.StartClient();
            //startRaceButton.gameObject.SetActive(true);
            Hide();
        }

        void Hide() => gameObject.SetActive(false);
    }
}