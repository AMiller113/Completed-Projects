package com.datechnologies.androidtest.animation;

import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;

import com.datechnologies.androidtest.MainActivity;
import com.datechnologies.androidtest.R;

/**
 * Screen that displays the D & A Technologies logo.
 * The icon can be moved around on the screen as well as animated.
 * */

public class AnimationActivity extends AppCompatActivity {

    private static final int fade_delay = 100;
    private ImageView imageView;
    private ViewGroup relative_layout;
    private Button fade_in_button;
    private int x;
    private int y;

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

        relative_layout = findViewById(R.id.RelativeLayout);

        imageView = findViewById(R.id.DaImage);
        imageView.setOnTouchListener(new MoveListener());


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
                    FadeInAndOut();
                }
            };
            Handler handler = new Handler();
            handler.postDelayed(runnable, fade_delay);
        }
    };

    private void FadeInAndOut() {

        imageView = findViewById(R.id.DaImage);

        Animation fadeOut = new AlphaAnimation(1, 0);
        fadeOut.setInterpolator(new AccelerateInterpolator());
        fadeOut.setDuration(1000);

        fadeOut.setAnimationListener(new Animation.AnimationListener()
        {
            public void onAnimationEnd(Animation animation)
            {
                Animation fadeIn = new AlphaAnimation(0, 1);
                fadeIn.setInterpolator(new AccelerateInterpolator());
                fadeIn.setDuration(1000);
                imageView.startAnimation(fadeIn);
            }
            public void onAnimationRepeat(Animation animation) {}
            public void onAnimationStart(Animation animation) {}
        });

        imageView.startAnimation(fadeOut);
    }

    private final class MoveListener implements View.OnTouchListener {

        @Override
        public boolean onTouch(View v, MotionEvent event) {
            final int eventRawX = (int)event.getRawX();
            final int eventRawY = (int)event.getRawY();
            switch(event.getActionMasked()){
                case MotionEvent.ACTION_DOWN:
                    RelativeLayout.LayoutParams layoutP = (RelativeLayout.LayoutParams) v.getLayoutParams();
                    x = eventRawX - layoutP.leftMargin;
                    y = eventRawY - layoutP.topMargin;
                    break;
                case MotionEvent.ACTION_UP:
                    break;
                case MotionEvent.ACTION_POINTER_DOWN:
                    break;
                case MotionEvent.ACTION_POINTER_UP:
                    break;
                case MotionEvent.ACTION_MOVE:
                    RelativeLayout.LayoutParams layoutParams = (RelativeLayout.LayoutParams) v.getLayoutParams();
                    layoutParams.leftMargin = eventRawX - x;
                    layoutParams.topMargin = eventRawY - y;
                    layoutParams.rightMargin = -250;
                    layoutParams.bottomMargin = -250;
                    v.setLayoutParams(layoutParams);
                    break;
            }
            relative_layout.invalidate();
            return true;
        }
    }
}
