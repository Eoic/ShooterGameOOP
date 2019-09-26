package com.badlogic.gfx;

import javax.swing.*;
import java.awt.*;
import java.awt.event.KeyEvent;

public class Window extends JFrame {
    private int width;
    private int height;
    private Canvas canvas;

    private Window(int width, int height) {
        this.width = width;
        this.height = height;
        this.setSize(width, height);
        this.setVisible(true);
        // this.setFocusable(true);
        this.setLocationRelativeTo(null);
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        this.canvas = new Canvas();
        this.canvas.setPreferredSize(new Dimension(width, height));
        this.canvas.setMaximumSize(new Dimension(width, height));
        this.canvas.setMinimumSize(new Dimension(width, height));
        this.canvas.setFocusable(false);
        this.add(canvas);
        this.pack();
    }

    private Window(int width, int height, String title) {
        this(width, height);
        this.setTitle(title);
    }

    public Window(int width, int height, String title, boolean isResizeable) {
        this(width, height, title);
        this.setResizable(isResizeable);
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public Canvas getCanvas() {
        return canvas;
    }
}
