package com.example.tasklist;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

import java.util.List;

@Dao
public interface TaskListDao {

    @Query("Select * From TaskList Order By Date_Logged Desc")
    List<TaskList> GetAllTasks();

    @Query("Select * From TaskList Where Task_Name Like :taskName Limit 1")
    TaskList GetByTaskName(String taskName);

    @Query("Select * From TaskList Where task_id = :taskID")
    TaskList GetTaskByID(int taskID);

    @Query("Select Task_Details From TaskList Where task_id = :taskID")
    String GetTaskDetailsByID(int taskID);

    @Insert
    void AddLog(TaskList todo);

    @Delete
    void RemoveLog(TaskList todo);

    @Update
    void ChangeLog(TaskList todo);
}
