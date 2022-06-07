package com.example.weather;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.ResourceBundle;

import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.fxml.FXML;
import javafx.scene.chart.CategoryAxis;
import javafx.scene.chart.LineChart;
import javafx.scene.chart.NumberAxis;
import javafx.scene.chart.XYChart;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.text.Text;
import org.bson.Document;
import org.json.JSONArray;
import org.json.JSONObject;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Calendar;


public class HelloController {


    @FXML
    private ResourceBundle resources;

    @FXML
    private URL location;

    @FXML
    private LineChart<?, ?> LineChart;

    @FXML
    private TextField city;

    @FXML
    private Button getData;

    @FXML
    private Text pressure;

    @FXML
    private Text temp;

    @FXML
    private Text temp_feels;

    @FXML
    private Text temp_max;

    @FXML
    private Text temp_min;

    @FXML
    private Label welcomeText;

    @FXML
    private CategoryAxis x;

    @FXML
    private NumberAxis y;

    @FXML
    void initialize() {
        getData.setOnAction(event -> {
            String getUserCity = city.getText().trim();
            String output = getUrlContent("http://api.openweathermap.org/data/2.5/weather?q=" + getUserCity + "&appid=78bbffef3c4b481a1bd6798b18d1f6ae&units=metric");
            System.out.println(output);
            Double lat = null;
            Double lon = null;
            if (!output.isEmpty()) { // The city Exist
                JSONObject obj = new JSONObject(output);
                // Change JSON to string
                temp.setText("Temperature: " + obj.getJSONObject("main").getDouble("temp") + "°C");
                temp_feels.setText("Feels: " + obj.getJSONObject("main").getDouble("feels_like") + "°C");
                temp_max.setText("Max: " + obj.getJSONObject("main").getDouble("temp_max") + "°C");
                temp_min.setText("Min: " + obj.getJSONObject("main").getDouble("temp_min") + "°C");
                pressure.setText("Pressure: " + obj.getJSONObject("main").getDouble("pressure") + "mbar");

                lon = obj.getJSONObject("coord").isNull("lon") ? null : obj.getJSONObject("coord").getDouble("lon");
                lat = obj.getJSONObject("coord").isNull("lat") ? null : obj.getJSONObject("coord").getDouble("lat");
            }

            String output1 = getUrlContent("http://api.openweathermap.org/data/2.5/forecast?lat=" + lat + "&lon=" + lat + "&appid=78bbffef3c4b481a1bd6798b18d1f6ae");
            System.out.println(output1);
            ArrayList<Double> temp = myTemp(output1);
            ArrayList<String> date = myDate(output1);
            System.out.println(getUserCity);


            XYChart.Series series = new XYChart.Series();


            for (int i = 0; i < 40; i++) {
                series.setName(getUserCity);
                series.getData().add(new XYChart.Data(date.get(i), temp.get(i) - 273));
            }
            LineChart.getData().add(series);

            /*TESTS FOR DB*/

            DBTools db = new DBTools();

            db.AddToMongoDB(getUserCity, temp);

            db.GetWeatherFromDB();
            });
    }

    public ArrayList<Double> myTemp(String output) {
        ArrayList<Double> numbers = new ArrayList<Double>();

        JSONObject obj1 = new JSONObject(output);
        //double temp = obj1.getJSONObject("main").isNull("temp") ? null : obj1.getJSONObject("main").getDouble("temp");
        JSONArray arr = obj1.getJSONArray("list");
        arr.length();
        JSONObject weth;
        double temp;
        String date;
        for (int i = 0; i < 40; i++) {
            weth = (JSONObject) arr.get(i);
            temp = weth.getJSONObject("main").getDouble("temp");
            System.out.println(weth.getString("dt_txt"));
            numbers.add(temp);
        }

        return(numbers);
    }

    public ArrayList<String> myDate(String output) {
        ArrayList<String> dates = new ArrayList<String>();

        JSONObject obj1 = new JSONObject(output);
        //double temp = obj1.getJSONObject("main").isNull("temp") ? null : obj1.getJSONObject("main").getDouble("temp");
        JSONArray arr = obj1.getJSONArray("list");

        JSONObject weth;
        String date;

        for (int i = 0; i < 40; i++) {
            weth = (JSONObject) arr.get(i);
            date = weth.getString("dt_txt");
            dates.add(date);
        }

        return(dates);
    }
    private static String getUrlContent(String urlAdress) {
        StringBuffer content = new StringBuffer();

        try {
            URL url = new URL(urlAdress);
            URLConnection urlConn = url.openConnection();

            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
            String line;

            while((line = bufferedReader.readLine()) != null) {
                content.append(line + "\n");
            }
            bufferedReader.close();
        } catch(Exception e) {
            System.out.println("Invalid city!");
        }
        return content.toString();
    }

}
