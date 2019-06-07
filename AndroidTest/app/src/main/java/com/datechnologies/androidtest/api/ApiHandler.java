package com.datechnologies.androidtest.api;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;

import org.json.JSONObject;

import android.content.Context;
import android.util.Log;

public class ApiHandler {

    private static final String test_url = "http://dev.datechnologies.co/Tests/scripts/login.php/";
    private URL url;
    private HttpURLConnection connection;

    public void PostWebData(String email, String password){
        try{
            String parameters = email+"=data1&"+password+"=data2";
            byte[] postData = parameters.getBytes( StandardCharsets.UTF_8 );
            int postDataLength = postData.length;
            url = new URL( test_url );
            connection = (HttpURLConnection) url.openConnection();
            connection.setDoOutput(true);
            connection.setInstanceFollowRedirects(false);
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            connection.setRequestProperty("charset", "utf-8");
            connection.setRequestProperty("Content-Length", Integer.toString(postDataLength ));
            connection.setUseCaches(false);
            DataOutputStream outputStream = new DataOutputStream(connection.getOutputStream());
            outputStream.write( postData );
            outputStream.close();
        }
        catch (Exception e){
            e.printStackTrace();
        }

    }

    public JSONObject GetWebData(){

        if (url == null||connection == null)
            return null;

        try{
            BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            StringBuffer json = new StringBuffer();
            String line;

            while((line = reader.readLine()) !=null)
                json.append(line);

            reader.close();

            JSONObject jsonObject = new JSONObject(json.toString());
            System.out.print(json.toString());
            return jsonObject;
        }
        catch (Exception e){
            return null;
        }
    }

}
