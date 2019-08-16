package Banking;

import java.util.List;
import java.util.Scanner;

public class Customer {
	private String user_name;
	private int password;
	private List<Account> accounts;

	public List<Account> getAccounts() {
		return accounts;
	}

	public void setAccounts(List<Account> accounts) {
		this.accounts = accounts;
	}

	public String getUser_name() {
		return user_name;
	}

	public void setUser_name(String user_name) {
		this.user_name = user_name;
	}

	public int getPassword() {
		return password;
	}

	public void setPassword(int password) {
		this.password = password;
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
}
