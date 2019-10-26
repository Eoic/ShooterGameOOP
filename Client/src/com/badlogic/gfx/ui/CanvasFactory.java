package com.badlogic.gfx.ui;

import com.badlogic.gfx.Window;
import com.badlogic.gfx.ui.panels.HealthBar;

public class CanvasFactory {
    public static CanvasElement createPanel(String elementType, Window window, Position xPosition, Position yPosition, int width, int height) {
        if (elementType.equals(CanvasElementType.HealthBar)) {
            return new HealthBar(window, width, height, xPosition, yPosition);
        } else return null;
    }
}
