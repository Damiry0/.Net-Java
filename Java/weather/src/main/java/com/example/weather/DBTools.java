package com.example.weather;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;
import java.time.*;
import com.google.gson.Gson;


public class DBTools {

        MongoClient client = MongoClients.create("mongodb://localhost:27017");
        public void AddToMongoDB(String city, ArrayList<Double> temp){
                MongoDatabase db = client.getDatabase("WeatherDB");

                MongoCollection col = db.getCollection("WeatherCollection");

                Document sampleDoc = new Document("city", city).append("temp_array", temp);

                col.insertOne(sampleDoc);
        }

        public void GetWeatherFromDB(){

                MongoDatabase db = client.getDatabase("WeatherDB");

                MongoCollection<org.bson.Document> coll = db.getCollection("WeatherCollection");
                Document doc = (Document) coll.find().first();

                System.out.println(doc);
        }
}
