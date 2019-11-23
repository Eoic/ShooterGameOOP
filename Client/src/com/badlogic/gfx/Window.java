package com.badlogic.gfx;

import com.badlogic.gfx.ui.*;
import com.badlogic.gfx.ui.panels.HealthBar;
import com.badlogic.gfx.ui.panels.TeamSelection;
import com.badlogic.network.MessageEmitter;
import com.badlogic.serializables.SerializableGame;
import com.badlogic.ui.GameList;
import com.badlogic.util.JsonParser;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionListener;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.util.ArrayList;

public class Window extends JFrame implements ComponentListener {
    private int width;
    private int height;
    private Canvas canvas;
    private GameList gameList;
    private JButton createGameBtn;
    private JButton quitGameBtn;
    private HealthBar clientHealthBar;
    private TeamSelection teamSelectionPanel;
    private Timer timer;
    private CanvasElementCollection canvasElementCollection;

    private Window(int width, int height) {
        this.width = width;
        this.height = height;
        this.setSize(width, height);
        this.setVisible(true);
        this.setFocusable(true);
        this.setLocationRelativeTo(null);
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        this.getContentPane().addComponentListener(this);
        this.canvas = new Canvas();
        this.canvas.setBackground(Color.white);
        this.canvas.setPreferredSize(new Dimension(width, height));
        this.canvas.setMaximumSize(new Dimension(width, height));
        this.canvas.setMinimumSize(new Dimension(width, height));
        this.canvasElementCollection = new CanvasElementCollection();
        this.canvas.setFocusable(false);
        this.clientHealthBar = (HealthBar) CanvasFactory.createPanel(CanvasElementType.HealthBar, this, Position.CENTER, Position.END, 750, 35);
        this.teamSelectionPanel = new TeamSelection(this, new String[]{"A", "B" });
        this.teamSelectionPanel.setVisible(false);
        assert clientHealthBar != null;
        this.clientHealthBar.setOffset(0, -31);
        this.canvasElementCollection.attach(clientHealthBar);
        this.canvasElementCollection.attach(teamSelectionPanel);
        this.clientHealthBar.addToFrame(this);
        this.teamSelectionPanel.addToFrame(this);
        this.clientHealthBar.setVisible(false);
        this.createInterface();
        this.createGameListWindow();
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

    private void createInterface() {
        createGameBtn = this.createButton(10, 10, 150, 35, "Create game");
        quitGameBtn = this.createButton(this.width - 160, 10, 150, 35, "Quit game");
        quitGameBtn.setVisible(false);
        this.add(createGameBtn);
        this.add(quitGameBtn);
    }

    private JButton createButton(int posX, int posY, int width, int height, String content) {
        var button = new JButton();
        button.setBounds(posX, posY, width, height);
        button.setText(content);
        button.setFocusable(false);
        return button;
    }

    // Perform game creation and hide unrelated ui elements.
    public void setCreateGameBtnEvent(ActionListener actionListener) {
        createGameBtn.addActionListener(actionListener);
    }

    public void setActiveGameMode() {
        gameList.setVisible(false);
        teamSelectionPanel.setVisible(false);
        createGameBtn.setVisible(false);
        quitGameBtn.setVisible(true);
    }

    public void setQuitGameBtnEvent(ActionListener actionListener) {
        quitGameBtn.addActionListener(actionListener);
    }

    @Override
    public void componentResized(ComponentEvent componentEvent) {
        this.width = componentEvent.getComponent().getWidth();
        this.height = componentEvent.getComponent().getHeight();
        this.gameList.setSize(width, height);
        this.gameList.revalidate();
        this.canvasElementCollection.refresh();
        this.quitGameBtn.setBounds(this.width - 160, 10, 150, 35);
        this.revalidate();
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

    public HealthBar getClientHealthBar() {
        return this.clientHealthBar;
    }

    private void createGameListWindow() {
        this.gameList = new GameList();
        this.gameList.setSize(this.width, this.height);
        this.add(this.gameList, BorderLayout.PAGE_START);
    }

    public void updateGameList(ArrayList<SerializableGame> gameArrayList, MessageEmitter messageEmitter) {
        this.gameList.updateList(gameArrayList, messageEmitter, new JsonParser(), this);
    }

    public void setCanvasColor(Color color) {
        this.canvas.setBackground(color);
    }

    public void showTeamSelectionWindow() {
        this.gameList.setVisible(false);
        this.createGameBtn.setVisible(false);
        this.teamSelectionPanel.setVisible(true);
    }

    public void setTeamSelectionEvents(MessageEmitter messageEmitter, ArrayList<ActionListener> listeners) {
        this.teamSelectionPanel.setEventForSelection(messageEmitter, listeners);
    }
}
