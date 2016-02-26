using UnityEngine;
using CheloniiUnity;

public class UnityInterface : MonoBehaviour
{
    GameEngine engine = new TurtleTime.TurtleTimeEngine();

    public static float DELTA_TIME = 1 / 60f;
    GameObject world;
    GameObject ui;

    // Use this for initialization
    void Start ()
    {
        world = new GameObject("3D Node");
        ui = new GameObject("UI Node");
        world.transform.parent = gameObject.transform;
        ui.transform.parent = gameObject.transform;
        ui.AddComponent<Canvas>();
        ui.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        engine.SetGameObjects(world, ui);
        engine.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        engine.Update(DELTA_TIME);
	}
}
