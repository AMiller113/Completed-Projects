package Banking;

import java.util.List;
import java.util.Scanner;

public class Customer implements java.io.Serializable {
	
	private static final long serialVersionUID = 1L;
	private String user_name;
	private int password;
	private List<Account> accounts;

	public List<Account> getAccounts() { return accounts; }

	public String getUser_name() { return user_name; }
	
	public int getPassword() { return password; }
	
	public void setAccounts(List<Account> accounts) { this.accounts = accounts; }

	public void setUser_name(String user_name) { this.user_name = user_name;}

	public void setPassword(){ 
		Scanner scanner = new Scanner(System.in);
		System.out.println("Please enter your new password");
		String password =  scanner.nextLine();
		int passwordHash = password.hashCode();
		this.password = passwordHash;
		}
	
	public void DepositFunds(){
		Scanner scanner = new Scanner(System.in);
	}
	
	public void WithdrawFunds(){
		Scanner scanner = new Scanner(System.in);
	}
	
	public void TransferFunds(){
		Scanner scanner = new Scanner(System.in);
	}
	
	public void ApplyForAccount(){}
	
	public boolean ValidatePassword(String potentialPassword){
		if(potentialPassword.matches()){
			
		}
		return true;
	}
}
