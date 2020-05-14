using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int MAX = 3;
    private List<int> scenes;

    int level;

    //bool scene_0 = true;
    //bool scene_1 = true;
    //bool scene_2 = true;
    //bool scene_3 = true;
    bool scene_0;
    bool scene_1;
    bool scene_2;
    bool scene_3;
    int currentScene;
    GameObject[] objs;
    GameObject[] lastInteractable;
    AudioSource[] lastAudio;
    GameObject[] preAudioSource;
    AudioSource[] preAudio;
    bool preAudioPlayed = false;
    bool lastAudioPlayed = false;

    Scene activeScene;

    void Start()
    {
        // Initialize the list with levels
        scenes = new List<int>(Enumerable.Range(0, MAX)); // This creates a list with values from 1 to MAX

        objs = GameObject.FindGameObjectsWithTag("scenes");
        lastInteractable = GameObject.FindGameObjectsWithTag("lastInteractable"); // Last interactable object in scene
        //lastAudio = lastInteractable.GetComponent<AudioSource>(); // Audio of last interactable4
        lastAudio = new AudioSource[lastInteractable.Length];
        preAudioSource = GameObject.FindGameObjectsWithTag("preAudio"); // Pre-Scene Audio Objects
        Debug.Log(preAudioSource.Length);
        preAudio = new AudioSource[3];
        //preAudio = preAudioSource.GetComponents<AudioSource>(); // Audio of preAudioSource
        for(int i = 0; i < preAudioSource.Length; i++)
        {
            preAudio[i] = preAudioSource[i].GetComponent<AudioSource>();
        }

        activeScene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene name is: " + activeScene.name + "\nActive Scene index: " + activeScene.buildIndex);
        //Debug.Log("PreAudioPlayed at start:" + preAudioPlayed);

        //if (activeScene.buildIndex == 4)
        //{
        //    scene_1 = false;
        //} else 
        //{ 
        //    scene_1 = true; 
        //}
        //if (activeScene.buildIndex == 5)
        //{
        //    scene_2 = false;
        //}
        //else
        //{
        //    scene_2 = true;
        //}
        //if (activeScene.buildIndex == 6)
        //{
        //    scene_3 = false;
        //}
        //else
        //{
        //    scene_3 = true;
        //}

        if (activeScene.buildIndex == 1 && scene_1)
        {
            scene_1 = false;
        }
        else { scene_1 = true; }
        if (activeScene.buildIndex == 2 && scene_2)
        {
            scene_2 = false;
        }
        else { scene_2 = true; }
        if (activeScene.buildIndex == 3 && scene_3)
        {
            scene_3 = false;
        }
        else { scene_3 = true; }

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(preAudioSource[0]);
        DontDestroyOnLoad(preAudioSource[1]);
        //DontDestroyOnLoad(lastAudio);
    }

    void Update()
    {
        // Check if Pre-Scene Narration has been played
        if (preAudio[0].isPlaying)
        {
            preAudioPlayed = true;
        }
        

        // When Pre-Scene narration has been played, load main scene (pre-scene index +3)
        if (!preAudio[0].isPlaying && preAudioPlayed == true)
        {
            SceneManager.LoadScene(activeScene.buildIndex + 3);
            preAudioPlayed = false;
        }

        if (preAudio[1].isPlaying)
        {
            preAudioPlayed = true;
        }


        // When Pre-Scene narration has been played, load main scene (pre-scene index +3)
        if (!preAudio[1].isPlaying && preAudioPlayed == true)
        {
            SceneManager.LoadScene(activeScene.buildIndex + 3);
            preAudioPlayed = false;
        }


        //CheckLastAudio();


        //// If last narrated part has been played
        //if(!lastAudio[0].isPlaying && lastAudioPlayed == true)
        //{
        //    // and final scene is active, load end scene
        //    if (MAX <= 0)
        //    {
        //        //Application.Quit();
        //        Debug.Log("app quit");
        //        SceneManager.LoadScene(7);

        //    }
        //    // load new scene
        //    LoadNewScene();
        //    lastAudioPlayed = false;
        //} 
        if (Input.GetKeyDown("space"))
        {
            if (MAX <= 0)
            {
                //Application.Quit();
                Debug.Log("app quit");
                SceneManager.LoadScene(7);

            }

            LoadNewScene( );     
            lastAudioPlayed = false;
        }
    }
    public void CheckPreScene()
    {
        

    }
    public void LoadMainScene()
    {
        
    }

    public void CheckLastAudio()
    {
        //// Check if last narrated part has been played
        //if (lastAudio[0].isPlaying)
        //{
        //    lastAudioPlayed = true;
        //    //Debug.Log("LastAudio played");

        //}
    }

    public void LoadNewScene()
    {

        currentScene = Random.Range(1, 4);
        animator.SetTrigger("FadeOut");
        SceneChanger();
        //Debug.Log("The current Random is: " + currentScene);

    }
    public void ReloadScene()
    {
        currentScene = Random.Range(1, 4);
        SceneChanger();
    }
    public void SceneChanger()
    {
        if (currentScene == 0 && scene_0)
        {
            //SceneManager.LoadScene(0);
            //scene_0 = false;
            //MAX = MAX - 1;
            Debug.Log("Loaded Scene 0");
            //animator.SetTrigger("FadeIn");
        }

        // pre-scene 1
        else if (currentScene == 1 && scene_1)
        {
            SceneManager.LoadScene(1);
            scene_1 = false;
            MAX = MAX - 1;
            Debug.Log("Loaded Scene 1");
            animator.SetTrigger("FadeIn");
        }
        // pre-scene 2
        else if (currentScene == 2 && scene_2)
        {
            SceneManager.LoadScene(2);
            scene_2 = false;
            MAX = MAX - 1;
            Debug.Log("Loaded Scene 2");
            animator.SetTrigger("FadeIn");
        }
        // pre-scene 3
        else if (currentScene == 3 && scene_3)
        {
            SceneManager.LoadScene(3);
            scene_3 = false;
            MAX = MAX - 1;
            Debug.Log("Loaded Scene 3");
            animator.SetTrigger("FadeIn");
        }
        else
        {
            ReloadScene();
            animator.SetTrigger("FadeIn");
            Debug.Log("Had to reload new Scene!");
        }
    }




}




//using UnityEngine;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;

//public class LevelChanger : MonoBehaviour
//{
//    public Animator animator;
//    private int levelToLoad;




//    public List<int> availableScenes = new List<int>();

//    //private List<int> playedScenes = new List<int>();
//    //private List<string> sceneNames = new List<string>() { "Pre_Scene1", "Scene1", "Scene2", "Scene3" };


//    //void LoadNewScene()
//    //{
//    //    //if (playedScenes.Count <= 0)
//    //    //{
//    //    //    playedScenes.AddRange(availableScenes.ToArray());
//    //    //}
//    //    int a = UnityEngine.Random.Range(0, playedScenes.Count);
//    //    int nextScene = playedScenes[a];
//    //    playedScenes.RemoveAt(a);
//    //    SceneManager.LoadScene(sceneNames[nextScene]);
//    //}

//    void Start()
//    {
//        availableScenes.Add(0);
//        availableScenes.Add(1);
//        availableScenes.Add(2);
//        availableScenes.Add(3);

//        DontDestroyOnLoad(this.gameObject);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            FadeToNextLevel();
//            //LoadNewScene();
//        }

//    }


//    public void FadeToNextLevel()
//    {
//        if (availableScenes.Count == 0)
//        {
//            Application.Quit();
//        }
//        else
//        {
//            int index = Random.Range(0, availableScenes.Count);

//            print("random index:" + index);
//            // int theSceneIndex = availableScenes[index];
//            FadeToLevel(index);

//            availableScenes.RemoveAt(index);
//            for (int i = 0; i < availableScenes.Count; i++)
//            {
//                print("index after remove:" + i);
//            }
//            //playedScenes.Add(theSceneIndex);
//        }



//    }

//    public void FadeToLevel(int levelIndex)
//    {
//        levelToLoad = levelIndex;
//        SceneManager.LoadScene(levelToLoad);
//        //animator.SetTrigger("FadeOut");
//    }

//    public void OnFadeComplete()
//    {
//        SceneManager.LoadScene(levelToLoad);
//    }
//}
////List<int> scenesWereAlreadyLoaded = new List<int>();
////int currentSceneToLoad;

////void Update()
////{


////    if (Input.GetMouseButtonDown(0))
////    {
////        while (true)
////        {
////            currentSceneToLoad = Random.Range(1, 3);
////            if (!scenesWereAlreadyLoaded.Contains(currentSceneToLoad))
////            {
////                scenesWereAlreadyLoaded.Add(currentSceneToLoad);
////                SceneManager.LoadScene(currentSceneToLoad);
////                break;
////            }

////        }
////    } else if (scenesWereAlreadyLoaded.Count >= 3)
////    {
////        SceneManager.LoadScene(3);
////    }