package com.badlogic.core.observer;

import java.util.ArrayList;

public class ConnectionSubject<T> implements Subject<T> {
    private ArrayList<Observer<T>> observers = new ArrayList<>();

    @Override
    public void attach(Observer observer) {
        observers.add(observer);
    }

    @Override
    public void detach(Observer observer) {
        observers.remove(observer);
    }

    @Override
    public void notifyAllObservers(T data) {
        observers.forEach(observer -> observer.update(data));
    }
}
