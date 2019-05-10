package com.company;

import java.io.FileInputStream;
import java.io.IOException;
import java.sql.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Properties;
import java.util.Scanner;

public class FoodLogDatabase {
    private String driver_name;
    private String url;
    private String username;
    private String password;
    private final static String properties_path = "database.properties";
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
    private Connection connection;
    private static FoodLogDatabase instance = null;

    private FoodLogDatabase() throws IOException, SQLException,ClassNotFoundException{
            FileInputStream in = new FileInputStream("database.properties");
            Properties properties = new Properties();
            properties.load(in);
            driver_name = (String) properties.get("DName");
            if (driver_name != null){
                Class.forName(driver_name);
            }
            url = (String) properties.get("URL");
            username = (String) properties.get("UName");
            password = (String) properties.get("PassW");
            connection = DriverManager.getConnection(url,username,password);
    }

    public static synchronized FoodLogDatabase getInstance() throws IOException,SQLException,ClassNotFoundException{

        if (instance == null){
            instance = new FoodLogDatabase();
        }

        return instance;
    }

    public synchronized void AddLog(){

        try (PreparedStatement preparedStatement = connection.prepareStatement(add_log_command))
        {
            Scanner in = new Scanner(System.in);
            System.out.println("Please enter the following values separated by spaces (or 0 if you don't know).");
            System.out.println("Food_Name, Food_Group, Calories_Per_Serving, Number_of_Servings(will Default to 1)");
            String [] log = in.nextLine().split(" ");

            if(!ValidateInput(log)){
                return;
            }

            int netCalories = 0;

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
           // e.printStackTrace();
        }
    }

    public synchronized void ChangeLog(){
        try (PreparedStatement preparedStatement = connection.prepareStatement(change_log_command))
        {
            Scanner in = new Scanner(System.in);
            System.out.println("Enter the logs ID number");
            ShowAllLogs();
            String i = in.nextLine();

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
            //e.printStackTrace();
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
            //e.printStackTrace();
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
           // e.printStackTrace();
        }
    }

    public void ShowAllLogs(){
        try (PreparedStatement preparedStatement = connection.prepareStatement(show_all_logs_command)){
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
        catch (SQLException e) {
            return;
            //e.printStackTrace();
        }
    }

    public void GetLogFromDay(){
        try(PreparedStatement preparedStatement = connection.prepareStatement(get_log_from_day_command)){

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
           // e.printStackTrace();
        }
        catch (ParseException e) {
            System.out.println("Error in date submission, returning to main menu.");
            return;
        }
    }

    public void CloseConnection() throws SQLException{

        if(connection!=null){
            connection.close();
        }

    }

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

    private boolean isNumeric(String possibleNumber){
        try{
            Double.parseDouble(possibleNumber);
            return true;
        }
        catch(NumberFormatException e){
            return false;
        }
    }

    private int CalculateNetCalories(int caloriesPerServing, float numberOfServings){
        return (int) (caloriesPerServing*numberOfServings);
    }
}
