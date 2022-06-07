package com.example.weather;
import org.bson.types.ObjectId;

import java.util.ArrayList;
import java.time.*;

public class WeatherModel {

    ObjectId _id;
    String city;
    ArrayList<Double> temp;



    public WeatherModel(ObjectId id, String city, ArrayList<Double> temperature) {

        this._id = id;
        this.city = city;
        this.temp = temperature;
        }

    public WeatherModel() {

    }

    public ObjectId get_id() {
        return _id;
    }

    public String getCity() {
        return city;
    }

    public ArrayList<Double> getTemp() {
        return temp;
    }

    public void set_id(ObjectId _id) {
        this._id = _id;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public void setTemp(ArrayList<Double> temp) {
        this.temp = temp;
    }
}
