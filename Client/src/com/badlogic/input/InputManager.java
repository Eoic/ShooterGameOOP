package com.badlogic.input;

import com.badlogic.gfx.Camera;
import com.badlogic.util.Point;
import com.badlogic.util.Vector;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

public class InputManager implements KeyListener, MouseListener {
    // Keyboard
    private boolean[] keys;
    public boolean up, down, left, right;

    // Mouse
    public boolean lmb;
    private Vector mouseClickPoint;

    // View
    private Camera camera;
    private Vector mouseDirection;

    public InputManager(Camera camera) {
        keys = new boolean[256];
        mouseClickPoint = new Vector(0, 0);
        mouseDirection = new Vector(0, 0);
        this.camera = camera;
    }

    public void tick() {
        up = keys[KeyEvent.VK_W];
        down = keys[KeyEvent.VK_S];
        left = keys[KeyEvent.VK_A];
        right = keys[KeyEvent.VK_D];
    }

    @Override
    public void keyTyped(KeyEvent keyEvent) {
        keys[keyEvent.getKeyCode()] = true;
    }

    @Override
    public void keyPressed(KeyEvent keyEvent) {
        keys[keyEvent.getKeyCode()] = true;
    }

    @Override
    public void keyReleased(KeyEvent keyEvent) {
        keys[keyEvent.getKeyCode()] = false;
    }

    @Override
    public void mouseClicked(MouseEvent mouseEvent) {

    }

    @Override
    public void mousePressed(MouseEvent mouseEvent) {
        if (!lmb) {
            lmb = true;
            this.mouseClickPoint = new Vector(mouseEvent.getX(), mouseEvent.getY());
        }
    }

    @Override
    public void mouseReleased(MouseEvent mouseEvent) {
        lmb = false;
    }

    @Override
    public void mouseEntered(MouseEvent mouseEvent) {

    }

    @Override
    public void mouseExited(MouseEvent mouseEvent) {

    }

    public Vector getMouseClickPoint() {
        return this.mouseClickPoint;
    }
}
