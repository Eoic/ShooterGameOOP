package com.badlogic.core.factory;

import com.badlogic.core.GameObject;
import com.badlogic.game.GameManager;
import com.badlogic.game.Player;
import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;


public abstract class Bonus extends GameObject {
    protected int lifespan;
    protected int bonusAmount;
    protected GameManager gameManager;
    public abstract void applyBonus(Player player);
    public abstract int getBonusAmount();
    public abstract int getLifespan();
    public abstract void setLifespan(int lifespan);
    public abstract void setBonusAmount(int bonusAmount);

    public Bonus(GameManager gameManager, BufferedImage sprite) {
        this.position = new Vector(0, 0);
        this.gameManager = gameManager;
        this.sprite = sprite;
    }

    @Override
    public void render(Graphics graphics) {
        var camOffset = gameManager.getCamera().getOffset();
        var windowSize = gameManager.getWindow().getSize();
        int posX = (int)position.getX() - (int)camOffset.getX() - Constants.SPRITE_WIDTH_HALF;
        int posY = (int)position.getY() - (int)camOffset.getY() - Constants.SPRITE_HEIGHT_HALF;
        graphics.drawImage(sprite, posX, posY, null);
    }
}
