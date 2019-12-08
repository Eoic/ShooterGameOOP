package com.badlogic.network;

import com.badlogic.core.observer.ConnectionSubject;
import com.badlogic.core.observer.Observer;

import java.net.http.WebSocket;
import java.util.concurrent.CompletionStage;
import java.util.concurrent.CountDownLatch;

public class Connector implements WebSocket.Listener {
    private final CountDownLatch latch;
    private ConnectionSubject<Object> connectionSubject;

    public Connector(CountDownLatch latch) {
        this.latch = latch;
        this.connectionSubject = new ConnectionSubject<>();
    }

    public void addListener(Observer observer) {
        connectionSubject.attach(observer);
    }

    public void removeListener(Observer observer) {
        connectionSubject.detach(observer);
    }

    @Override
    public void onOpen(WebSocket webSocket) {
        connectionSubject.notifyAllObservers("Connection opened");
        WebSocket.Listener.super.onOpen(webSocket);
    }

    @Override
    public CompletionStage<?> onText(WebSocket webSocket, CharSequence data, boolean last) {
        connectionSubject.notifyAllObservers(data);
        latch.countDown();
        return WebSocket.Listener.super.onText(webSocket, data, last);
    }

    @Override
    public void onError(WebSocket webSocket, Throwable error) {
        System.out.println("An error occurred: " + webSocket.toString());
        error.printStackTrace();
        WebSocket.Listener.super.onError(webSocket, error);
    }

    @Override
    public CompletionStage<?> onClose(WebSocket webSocket, int statusCode, String reason) {
        System.out.println("Connection closed with status code " + statusCode + ": " + reason);
        return WebSocket.Listener.super.onClose(webSocket, statusCode, reason);
    }
}
