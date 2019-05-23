package com.company;

import java.io.IOException;
import java.sql.SQLException;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        /*
        *
        * Setup the Database for use by the main loop
        *
        * */

        FoodLogDatabase foodLogDatabase = null;

        //Removed the stack traces for the deployed version as while using the if statement below to let the user know if the database fails to load

        try {
            foodLogDatabase = FoodLogDatabase.getInstance();
        }
        catch (IOException e) {}
        catch (SQLException e) {}
        catch (ClassNotFoundException e) {}

        if (foodLogDatabase == null){
            System.err.println("DATABASE FAILED TO INITIALIZE, EXITING PROGRAM. (Database.properties may be setup improperly)");
            System.exit(0);
        }

        /*
         *
         * Setup fot the main loop (User input and exit clause)
         *
         * */

        boolean exit = false;
        String command;

        /*
         *
         * The Main loop itself
         *
         * */

        while(!exit){

            System.out.println("Welcome to your FoodLog! Enter the desired command using 1 through 6. \n" +
                    "1) Add Log\n" +
                    "2) Change a Log\n" +
                    "3) Delete a Log\n" +
                    "4) View Today's Logs\n" +
                    "5) View All Logs\n" +
                    "6) Get Logs From a certain Day\n"+
                    "7) Exit");

            Scanner in = new Scanner(System.in);
            command = in.nextLine();

            switch (command){
                case("1"):
                   foodLogDatabase.AddLog();
                   break;
                case("2"):
                   foodLogDatabase.ChangeLog();
                   break;
                case("3"):
                   foodLogDatabase.RemoveLog();
                   break;
                case("4"):
                   foodLogDatabase.ShowTodaysLog();
                   break;
                case("5"):
                   foodLogDatabase.ShowAllLogs();
                   break;
                case("6"):
                    foodLogDatabase.GetLogFromDay();
                    break;
                case("7"):
                   exit = true;
                   in.close();
                   break;
                default:
                   System.out.println("Invalid Command. Please Try Again");
                   break;
           }
        }

        /*
         *
         * Close resources
         *
         * */

        System.out.println("Take care, and stay healthy!");
        try {
            foodLogDatabase.CloseConnection();

        } catch (SQLException e) {
            //e.printStackTrace();
        }
    }
}
