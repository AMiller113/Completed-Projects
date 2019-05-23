package com.company;

import java.io.FileInputStream;
import java.io.IOException;
import java.sql.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Properties;
import java.util.Scanner;

/*
*
* Class class contains the majority of the functionality of the application besides the main loop
*
* */


public class FoodLogDatabase {

    /*
     *
     * The data references that will receive the properties from the properties file
     *
     * */

    private String driver_name;
    private String url;
    private String username;
    private String password;

    /*
    *
    *The SQL strings that will be used in the individual methods of the program via prepared statements
    *
    * */

    private final static String add_log_command =
                             "INSERT INTO demo.FoodLog " +
                             "VALUES (NULL, ?, ?, ?, ?, ?, ?, ?)";
    private final static String change_log_command =
                             "UPDATE demo.FoodLog " +
                             "SET FoodName = ?, FoodGroup = ?, CaloriesPerServing = ?, NumberOfServings = ?, NetCalories = ? " +
                             "WHERE ID = ?";
    private final static String delete_log_command =
                             "DELETE FROM demo.FoodLog " +
                             "WHERE ID = ?";
    private final static String show_all_logs_command =
                             "SELECT * FROM demo.FoodLog ";
    private final static String get_log_from_day_command =
                              "SELECT * "+
                              "FROM demo.FoodLog "+
                              "WHERE DateConsumed = ?";
    private final static String show_todays_log_command =
                              "SELECT * "+
                              "FROM demo.FoodLog "+
                              "WHERE DateConsumed = current_date()";

    //A reference to the connection object for the database

    private Connection connection;

    /*
     *
     *Instance for the singleton pattern, I've read that database functionality can be result in relatively expensive objects
     * and methods thus using a singleton can help reduce the databases overhead by limiting the number of them being created
     *
     * */

    private static FoodLogDatabase instance = null;

    /*
     *
     *The constructor does the heavy lifting in terms of setting up the database, including reading in the properties.
     * I opted to have the exceptions be thrown rather than handling them here as the constructor was getting a bit large.
     *
     * */

    private FoodLogDatabase() throws IOException, SQLException,ClassNotFoundException{
            FileInputStream in = new FileInputStream("database.properties");
            Properties properties = new Properties();
            properties.load(in);
            in.close();
            driver_name = (String) properties.get("DName");
            if (driver_name != null){
                Class.forName(driver_name);
            }
            url = (String) properties.get("URL");
            username = (String) properties.get("UName");
            password = (String) properties.get("PassW");
            connection = DriverManager.getConnection(url,username,password);

    }

    /*
     *
     *
     * The instance getter for the singleton pattern
     *
     * */

    public static synchronized FoodLogDatabase getInstance() throws IOException,SQLException,ClassNotFoundException{

        if (instance == null){
            instance = new FoodLogDatabase();
        }
        return instance;
    }

    public synchronized void AddLog(){

        /*
         *
         *Utilizing prepared statements makes it easier to deal with placing user input within the queries,
         * from what I know it also allows SQL to cache the queries rather than creating new ones for every new statement object
         *
         * */

        try (PreparedStatement preparedStatement = connection.prepareStatement(add_log_command))
        {
            Scanner in = new Scanner(System.in);
            System.out.println("Please enter the following values separated by spaces (or 0 if you don't know).");
            System.out.println("Food_Name, Food_Group, Calories_Per_Serving, Number_of_Servings(will Default to 1)");
            String [] log = in.nextLine().split(" ");

            /*
             *
             *This next section is a target for future refactoring as there is some problematic code reuse
             * in the majority of the methods for the main commands
             *
             * */

            if(!ValidateInput(log)){
                return;
            }

            int netCalories;

            if (!log[3].equals("0")){
                int calories = Integer.parseInt(log[2]);
                float servings = Float.parseFloat(log[3]);
                netCalories = (int) (calories*servings);
            }
            else{
                netCalories = Integer.parseInt(log[2]);
            }

            preparedStatement.setString(1,log[0]);
            preparedStatement.setString(2,log[1]);
            preparedStatement.setInt(3,Integer.parseInt(log[2]));
            preparedStatement.setFloat(4, Float.parseFloat(log[3]));
            preparedStatement.setInt(5,netCalories);
            preparedStatement.setDate(6,new Date(System.currentTimeMillis()));
            preparedStatement.setTime(7, new Time(System.currentTimeMillis()));

            /*
             *
             *
             * Let user know of the success or failure of the operation
             *
             * */

            int rowsAffected = preparedStatement.executeUpdate();

            if(rowsAffected > 0){
                System.out.println("Your log was completed successfully.");
                return;
            }
            else{
                System.err.println("Log failed, Apologies.");
            }

        }
        catch (SQLException e) {
            return;
        }
    }

    public synchronized void ChangeLog(){
        try (PreparedStatement preparedStatement = connection.prepareStatement(change_log_command))
        {
            /*
            *
            * I call the show logs so the user can see the user ID for the log they wish to change or remove
            *
            * */

            Scanner in = new Scanner(System.in);
            System.out.println("Enter the logs ID number");
            ShowAllLogs();
            String i = in.nextLine();

            //Additional validation for the ID number

            if(!isNumeric(i)){
                System.out.println("You did not enter a valid ID number, returning to main menu.");
                return;
            }

            int id = Integer.parseInt(i);

            if(id == 0){
                System.out.println("You did not enter a valid ID number, returning to main menu.");
                return;
            }

            System.out.println("Enter the following values separated by spaces (or 0 if you don't know).");
            System.out.println("Food_Name, Food_Group, Calories_Per_Serving, Number_of_Servings(will Default to 1)");
            String [] log = in.nextLine().split(" ");

            /*
            *
            * Refactor Target
            *
            * */

            if(!ValidateInput(log)){
                return;
            }

            int netCalories;

            if (!log[3].equals("0")){
                int calories = Integer.parseInt(log[2]);
                float servings = Float.parseFloat(log[3]);
                netCalories = (int) (calories*servings);
            }
            else{
                netCalories = Integer.parseInt(log[2]);
            }

            preparedStatement.setString(1,log[0]);
            preparedStatement.setString(2,log[1]);
            preparedStatement.setInt(3,Integer.parseInt(log[2]));
            preparedStatement.setFloat(4, Float.parseFloat(log[3]));
            preparedStatement.setInt(5,netCalories);
            preparedStatement.setInt(6,id);

            int rowsAffected = preparedStatement.executeUpdate();

            if(rowsAffected > 0){
                System.out.println("The log was changed successfully.");
                return;
            }
            else{
                System.err.println("Change failed, ID number may be incorrect.");
            }


        }
        catch (SQLException e) {
            return;
        }
    }

    public synchronized void RemoveLog(){

        try (PreparedStatement preparedStatement = connection.prepareStatement(delete_log_command))
        {
            Scanner in = new Scanner(System.in);
            System.out.println("Enter the logs ID number that you wish to remove or 0 to go back to the main menu.");
            ShowAllLogs();
            String i = in.nextLine();

            if(!isNumeric(i)){
                System.out.println("You did not enter a valid ID number, returning to main menu.");
                return;
            }

            int id = Integer.parseInt(i);
            if(id == 0){
                System.out.println("Returning to main menu.");
                return;
            }

            preparedStatement.setInt(1,id);
            int rowsAffected = preparedStatement.executeUpdate();



            if(rowsAffected > 0){
                System.out.println("The log was deleted successfully.");
                return;
            }
            else{
                System.err.println("Delete failed, Apologies.");
                return;
            }


        }
        catch (SQLException e) {
            return;
        }
    }

    public void ShowTodaysLog(){

        try(PreparedStatement preparedStatement = connection.prepareStatement(show_todays_log_command)){

            ResultSet resultSet = preparedStatement.executeQuery();
            ResultSetMetaData resultSetMetaData = resultSet.getMetaData();
            int numOfColumns = resultSetMetaData.getColumnCount();
            System.out.println("ID FoodName FoodGroup Calories NumberOfServings NetCalories DateConsumed TimeLogged");
            System.out.println("======================================================================================================================================");

            while (resultSet.next()){
                for(int i = 1; i < numOfColumns; i++){
                    System.out.print(resultSet.getString(i) + " ");
                }
                System.out.print("\n");
            }
        }
        catch (SQLException e){
            return;
        }
    }

    public void ShowAllLogs(){
        try (PreparedStatement preparedStatement = connection.prepareStatement(show_all_logs_command)){

            ResultSet resultSet = preparedStatement.executeQuery();
            ResultSetMetaData resultSetMetaData = resultSet.getMetaData();

            //Gets the number of columns for the purpose of iterating through the result set
            int numOfColumns = resultSetMetaData.getColumnCount();

            //Iterates through the result set/logs currently in the database and outputs it to the user using the format below

            System.out.println("ID FoodName FoodGroup Calories NumberOfServings NetCalories DateConsumed TimeLogged");
            System.out.println("======================================================================================================================================");

            while (resultSet.next()){
                for(int i = 1; i < numOfColumns; i++){
                    System.out.print(resultSet.getString(i) + " ");
                }
                System.out.print("\n");
            }
        }
        catch (SQLException e) {
            return;
        }
    }

    public void GetLogFromDay(){

        try(PreparedStatement preparedStatement = connection.prepareStatement(get_log_from_day_command) ){

            Scanner in = new Scanner(System.in);
            System.out.println("Enter the date for the logs you wish to see in dd-MM-yyyy format");
            String day = in.nextLine();
            SimpleDateFormat format = new SimpleDateFormat("dd-MM-yyyy");
            java.util.Date date = format.parse(day);
            Date sqlDate = new Date(date.getTime());
            preparedStatement.setDate(1,sqlDate);
            ResultSet resultSet = preparedStatement.executeQuery();
            ResultSetMetaData resultSetMetaData = resultSet.getMetaData();
            int numOfColumns = resultSetMetaData.getColumnCount();

            System.out.println("ID FoodName FoodGroup Calories NumberOfServings NetCalories DateConsumed TimeLogged");
            System.out.println("======================================================================================================================================");

            while (resultSet.next()){
                for(int i = 1; i < numOfColumns; i++){
                    System.out.print(resultSet.getString(i) + " ");
                }
                System.out.print("\n");
            }
        }
        catch (SQLException e){
            return;
        }
        catch (ParseException e) {
            System.out.println("Error in date submission, returning to main menu.");
            return;
        }
    }

    //Closes the connection if it is currently open, necessary due to accessibility of the connection object

    public void CloseConnection() throws SQLException{

        if(connection!=null){
            connection.close();
        }

    }

    //Validates the string input from the user for the functions that add or change logs

    private boolean ValidateInput(String [] userInput){

        if(userInput.length!=4){
            System.err.println("Their was an error in your log submission, Please try again.");
            return false;
        }
        for(String s : userInput){
            if(s.length()>50){
                System.err.println("The maximum character length is 50. Please resubmit your log.");
            }
        }

        if(!isNumeric(userInput[2]) && !isNumeric(userInput[3])){
            System.err.println("Their was an error in your log submission, Please try again.");
            return false;
        }

        return true;
    }

    //Checks the validity of numbers passed by the user

    private boolean isNumeric(String possibleNumber){
        try{
            Double.parseDouble(possibleNumber);
            return true;
        }
        catch(NumberFormatException e){
            return false;
        }
    }
}
