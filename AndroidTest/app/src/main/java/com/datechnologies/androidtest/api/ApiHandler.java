package com.datechnologies.androidtest.api;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.util.Timer;

import org.json.JSONObject;
import android.util.Log;

public class ApiHandler {

    private static final String test_url = "http://dev.datechnologies.co/Tests/scripts/login.php/";
    private URL url;
    private HttpURLConnection connection;

    public void PostWebData(String email, String password){
        try{
            url = new URL( test_url );
            connection = (HttpURLConnection) url.openConnection();
            connection.setDoOutput(true);
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            PrintWriter postData = new PrintWriter(connection.getOutputStream());
            postData.write("email="+ URLEncoder.encode(email,"UTF-8")+"&");
            postData.write("password="+URLEncoder.encode(password,"UTF-8"));
            postData.close();

        }
        catch (Exception e){
            e.printStackTrace();
        }

    }

    public JSONObject RetrieveWebData(){

        if (url == null||connection == null)
            return null;

        try{
            BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            StringBuffer json = new StringBuffer();
            String line;

            while((line = reader.readLine()) !=null){
                json.append(line);
            }

            reader.close();

            JSONObject jsonObject = new JSONObject(json.toString());
            Log.d("jsonText", json.toString());
            System.out.print(json.toString());
            return jsonObject;
        }
        catch (Exception e){
            return null;
        }
    }
}
