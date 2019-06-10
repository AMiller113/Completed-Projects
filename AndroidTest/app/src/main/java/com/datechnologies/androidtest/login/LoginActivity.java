package com.datechnologies.androidtest.login;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.datechnologies.androidtest.MainActivity;
import com.datechnologies.androidtest.R;
import com.datechnologies.androidtest.api.ApiHandler;
import com.google.gson.JsonIOException;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * A screen that displays a login prompt, allowing the user to login to the D & A Technologies Web Server.
 *
 */
public class LoginActivity extends AppCompatActivity {


    ApiHandler apiHandler;
    EditText email_text,password_text;
    Button login_button;
    String alertString;

    //==============================================================================================
    // Static Class Methods
    //==============================================================================================

    public static void start(Context context)
    {
        Intent starter = new Intent(context, LoginActivity.class);
        context.startActivity(starter);
    }

    //==============================================================================================
    // Lifecycle Methods
    //==============================================================================================

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        ActionBar actionBar = getSupportActionBar();

        assert actionBar != null;
        actionBar.setDisplayHomeAsUpEnabled(true);
        actionBar.setDisplayShowHomeEnabled(true);

        login_button = findViewById(R.id.login_button);
        login_button.setOnClickListener(loginOnClickListener);

        // TODO: Make the UI look like it does in the mock-up. Allow for horizontal screen rotation.
        // TODO: Add a ripple effect when the buttons are clicked
        // TODO: Save screen state on screen rotation, inputted username and password should not disappear on screen rotation

        // TODO: Send 'email' and 'password' to http://dev.datechnologies.co/Tests/scripts/login.php
        // TODO: as FormUrlEncoded parameters.

        // TODO: When you receive a response from the login endpoint, display an AlertDialog.
        // TODO: The AlertDialog should display the 'code' and 'message' that was returned by the endpoint.
        // TODO: The AlertDialog should also display how long the API call took in milliseconds.
        // TODO: When a login is successful, tapping 'OK' on the AlertDialog should bring us back to the MainActivity

        // TODO: The only valid login credentials are:
        // TODO: email: info@datechnologies.co
        // TODO: password: Test123
        // TODO: so please use those to test the login.
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case android.R.id.home:
                Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                startActivity(intent);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    public void onBackPressed()
    {
        Intent intent = new Intent(LoginActivity.this, MainActivity.class);
        startActivity(intent);
    }

    private DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
        @Override
        public void onClick(DialogInterface dialog, int which) {
            Intent intent = new Intent(LoginActivity.this, MainActivity.class);
            startActivity(intent);
        }
    };

    private View.OnClickListener loginOnClickListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            Thread thread = new Thread(new Runnable() {
                @Override
                public void run() {
                    try  {
                        alertString = LoginFunction();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });

            thread.start();

            try {
                thread.join();
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(LoginActivity.this);
                alertDialogBuilder.setMessage(alertString);
                alertDialogBuilder.setPositiveButton("OK",dialogClickListener);
                alertDialogBuilder.show();
            }
           catch(InterruptedException e){
                e.printStackTrace();
           }
        }
    };

    public String LoginFunction(){
        email_text = findViewById(R.id.email_field);
        password_text = findViewById(R.id.password_field);
        String email = email_text.getText().toString();
        String password = password_text.getText().toString();

        long startTime = System.currentTimeMillis();
        apiHandler = new ApiHandler();
        apiHandler.PostWebData(email,password);
        JSONObject jsonObject = apiHandler.RetrieveWebData();
        long timeForResponse = System.currentTimeMillis() - startTime;

        StringBuilder stringBuilder = new StringBuilder();
        try {
            stringBuilder.append("Code: " + jsonObject.getString("code")+"\r\n");
            stringBuilder.append("Message: "+ jsonObject.getString("message")+"\r\n");
            stringBuilder.append("Response Time: "+ timeForResponse + " milliseconds\r\n");
        }
        catch(JSONException e){
        Log.d("Exception", "JSOn exception occurred");
        }

       return stringBuilder.toString();
    }
}
