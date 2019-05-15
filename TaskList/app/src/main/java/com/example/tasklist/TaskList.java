package com.example.tasklist;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverter;

import java.sql.Date;

@Entity
public class TaskList {

    @PrimaryKey(autoGenerate = true)
    public int task_id;

    @ColumnInfo(name = "Task_Name")
    public String task_name;

    @ColumnInfo(name = "Task_Details")
    public String task_details;

    @ColumnInfo(name = "Date_Logged")
    public Date date_logged;

    @TypeConverter
    public static Date fromTimestamp(Long value) {
        return value == null ? null : new Date(value);
    }

    @TypeConverter
    public static Long dateToTimestamp(Date date) {
        return date == null ? null : date.getTime();
    }

    public TaskList(String task_name, String task_details, Date date_logged){
        this.task_name = task_name;
        this.task_details = task_details;
        this.date_logged = date_logged;
    }

    public int getTask_id() {
        return task_id;
    }

    public String getTask_name() {
        return task_name;
    }

    public void setTask_name(String task_name){this.task_name = task_name;}

    public String getTask_details() {
        return task_details;
    }

    public void setTask_details(String task_details){this.task_details = task_details;}

    public Date getDate_logged() {
        return date_logged;
    }

    public void setDate_logged(Date date_logged){ this.date_logged = date_logged; }
}
