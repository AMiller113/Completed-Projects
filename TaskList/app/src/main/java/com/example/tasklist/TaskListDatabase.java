package com.example.tasklist;

import android.arch.persistence.room.Database;
import android.arch.persistence.room.Room;
import android.arch.persistence.room.RoomDatabase;
import android.arch.persistence.room.TypeConverters;
import android.content.Context;

@Database(entities = {TaskList.class}, version = 1, exportSchema = false)
@TypeConverters(TaskList.class)
public abstract class TaskListDatabase extends RoomDatabase {

    private static TaskListDatabase instance;
    private final static String database_name ="Task_List_Database";

    public static synchronized TaskListDatabase getInstance(Context context) {
        if (instance == null){
           instance = Room.databaseBuilder(context,TaskListDatabase.class, database_name).allowMainThreadQueries().build();
        }
        return instance;
    }

    public abstract TaskListDao taskListDao();
}
