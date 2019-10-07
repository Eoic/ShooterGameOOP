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
        camera = new Camera();
        inputManager = new InputManager(camera);
        window = new Window(Constants.WIDTH, Constants.HEIGHT, Constants.TITLE, Constants.IS_RESIZEABLE);
        window.getCanvas().addMouseListener(inputManager);
        window.addKeyListener(inputManager);
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
