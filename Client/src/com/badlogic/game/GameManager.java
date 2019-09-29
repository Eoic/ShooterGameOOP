package com.badlogic.game;

import com.badlogic.gfx.Camera;
import com.badlogic.gfx.Window;
import com.badlogic.input.InputManager;
import com.badlogic.util.Constants;

public class GameManager {
    private InputManager inputManager;
    private Window window;
    private Camera camera;

    public GameManager() {
        inputManager = new InputManager();
        window = new Window(Constants.WIDTH, Constants.HEIGHT, Constants.TITLE, Constants.IS_RESIZEABLE);
        window.getCanvas().addMouseListener(inputManager);
        window.addKeyListener(inputManager);
        camera = new Camera();
    }

    public InputManager getInputManager() {
        return inputManager;
    }

    public Window getWindow() {
        return window;
    }

    public Camera getCamera() {
        return camera;
    }
}
