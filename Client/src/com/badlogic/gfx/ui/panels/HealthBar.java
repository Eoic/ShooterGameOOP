package com.badlogic.gfx.ui.panels;

import com.badlogic.gfx.Window;
import com.badlogic.gfx.ui.CanvasElement;
import com.badlogic.gfx.ui.Position;
import com.badlogic.util.Point;
import com.badlogic.util.Vector;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;
import java.awt.*;

public class HealthBar extends JPanel implements CanvasElement {
    private Window window;
    private JPanel background;
    private Position xPosition, yPosition;
    private int width, height, xOffset, yOffset, minValue, maxValue = 100, currentValue = 50;
    private JLabel label;

    public HealthBar(Window window, int width, int height, Position xPosition, Position yPosition) {
        this.label = new JLabel();
        this.window = window;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.width = width;
        this.height = height;
        this.createInfoLabel();
        this.setBounds(0, 0, width, height);
        this.setOpaque(true);
        this.setBackground(new Color(236, 16, 72));
        this.setBorder(new LineBorder(new Color(255, 150, 150), 5, false));
    }

    @Override
    public void update() {
        var startPoint = getStartPosition();
        this.setBounds((int)startPoint.getX() + this.xOffset, (int)startPoint.getY() + this.yOffset, this.width, this.height);
    }

    // Calculates from which point panel should be started to render
    private Point getStartPosition() {
        int xStart = getStartPoint(this.window.getWidth(), this.width, this.xPosition);
        int yStart = getStartPoint(this.window.getHeight(), this.height, this.yPosition);
        return new Point(xStart, yStart);
    }

    // Get position of rendered element according to its size and window width.
    private int  getStartPoint(int windowSize, int elementSize, Position position) {
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

    private void createInfoLabel() {
        this.label.setText(currentValue + " / " + maxValue);
        this.label.setHorizontalAlignment(SwingConstants.CENTER);
        this.label.setFont(new Font("Arial", Font.BOLD, 16));
        this.label.setForeground(Color.white);
        this.add(label);
    }

    public void updateStatus() {
        this.label.setText(currentValue + " / " + maxValue);
    }

    //
    public JPanel getBackgroundElement() {
        return null;
    }

    public JPanel getForegroundComponent() {
        return null;
    }
}
