using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class Main : MonoBehaviour
{

    public Trivial trivial;

    private int respuestaAleatoria;

    
    void Start()
    {
        
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=5&category=31&difficulty=easy&type=boolean"));
        
        
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

            
            
            /*for(int i = 0; i < 5; i++){
                respuestaAleatoria = Random.Range(0,2);
                
                Debug.Log("Pregunta: " + trivial.results[i].question);
                Debug.Log("Respuesta: " + Respuesta());
                
                if(Respuesta() != trivial.results[i].correct_answer){
                    Debug.Log("No has acertado");
                } else {
                    Debug.Log("Has acertado");
                }            
            }*/

            
           
            
            for(int i = 0; i < 5; i++){
                respuestaAleatoria = Random.Range(0,2);

                
                Debug.Log("Pregunta: " + trivial.results[i].question);

                if(respuestaAleatoria == 0){
                    Debug.Log("Respuesta: True");
                } else {
                    Debug.Log("Respuesta: False");
                }
                
                
                if(respuestaAleatoria.ToString() != trivial.results[i].correct_answer){
                    Debug.Log("No has acertado");
                } else {
                    Debug.Log("Has acertado");
                }            
            }
            
            

            
        }
    }

    
     
    

    
     /*private string Respuesta(){   
        
        if(respuestaAleatoria == 0){
            return "True";
        } else {
            return "False";
        }
     }
     */

   
    
    /*private bool TrueFalse(){   
        if(opcion == 0){
            return true;
        } else{
            return false;
        }

    }  */


   

    
    

}
