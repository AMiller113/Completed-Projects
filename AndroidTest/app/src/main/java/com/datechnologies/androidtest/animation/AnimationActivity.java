package com.datechnologies.androidtest.animation;

import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.support.v4.app.NavUtils;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toolbar;

import com.datechnologies.androidtest.MainActivity;
import com.datechnologies.androidtest.R;

/**
 * Screen that displays the D & A Technologies logo.
 * The icon can be moved around on the screen as well as animated.
 * */

public class AnimationActivity extends AppCompatActivity {

    private static final int fade_delay = 100;
    ImageView imageView;
    Button fade_in_button;

    //==============================================================================================
    // Class Properties
    //==============================================================================================

    //==============================================================================================
    // Static Class Methods
    //==============================================================================================

    public static void start(Context context)
    {
        Intent starter = new Intent(context, AnimationActivity.class);
        context.startActivity(starter);
    }

    //==============================================================================================
    // Lifecycle Methods
    //==============================================================================================

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_animation);

        ActionBar actionBar = getSupportActionBar();

        assert actionBar != null;
        actionBar.setDisplayHomeAsUpEnabled(true);
        actionBar.setDisplayShowHomeEnabled(true);

        fade_in_button = findViewById(R.id.FadeInButton);
        fade_in_button.setOnClickListener(fade_in_listener);

        // TODO: Make the UI look like it does in the mock-up. Allow for horizontal screen rotation.
        // TODO: Add a ripple effect when the buttons are clicked

        // TODO: When the fade button is clicked, you must animate the D & A Technologies logo.
        // TODO: It should fade from 100% alpha to 0% alpha, and then from 0% alpha to 100% alpha

        // TODO: The user should be able to touch and drag the D & A Technologies logo around the screen.

        // TODO: Add a bonus to make yourself stick out. Music, color, fireworks, explosions!!!
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case android.R.id.home:
                Intent intent = new Intent(AnimationActivity.this, MainActivity.class);
                startActivity(intent);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    public void onBackPressed()
    {
        Intent intent = new Intent(AnimationActivity.this, MainActivity.class);
        startActivity(intent);
    }

    private View.OnClickListener fade_in_listener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            Runnable runnable  = new Runnable() {
                @Override
                public void run() {
                    FadeIn();
                }
            };
            Handler handler = new Handler();
            handler.postDelayed(runnable, fade_delay);
        }
    };

    private void FadeIn() {

        imageView = findViewById(R.id.DaImage);

        for(int i = imageView.getImageAlpha() ; i >= 0; i--){
            imageView.setImageAlpha(i);
        }

        /*Thread.sleep(500);

        for(int i = imageView.getImageAlpha(); i <= 255; i++){
            imageView.setImageAlpha(i);
            Thread.sleep(fade_delay);
        }*/
    }
}
