using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Main : MonoBehaviour
{

    public Trivial trivial;

    private int respuestaAleatoria;

    
    void Start()
    {
        
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=10&category=15&type=boolean"));
        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;               
            }
            trivial = JsonUtility.FromJson<Trivial>(webRequest.downloadHandler.text);

            int i = 0;
            while(i < 10){           
                Debug.Log("Pregunta " + (i + 1) + ": " + trivial.results[i].question);                               
                
                respuestaAleatoria = Random.Range(0,2);
                Respuesta();
               
                Debug.Log("Respuesta " + (i + 1) + ": " + Respuesta());

                Debug.Log("Tu respuesta " + (i + 1) + ": " + Respuesta().ToString() + "   Respuesta correcta " + (i + 1) + ": " + trivial.results[i].correct_answer); 
                
                if(Respuesta().ToString() == trivial.results[i].correct_answer){        
                    Debug.Log("Has acertado");
                } else {
                    Debug.Log("No has acertado");
                }   
                i++;          
            }   

        }       
        
    }
    private bool Respuesta(){         
        if(respuestaAleatoria == 0){
            return true;
        } else {
            return false;
        }                
    }
}
