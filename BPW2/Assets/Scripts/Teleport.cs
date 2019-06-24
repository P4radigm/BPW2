using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Camera mainCamera;
    public float yOffset = 0.5f;
    public float range;
    public float Timervalue = 3;
    public float SingleCharge = 3;
    public float LastChargeThresh = 1.5f;
    public float TimerMax = 9;
    private bool LastCharge = true;
    private float TeleportProgression;
    public Material playerTeleportMat;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timervalue < TimerMax && LastCharge == true)
        {
            Timervalue += Time.deltaTime;
        }
        else if (Timervalue < TimerMax)
        {
            Timervalue += Time.deltaTime * 0.8f;
        }

        if (LastCharge == false && Timervalue > SingleCharge)
        {
            LastCharge = true;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            Vector3 hitPoint = hit.point;

            hitPoint = new Vector3(hitPoint.x, hitPoint.y + yOffset, hitPoint.z);

            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Vector3.Distance(hitPoint, transform.position) < range && Timervalue > SingleCharge)
                {
                    StartCoroutine(TeleportAnimation1());
                }
                else if (Vector3.Distance(hitPoint, transform.position) > range && Timervalue > SingleCharge)
                {
                    StartCoroutine(TeleportAnimation2());
                }
                else if (Vector3.Distance(hitPoint, transform.position) < range && Timervalue > LastChargeThresh && LastCharge == true)
                {
                    StartCoroutine(TeleportAnimation1());
                    LastCharge = false;
                }
                else if (Vector3.Distance(hitPoint, transform.position) > range && Timervalue > LastChargeThresh && LastCharge == true)
                {
                    StartCoroutine(TeleportAnimation2());
                    LastCharge = false;
                }
                else
                {
                    //Dingen gebeuren niet
                }
            }
        }

        IEnumerator TeleportAnimation1()
        {
            Vector3 hitPoint = hit.point;

            hitPoint = new Vector3(hitPoint.x, hitPoint.y + yOffset, hitPoint.z);

            for (float f = -15; f < 2; f += 1f)
            {
                TeleportProgression = f;
                playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
                yield return new WaitForSeconds(0.0001f);
            }

            transform.position = hitPoint;

            playerTeleportMat.SetFloat("Boolean_A538A6D1", 1f);

            for (float g = -9; g < 8; g += 1f)
            {
                TeleportProgression = g;
                playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
                yield return new WaitForSeconds(0.0001f);
            }

            TeleportProgression = -15;
            playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
            playerTeleportMat.SetFloat("Boolean_A538A6D1", 0f);
            Timervalue -= SingleCharge;
        }

        IEnumerator TeleportAnimation2()
        {
            Vector3 hitPoint = hit.point;

            hitPoint = new Vector3(hitPoint.x, hitPoint.y + yOffset, hitPoint.z);

            Ray PlayerToMouse = new Ray(transform.position, hitPoint - transform.position);

            for (float f = -15; f < 2; f += 1f)
            {
                TeleportProgression = f;
                playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
                yield return new WaitForSeconds(0.0001f);
            }

            transform.position = PlayerToMouse.GetPoint(range);

            playerTeleportMat.SetFloat("Boolean_A538A6D1", 1f);

            for (float g = -9; g < 8; g += 1f)
            {
                TeleportProgression = g;
                playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
                yield return new WaitForSeconds(0.0001f);
            }

            TeleportProgression = -15;
            playerTeleportMat.SetFloat("Vector1_58DAC233", TeleportProgression);
            playerTeleportMat.SetFloat("Boolean_A538A6D1", 0f);
            Timervalue -= SingleCharge;
        }
    }
}
