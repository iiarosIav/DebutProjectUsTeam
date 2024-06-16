using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    public int FishingVector;
    public float FishingTarget = 2f;
    public bool CanBeFished;

    [SerializeField] private Transform _fishTransform;
    [SerializeField] private ParticleSystem _fishParticleSystem;

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.3f);
        int[] fishingVectors = { -1, 1 };
        FishingVector = fishingVectors[Random.Range(0, fishingVectors.Length)];
        _fishTransform.localEulerAngles = new Vector3(0, 90 * FishingVector * -1, 0);
        FishingTarget = Random.Range(60, 100) / 10f;
        _spawnCoroutine = StartCoroutine(SpawnParticleSystem());
        StartCoroutine(SwimCoroutine(FishingVector));
    }

    private IEnumerator SwimCoroutine(int fishingVector)
    {
        float posZ = transform.position.z;
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            transform.position += new Vector3(0, 0, 3 * Time.deltaTime * fishingVector);
            if (t > 0.5f)
            {
                CanBeFished = true;
            }
            yield return null;
        }

        float angleY = _fishTransform.localEulerAngles.y;
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.2f))
        {
            _fishTransform.localEulerAngles += new Vector3(0, 180 * (Time.deltaTime / 0.2f), 0);
            yield return null;
        }
        _fishTransform.localEulerAngles = new Vector3(0, angleY + 180, 0);

        float newPosZ = transform.position.z;
        
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            if (t > 0.5f)
            {
                CanBeFished = false;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(newPosZ, posZ, t));
            yield return null;
        }
        
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.5f))
        {
            yield return null;
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
        FishingVector = fishingVector * -1;
        StartCoroutine((SwimCoroutine(FishingVector)));
    }

    public void Catch(Vector3 endPosition)
    {
        StopCoroutine(_spawnCoroutine);
        StartCoroutine(CatchProcess(endPosition));
    }
    
    private IEnumerator CatchProcess(Vector3 endPosition)
    {
        Vector3 startPosition = transform.position;
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.25f))
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
        yield return null;
        }

        FindObjectOfType<FishingPlayer>().CheckFish();
        
        Destroy(gameObject);
    }

    private IEnumerator SpawnParticleSystem()
    {
        while (true)
        {
            Instantiate(_fishParticleSystem, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
