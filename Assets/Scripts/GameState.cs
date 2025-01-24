using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public enum GameStateType { Normal, Unsettling, FullHorror }

    [System.Serializable]
    public class GameStateCanvases {
        public GameStateType stateType;
        public Canvas[] canvases;
    }

    public GameStateCanvases[] stateCanvasesArray;
    public GameStateType currentState = GameStateType.Normal;

    private Queue<Canvas> canvasQueue = new Queue<Canvas>();
    private Dictionary<GameStateType, Canvas[]> canvasMapping;

    void Start() {
        canvasMapping = new Dictionary<GameStateType, Canvas[]>();
        foreach (var stateCanvas in stateCanvasesArray) {
            canvasMapping[stateCanvas.stateType] = stateCanvas.canvases;
        }

        InitializeCanvasQueue();
    }

    public void OnButtonPress() {
        if (canvasQueue.Count == 0) {
            InitializeCanvasQueue();
        }

        foreach (Canvas canvas in canvasMapping[currentState]) {
            canvas.gameObject.SetActive(false);
        }

        if (canvasQueue.Count > 0) {
            Canvas nextCanvas = canvasQueue.Dequeue();
            nextCanvas.gameObject.SetActive(true);
        }
    }

    public void SetGameState(GameStateType newState) {
        currentState = newState;
        InitializeCanvasQueue();
    }

    public void IncreaseGameState() {
        if (currentState < GameStateType.FullHorror) {
            currentState++;
            Debug.Log($"Game state increased to: {currentState}");
            InitializeCanvasQueue();
        } else {
            Debug.LogError("Cannot increase game state. Already at Full Horror.");
        }
    }

    public void DecreaseGameState() {
        if (currentState > GameStateType.Normal) {
            currentState--;
            Debug.Log($"Game state decreased to: {currentState}");
            InitializeCanvasQueue();
        } else {
            Debug.LogError("Cannot decrease game state. Already at Normal.");
        }
    }

    private void InitializeCanvasQueue() {
        if (canvasMapping.TryGetValue(currentState, out Canvas[] canvases)) {
            canvasQueue.Clear();
            foreach (Canvas canvas in canvases) {
                canvasQueue.Enqueue(canvas);
            }
        }
    }
}