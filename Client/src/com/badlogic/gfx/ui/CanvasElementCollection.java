package com.badlogic.gfx.ui;

import java.util.ArrayList;

public class CanvasElementCollection {
    private ArrayList<CanvasElement> elements;

    public CanvasElementCollection() {
        this.elements = new ArrayList<>();
    }

    // Add ui element to canvas collection
    public void attach(CanvasElement element) {
        this.elements.add(element);
    }

    // Remove ui element form canvas collection
    public void detach(CanvasElement element) {
        this.elements.remove(element);
    }

    // Update all canvas elements.
    public void refresh() {
        this.elements.forEach(CanvasElement::update);
    }
}
