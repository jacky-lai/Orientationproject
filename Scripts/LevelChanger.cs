using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private float MAX = 4f; // the number of scenes to be loaded
    private List<int> scenes; // array holding all LevelChanger GOs

    int level;

    // booleans to check if scene has been loaded yet
    bool scene_0 = true;
    bool scene_1 = true;
    bool scene_2 = true;
    bool scene_3 = true;
 
    int currentScene;
    GameObject[] objs;

    GameObject lastInteractable;
    AudioSource lastAudio;

    GameObject preAudioSource;
    AudioSource preAudio;

    bool preAudioPlayed = false;
    bool lastAudioPlayed = false;

    Scene activeScene;

    void Awake()
    {
        scenes = new List<int>(Enumerable.Range(0, 4)); // This creates a list with values from 1 to MAX
    }

    void Start()
    {
        // Initialize the list with levels
       
        lastInteractable = GameObject.FindWithTag("lastInteractable"); // Last interactable object in scene
        lastAudio = lastInteractable.GetComponent<AudioSource>(); // Audio of last interactable

        preAudioSource = GameObject.FindWithTag("preAudio"); // GameObject holding the PreNarration
        preAudio = preAudioSource.GetComponent<AudioSource>(); // Audio of preAudioSource

        //makeActiveSceneUnavailable();

    }

    void Update()
    {

        preAudioSource = GameObject.FindWithTag("preAudio"); // GameObject holding the PreNarration
        preAudio = preAudioSource.GetComponent<AudioSource>(); // Audio of preAudioSource

        lastInteractable = GameObject.FindWithTag("lastInteractable"); // Last interactable object in scene
        lastAudio = lastInteractable.GetComponent<AudioSource>(); // Audio of last interactable

        makeActiveSceneUnavailable();

        // Load MainScene after PreScene Audio has been played
        if (activeScene.buildIndex == 1 || activeScene.buildIndex == 2 || activeScene.buildIndex == 3)
        {
            LoadMainSceneAfterPreAudio();
        }

        if (activeScene.buildIndex == 4 || activeScene.buildIndex == 5 || activeScene.buildIndex == 6)
        {
            LoadNewSceneAfterLastAudio();
        }

        // Load random PreScene (for testing, should be replaced by LoadNewSceneAfterLastAudio())
        if (Input.GetKeyDown("space"))
        {
            //LoadNewScene();
            StartCoroutine(CoroutineFadePreScene());
            lastAudioPlayed = false;
        }
        //LoadNewSceneAfterLastAudio();

    }

    // Check if scene has been loaded and set boolean so they are only loaded once 
    public void makeActiveSceneUnavailable()
    {
        Debug.Log("Active Scene: " + activeScene);
        activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex == 1 && scene_1)
        {
            scene_1 = false;
            Debug.Log("scene_1: " + scene_1);
        }
        else if (activeScene.buildIndex == 2 && scene_2)
        {
            scene_2 = false;
            Debug.Log("scene_2: " + scene_2);
        }
        else if (activeScene.buildIndex == 3 && scene_3)
        {
            scene_3 = false;
            Debug.Log("scene_3: " + scene_3);
        }
    }


    // Check if Pre-Scene Narration has been played 
    // Then load main scene 
    public void LoadMainSceneAfterPreAudio()
    {
        if (preAudio != null)
        {
            if (preAudio.isPlaying)
            {
                preAudioPlayed = true;
            }
        }

        // When Pre-Scene narration has been played, load main scene (pre-scene index +3)
        if (!preAudio.isPlaying && preAudioPlayed == true)
        {
            preAudioPlayed = false;
            StartCoroutine(CoroutineFadeMainScene());
        }
    }


    // Check if last audio has been played. Then load load new scene via SceneChanger
    public void LoadNewSceneAfterLastAudio()
    {
        if (lastAudio != null)
        {
            //// Check if last narrated part has been played
            if (lastAudio.isPlaying)
            {
                lastAudioPlayed = true;
            }
        }

        if (!lastAudio.isPlaying && lastAudioPlayed == true)
        {
            lastAudioPlayed = false;
            StartCoroutine(CoroutineFadePreScene());
        }

    }


    public IEnumerator CoroutineFadePreScene()
    {
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3);

        LoadNewScene();
    }

    public IEnumerator CoroutineFadeMainScene()
    {
        Scene currentActiveScene = SceneManager.GetActiveScene();

        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(currentActiveScene.buildIndex + 3);

        animator.SetTrigger("FadeIn");

    }

    // Calculate random (scene) number and pass it to SceneChanger()
    public void LoadNewScene()
    {

        if (MAX <= 1) // MAX = number of scenes to be loaded, if MAX <= 0 the end scene is loaded 
        {
            //Application.Quit();
            Debug.Log("app quit");
            SceneManager.LoadScene(7);
            animator.SetTrigger("FadeIn");
        }

        currentScene = Random.Range(1, 4); // else load a scene by random number between 1 and 3
        //animator.SetTrigger("FadeOut");
        SceneChanger();

    }

    // If scene has been already loaded, reload random scene via SceneChanger()
    public void ReloadScene()
    {
        currentScene = Random.Range(1, 4); 
        SceneChanger();
    }

    // Take random (scene) number and load pre-scene, if it hasn't been loaded yet
    public void SceneChanger()
    {

        // PRE-SCENE 1
        if (currentScene == 1 && scene_1) // if random scene == 1 AND scene_1 hasn't been loaded yet
        {
            SceneManager.LoadScene(1);
            scene_1 = false; // set scene_1 to false so it's only loaded once
            MAX = MAX - 1; // reduce MAX = number of scenes to be loaded
            Debug.Log("MAX: " + MAX);
            Debug.Log("Loaded Scene 1");
            animator.SetTrigger("FadeIn");
            
        }
        // PRE-SCENE 2
        else if (currentScene == 2 && scene_2)
        {
            SceneManager.LoadScene(2);
            scene_2 = false;
            MAX = MAX - 1;
            Debug.Log("MAX: " + MAX);
            Debug.Log("Loaded Scene 2");
            animator.SetTrigger("FadeIn");
            
        }
        // PRE-SCENE 3
        else if (currentScene == 3 && scene_3)
        {
            SceneManager.LoadScene(3);
            scene_3 = false;
            MAX = MAX - 1;
            Debug.Log("MAX: " + MAX);
            Debug.Log("Loaded Scene 3");
            animator.SetTrigger("FadeIn");
            
        }
        else
        {
            if (MAX != 1)
            {
                ReloadScene();
                animator.SetTrigger("FadeIn");
                //Debug.Log("Had to reload new Scene!");
                //Debug.Log(MAX);
            }
        }
    }


}
