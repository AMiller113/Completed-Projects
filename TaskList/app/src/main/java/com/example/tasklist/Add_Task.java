package com.example.tasklist;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import java.sql.Date;

public class Add_Task extends AppCompatActivity {

    EditText editText1, editText2;
    Button button,backButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add__task);

        final TaskListDatabase taskListDatabase = TaskListDatabase.getInstance(getApplicationContext());
        editText1 = findViewById(R.id.TitleText);
        editText2 = findViewById(R.id.TaskDetails);

        button = findViewById(R.id.SaveButton);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String text1,text2;
                text1 = editText1.getText().toString();
                text2 = editText2.getText().toString();
                java.util.Date date1 = new java.util.Date();
                Date date2 = new Date(date1.getTime());
                taskListDatabase.taskListDao().AddLog(new TaskList(text1,text2,date2));

                Intent intent = new Intent(Add_Task.this, MainActivity.class);
                startActivity(intent);
            }
        });

        backButton = findViewById(R.id.BackButton);
        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Add_Task.this, MainActivity.class);
                startActivity(intent);
            }
        });
    }
}
