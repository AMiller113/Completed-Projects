package com.example.tasklist;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    Button button;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final TaskListDatabase taskListDatabase = TaskListDatabase.getInstance(getApplicationContext());
        final List<TaskList> taskList =  taskListDatabase.taskListDao().GetAllTasks();
        final List<String> taskNames = new ArrayList<>();
        final List<Integer> taskIDs = new ArrayList<>();
        StringBuilder stringBuilder = new StringBuilder();

        for (TaskList t : taskList) {
            taskIDs.add(t.getTask_id());
            stringBuilder.append(t.getTask_name()+" - Added on: "+ t.getDate_logged());
            taskNames.add(stringBuilder.toString());
            stringBuilder.delete(0, stringBuilder.length());
            }


        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this,android.R.layout.simple_selectable_list_item,taskNames);
        ListView listView = findViewById(R.id.listView);
        listView.setAdapter(arrayAdapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                int taskID = taskIDs.get(position);
                String taskName = taskList.get(position).getTask_name();
                String taskDetails = taskList.get(position).getTask_details();
                Intent intent = new Intent(MainActivity.this, EditOrDeleteTask.class);
                intent.putExtra("taskName",taskName);
                intent.putExtra("taskDetails", taskDetails);
                intent.putExtra("taskID", taskID);
                startActivity(intent);
            }
        });

        button = findViewById(R.id.button1);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, Add_Task.class);
                startActivity(intent);
            }
        });
    }
}
