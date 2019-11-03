package com.badlogic.gfx.ui.panels;

import com.badlogic.gfx.Window;
import com.badlogic.gfx.ui.CanvasElement;
import com.badlogic.gfx.ui.Colors;
import com.badlogic.gfx.ui.Position;
import com.badlogic.util.Point;

import javax.swing.*;
import javax.swing.border.LineBorder;

public class HealthBar implements CanvasElement {
    private int width, height, xOffset, yOffset, maxValue = 100, currentValue = 100;
    private JPanel background = new JPanel();
    private JPanel foreground = new JPanel();
    private Position xPosition, yPosition;
    private Window window;

    public HealthBar(Window window, int width, int height, Position xPosition, Position yPosition) {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.window = window;
        this.height = height;
        this.width = width;
        this.createBackground();
        this.createForeground();
    }

    @Override
    public void update() {
        var startPoint = getStartPosition();
        var foregroundWidth = ((maxValue - currentValue) * this.width) / maxValue;
        this.foreground.setBounds((int)startPoint.getX() + this.xOffset, (int)startPoint.getY() + this.yOffset, this.width - foregroundWidth, this.height);
        this.background.setBounds((int)startPoint.getX() + this.xOffset + this.width - foregroundWidth, (int)startPoint.getY() + this.yOffset, foregroundWidth, this.height);
    }

    @Override
    public void addToFrame(JFrame frame) {
        frame.add(foreground);
        frame.add(background);
    }

    // Calculates from which point panel should be started to render
    private Point getStartPosition() {
        int xStart = getStartPoint(this.window.getWidth(), this.width, this.xPosition);
        int yStart = getStartPoint(this.window.getHeight(), this.height, this.yPosition);
        return new Point(xStart, yStart);
    }

    // Get position of rendered element according to its size and window width.
    private int getStartPoint(int windowSize, int elementSize, Position position) {
        int startPoint = 0;

        if (position == Position.END)
            startPoint = windowSize - elementSize;
        else if (position == Position.CENTER)
            startPoint = windowSize / 2 - elementSize / 2;

        return startPoint;
    }

    public void setOffset(int xOffset, int yOffset) {
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }

    private void createBackground() {
        this.background.setOpaque(true);
        this.background.setBackground(Colors.Gray);
        this.background.setBorder(new LineBorder(Colors.DarkRed, 1, false));
    }

    private void createForeground() {
        this.foreground.setOpaque(true);
        this.foreground.setBackground(Colors.Crimson);
        this.foreground.setBorder(new LineBorder(Colors.DarkRed, 1, false));
    }
    public void setVisible(boolean visible) {
        this.background.setVisible(visible);
        this.foreground.setVisible(visible);
    }

    public void setCurrentValue(int currentValue) {
        this.currentValue = currentValue;
        this.update();
    }

    public void setMaxValue(int maxValue) {
        this.maxValue = maxValue;
    }
}
