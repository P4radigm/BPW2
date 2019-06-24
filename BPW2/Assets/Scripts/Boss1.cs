using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject player;
    public Material attackTileMat;
    public GameObject[] attacktiles;
    public float playerPosX;
    public float playerPosY;
    public float gradientPos;
    private List<string> cases = new List<string>();
    private Coroutine currentAttackRoutine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attacktiles = GameObject.FindGameObjectsWithTag("AttackTile");
        playerPosX = player.transform.position.x;
        playerPosY = player.transform.position.y;
        gradientPos = attackTileMat.GetFloat("Vector1_66DC8054");

        for (int i = 0; i <= 79; i += 1)
        {
            attacktiles[i].GetComponent<Collider>().enabled = false;
            attacktiles[i].SetActive(false);
        }

        cases.Add("ColumnsEven");
        
        //StartCoroutine(Checkerboard());
        ChooseRandomAttack();
    }

    private void ChooseRandomAttack()
    {
        string attack = Choose(cases.ToArray());
        currentAttackRoutine = StartCoroutine(attack);
    }

    T Choose<T>(params T[] input)
    {
        return input[Random.Range(0, input.Length)];
    }

    //IEnumerator FirstFaseAttackGlow()
    //{
    //    for (float f = 10; f > 8; f -= 0.01f)
    //    {
    //        gradientPos = f;
    //        attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //}

    //IEnumerator SecondFaseAttackGlow()
    //{
    //    gradientPos = 1;
    //    attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
    //    yield return new WaitForSeconds(10f);
    //    yield return null;
    //}

    //IEnumerator ThirdFaseAttackGlow()
    //{
    //    for (float g = 1; g < 10.2; g += 0.5f)
    //    {
    //        gradientPos = g;
    //        attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    yield return null;
    //}

    IEnumerator Follow()
    {

        yield return null;
    }

    IEnumerator ColumnsEven()
    {
        //Sets the correct tiles to active
        for (int i = 0; i <= 79; i += 2)
        {
            attacktiles[i].SetActive(true);
        }

        //Raises the gradient slowly
        for (float f = 10; f > 8; f -= 0.01f)
        {
            gradientPos = f;
            attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
            yield return new WaitForSeconds(0.001f);
        }

        //enables the colliders
        for (int i = 0; i <= 79; i += 2)
        {
            attacktiles[i].GetComponent<Collider>().enabled = true;
        }

        //sets the gradient to max
        gradientPos = 1;
        attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
        yield return new WaitForSeconds(2f);

        //disables the colliders
        for (int i = 0; i <= 79; i += 2)
        {
            attacktiles[i].GetComponent<Collider>().enabled = false;
        }

        //Lowers the gradient quickly
        for (float g = 1; g < 10.2; g += 0.5f)
        {
            gradientPos = g;
            attackTileMat.SetFloat("Vector1_66DC8054", gradientPos);
            yield return new WaitForSeconds(0.01f);
        }
        //Disables all tiles
        for (int i = 0; i <= 79; i += 1)
        {
            attacktiles[i].SetActive(false);
        }

        ChooseRandomAttack();
    }
}
