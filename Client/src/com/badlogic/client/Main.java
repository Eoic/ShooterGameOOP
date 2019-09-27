package com.badlogic.client;

import com.badlogic.core.Loop;

public class Main {
    public static void main(String[] args)  {
        new Loop().start();
        /*
        CountDownLatch latch = new CountDownLatch(1);
        Connector connector = new Connector(latch);
        WebSocket webSocket = HttpClient.newHttpClient()
                                        .newWebSocketBuilder()
                                        .buildAsync(URI.create(Constants.SOCKET_CONNECTION_STRING), connector)
                                        .join();
        webSocket.sendText("Hello", true);
        latch.await();
         */
    }
}
