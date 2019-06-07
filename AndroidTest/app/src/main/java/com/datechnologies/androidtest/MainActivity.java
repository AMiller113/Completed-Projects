package com.datechnologies.androidtest;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Handler;
import android.os.Parcelable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import com.datechnologies.androidtest.animation.AnimationActivity;
import com.datechnologies.androidtest.api.ChatLogMessageModel;
import com.datechnologies.androidtest.chat.ChatActivity;
import com.datechnologies.androidtest.login.LoginActivity;

import java.util.List;

/**
 * The main screen that lets you navigate to all other screens in the app.
 *
 */

public class MainActivity extends AppCompatActivity {



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setTitle(R.string.activity_main_title);
        setContentView(R.layout.activity_main);


        /**
         * =========================================================================================
         * INSTRUCTIONS
         * =========================================================================================
         *
         * 1. UI must work on Android phones of multiple sizes. Do not worry about Android Tablets.
         *
         * 2. Use this starter project as a base and build upon it. It is ok to remove some of the
         *    provided code if necessary.
         *
         * 3. Read the additional 'TODO' comments throughout the codebase, they will guide you.
         *
         * 3. Please take care of the bug(s) we left for you in the project as well.
         *
         * Thank you and Good luck. -  D & A Technologies
         * =========================================================================================
         */

        // TODO: Make the UI look like it does in the mock-up
        // TODO: Add a ripple effect when the buttons are clicked
    }

    //==============================================================================================
    // Button Click Methods
    //==============================================================================================

    public void onChatClicked(View v)
    {
        Bundle bundle = getIntent().getExtras();

        if (bundle!=null){
            List<ChatLogMessageModel> chats = (List<ChatLogMessageModel>) bundle.get("chats");
            Intent intent = new Intent(MainActivity.this,ChatActivity.class);
            intent.putExtra("chats", (Parcelable) chats);
            startActivity(intent);
          //  ChatActivity.start(this);
        }
        else{ Toast.makeText(getApplicationContext(),"Please Login first.",Toast.LENGTH_SHORT).show(); }

    }

    public void onLoginClicked(View v)
    {
        LoginActivity.start(this);
    }

    public void onAnimationClicked(View v)
    {
        AnimationActivity.start(this);
    }
}
