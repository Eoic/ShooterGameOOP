package com.badlogic.gfx;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionListener;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;

public class Window extends JFrame implements ComponentListener {
    private int width;
    private int height;
    private Canvas canvas;
    private JButton createGameBtn;

    private Window(int width, int height) {
        this.width = width;
        this.height = height;
        this.setSize(width, height);
        this.setVisible(true);
        this.setLocationRelativeTo(null);
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        this.getContentPane().addComponentListener(this);
        this.canvas = new Canvas();
        this.canvas.setPreferredSize(new Dimension(width, height));
        this.canvas.setMaximumSize(new Dimension(width, height));
        this.canvas.setMinimumSize(new Dimension(width, height));
        this.canvas.setFocusable(false);
        this.createInterface();
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

    // -- UI --
    private void createInterface() {
        createGameBtn = new JButton();
        createGameBtn.setBounds(10, 10, 150, 35);
        createGameBtn.setText("Create game");
        createGameBtn.setFocusable(false);
        this.add(createGameBtn);
    }

    public void setCreateGameBtnEvent(ActionListener actionListener) {
        createGameBtn.addActionListener(actionListener);
    }

    // --------

    @Override
    public void componentResized(ComponentEvent componentEvent) {
        this.width = componentEvent.getComponent().getWidth();
        this.height = componentEvent.getComponent().getHeight();
    }

    @Override
    public void componentMoved(ComponentEvent componentEvent) {
    }

    @Override
    public void componentShown(ComponentEvent componentEvent) {
    }

    @Override
    public void componentHidden(ComponentEvent componentEvent) {
    }
}
