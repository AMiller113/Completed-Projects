package com.example.tasklist;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import java.sql.Date;

public class EditOrDeleteTask extends AppCompatActivity {

    Button saveButton, deleteButton;
    EditText editText3,editText4;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit_or_delete_task);

        final TaskListDatabase taskListDatabase = TaskListDatabase.getInstance(getApplicationContext());
        Bundle bundle = getIntent().getExtras();
        final int id = bundle.getInt("taskID");
        String taskTitle = bundle.getString("taskName");
        final String taskDetails = bundle.getString("taskDetails");

        editText3 = findViewById(R.id.TitleText);
        editText4 = findViewById(R.id.TaskDetails);
        editText3.setText(taskTitle);
        editText4.setText(taskDetails);

        saveButton = findViewById(R.id.SaveButton);
        saveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                 TaskList task = taskListDatabase.taskListDao().GetTaskByID(id);
                 java.util.Date date1 = new java.util.Date();
                 Date date2 = new Date(date1.getTime());

                 task.setTask_name(editText3.getText().toString());
                 task.setTask_details(editText4.getText().toString());
                 task.setDate_logged(date2);
                 taskListDatabase.taskListDao().ChangeLog(task);

                 Intent intent = new Intent(EditOrDeleteTask.this, MainActivity.class);
                 startActivity(intent);
            }
        });
        deleteButton = findViewById(R.id.DeleteButton);
        deleteButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                TaskList task = taskListDatabase.taskListDao().GetTaskByID(id);
                taskListDatabase.taskListDao().RemoveLog(task);

                Intent intent = new Intent(EditOrDeleteTask.this, MainActivity.class);
                startActivity(intent);
            }
        });
    }
}
