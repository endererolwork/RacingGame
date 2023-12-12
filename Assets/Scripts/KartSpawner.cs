using System;
using Cinemachine;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Race
{
    public class KartSpawner : MonoBehaviour
    {
        [SerializeField] Circuit circuit;
        [SerializeField] AIDriverData _aiDriverData;
        [SerializeField] GameObject[] aiKartPrefabs;

        [SerializeField] GameObject playerKartPrefab;
        [SerializeField] CinemachineVirtualCamera playerCamera;

        private void Start()
        {
            var playerKart = Instantiate(playerKartPrefab, circuit.spawnPoints[0].position,
                circuit.spawnPoints[0].rotation);

            playerCamera.Follow = playerKart.transform;
            playerCamera.LookAt = playerKart.transform;

            //Spawn AI Cars
            for (int i = 1; i < circuit.spawnPoints.Length; i++)
            {
                GameObject aiPrefab = aiKartPrefabs[Random.Range(0, aiKartPrefabs.Length)];

                new AIKartBuilder(aiPrefab, circuit, _aiDriverData, circuit.spawnPoints[i])
                    .build();
            }
        }
    }

    public class AIKartBuilder
    {
        private GameObject prefab;
        private AIDriverData data;
        private Circuit circuit;
        private Transform spawnPoint;

        public AIKartBuilder(GameObject prefab, Circuit circuit, AIDriverData data, Transform spawnPoint)
        {
            this.prefab = prefab;
            this.circuit = circuit;
            this.data = data;
            this.spawnPoint = spawnPoint;
        }

        public GameObject build()
        {
            var instance = Object.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            var aiInput = instance.GetOrAdd<AIInput>();

            aiInput.AddCircuit(circuit);
            aiInput.AddDriverData(data);
            instance.GetComponent<CarController>().SetInput(aiInput);

            return instance;
        }
    }
}