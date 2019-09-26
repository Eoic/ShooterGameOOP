package com.badlogic.network;

import java.net.http.WebSocket;
import java.util.concurrent.CompletionStage;
import java.util.concurrent.CountDownLatch;

public class Connector implements WebSocket.Listener {
    private final CountDownLatch latch;

    public Connector(CountDownLatch latch) {
        this.latch = latch;
    }

    @Override
    public void onOpen(WebSocket webSocket) {
        System.out.println("Connection opened.");
        WebSocket.Listener.super.onOpen(webSocket);
    }

    @Override
    public CompletionStage<?> onText(WebSocket webSocket, CharSequence data, boolean last) {
        System.out.println("Received message: " + data);
        latch.countDown();
        return WebSocket.Listener.super.onText(webSocket, data, last);
    }

    @Override
    public void onError(WebSocket webSocket, Throwable error) {
        System.out.println("An error occurred: " + webSocket.toString());
        WebSocket.Listener.super.onError(webSocket, error);
    }

    @Override
    public CompletionStage<?> onClose(WebSocket webSocket, int statusCode, String reason) {
        System.out.println("Connection closed with status code " + statusCode + ": " + reason);
        return WebSocket.Listener.super.onClose(webSocket, statusCode, reason);
    }
}
