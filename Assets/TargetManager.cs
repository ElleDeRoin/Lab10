using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] int enemyTypeLimit;
    [SerializeField] private GameObject[] targetTypes;
    [SerializeField] private GameObject fastTargetsContainer;
    [SerializeField] private GameObject largeTargetsContainer;
    [SerializeField] private GameObject valuableTargetsContainer;

    private List<TargetObject> fastTargets;
    private List<TargetObject> largeTargets;
    private List<TargetObject> valuableTargets;

    public static TargetManager Instance;

    private Shop shop;

    [SerializeField] private float spacing = 2f;
    [SerializeField] private float startX = -10f;
    [SerializeField] private float endX = 10f;

    [SerializeField] private PlayerMovement player;

    public GameObject GetFastTargetPrefab()
    {
        return targetTypes[0];
    }

    public GameObject GetLargeTargetPrefab()
    {
        return targetTypes[1];
    }

    public GameObject GetValuableTargetPrefab()
    {
        return targetTypes[2];
    }

    private void OnEnable()
    {
        // Listens to bullet hit event
        Bullet.OnHitTarget += DestroyTarget;
    }

    private void OnDisable()
    {
        Bullet.OnHitTarget -= DestroyTarget;
    }


    // Start is called before the first frame update
    void Start()
    {
        shop = new Shop();

        SpawnFastEnemies(enemyTypeLimit);
        SpawnLargeEnemies(enemyTypeLimit);
        SpawnValuableEnemies(enemyTypeLimit);
    }

    private void Update()
    {
        MoveTargets(fastTargets);
        MoveTargets(largeTargets);
        MoveTargets(valuableTargets);
    }


    public void SpawnFastEnemies(int limit)
    {
        fastTargets = new List<TargetObject>();

        for (int i = 0; i < limit; i++)
        {
            
            // build the fast target
            TargetFast fastBuilder = new TargetFast();
            shop.Construct(fastBuilder);
            TargetObject target = fastBuilder.InstantiateFastTarget();

            // spawn it in the scene
            Vector3 spawnPos = new Vector3(startX + (i * spacing), fastTargetsContainer.transform.position.y, fastTargetsContainer.transform.position.z);
            
            var targetObject = Instantiate(target.targetPrefab, spawnPos, Quaternion.identity);
            target.targetPrefab = targetObject;
            fastTargets.Add(target);
        }
    }

    public void SpawnLargeEnemies(int limit)
    {
        largeTargets = new List<TargetObject>();

        for (int i = 0; i < limit; i++)
        {

            // build the fast target
            TargetLarge largeBuilder = new TargetLarge();
            shop.Construct(largeBuilder);
            TargetObject target = largeBuilder.InstantiateLargeTarget();

            // spawn it in the scene
            Vector3 spawnPos = new Vector3(startX + (i * spacing * 1.5f), largeTargetsContainer.transform.position.y, largeTargetsContainer.transform.position.z);

            var targetObject = Instantiate(target.targetPrefab, spawnPos, Quaternion.Euler(0f, 0f, 90f));
            target.targetPrefab = targetObject;
            largeTargets.Add(target);
        }
    }

    public void SpawnValuableEnemies(int limit)
    {
        valuableTargets = new List<TargetObject>();

        for (int i = 0; i < limit; i++)
        {

            // build the valuable target
            TargetValuable valuableBuilder = new TargetValuable();
            shop.Construct(valuableBuilder);
            TargetObject target = valuableBuilder.InstantiateValuableTarget();

            // spawn it in the scene
            Vector3 spawnPos = new Vector3(startX + (i * spacing), valuableTargetsContainer.transform.position.y, valuableTargetsContainer.transform.position.z);

            var targetObject = Instantiate(target.targetPrefab, spawnPos, Quaternion.identity);
            target.targetPrefab = targetObject;
            valuableTargets.Add(target);
        }
    }

    public void MoveTargets(List<TargetObject> targets)
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i].targetPrefab == null)
            {
                targets.RemoveAt(i);
            }
            else
            {
                //Debug.Log("Target Speed is: " + targets[i].speed);
                if (targets[i].targetPrefab.name.Equals("LargeTarget(Clone)"))
                {
                    targets[i].targetPrefab.transform.Translate(Vector3.down * targets[i].speed * Time.deltaTime);
                }
                else
                {
                    targets[i].targetPrefab.transform.Translate(Vector3.right * targets[i].speed * Time.deltaTime);
                }

                if (targets[i].targetPrefab.transform.position.x > endX)
                {
                    targets[i].targetPrefab.transform.position = new Vector3(startX, targets[i].targetPrefab.transform.position.y, targets[i].targetPrefab.transform.position.z);
                }
            }
        }
    }

    // destroys the invoked target
    private void DestroyTarget(GameObject target)
    {

        switch (target.name)
        {
            case "FastTarget(Clone)":
                player.points += 7;
                break;
            case "LargeTarget(Clone)":
                player.points += 3;
                break;
            case "ValuableTarget(Clone)":
                player.points += 15;
                break;
            default:
                Debug.Log("what...");
                break;
        }
        Destroy(target);
    }
}
