using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectsToCollect : MonoBehaviour
{
    // The FPS camera
    public int bottles;
    public TextMeshProUGUI bottle;
    public int boxes;
    public TextMeshProUGUI box;
    public int multiparameters;
    public TextMeshProUGUI multiparameter;
    public int gloves;
    public TextMeshProUGUI glove;
    public int totalItems;
    public Camera fpsCamera;
    public GameObject gameEnd;
    public InputHandler inputHandler;
    public Progress progress;
    public bool notFinished;

    // The distance within which items can be collected
    public float collectionDistance = 5f;

    private void Update()
    {
        checkscore();
        // Check if the player clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the ray from the camera to the mouse cursor
            Ray ray = fpsCamera.ScreenPointToRay(Input.mousePosition);

            // Cast a raycast against all objects in the scene
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, collectionDistance))
            {
                // Check if the raycast hit an item
                if (hit.collider.gameObject.tag == "Bottle")
                {
                    bottles += 16;
                    progress.toAdd = 26;
                    progress.AddToProgress(0);
                    progress.PlayAudio(0);
                    // Collect the item
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.gameObject.tag == "Box")
                {
                    boxes++;
                    progress.PlayAudio(3);
                    progress.toAdd = 24;
                    progress.AddToProgress(0);

                    // Collect the item
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.gameObject.tag == "Multiparameter")
                {
                    multiparameters++;
                    if (multiparameters == 1)
                    {
                        progress.PlayAudio(1);
                    }

                    // Collect the item
                    Destroy(hit.collider.gameObject);
                    progress.toAdd = 24;
                    progress.AddToProgress(0);
                }

                if (hit.collider.gameObject.tag == "Gloves")
                {
                    gloves++;
                    progress.toAdd = 26;
                    progress.AddToProgress(0);
                    progress.PlayAudio(2);
                    // Collect the item
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void checkscore()
    {
        bottle.text = "Bottles : " + bottles + "/16";
        glove.text = "Gloves : " + gloves + "/1";
        multiparameter.text = "Multiparameter meter : " + multiparameters + "/1";
        box.text = "Coolers : " + boxes + "/1";
        if (bottles + boxes + multiparameters + gloves == totalItems)
        {
            gameEnd.SetActive(true);
            inputHandler.LockUnlockCursor(true);
            if (notFinished)
            {
                StartCoroutine(PlayDelayAudio(3));
                notFinished = false;
            }
        }
        /* if (bottles == 16)
         {
             progress.AddToProgress(0);
         }
         if (gloves == 1)
         {
             progress.AddToProgress(0);
         }
         if (boxes == 16)
         {
             progress.AddToProgress(0);
         }
         if (boxes == 16)
         {
             progress.AddToProgress(0);
         }*/
    }

    public void leave(int l)
    {
        SceneManager.LoadScene(l);
    }

    public IEnumerator PlayDelayAudio(float p)
    {
        yield return new WaitForSeconds(p);
        progress.PlayAudio(4);
    }
}