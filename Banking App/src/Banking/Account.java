package Banking;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.Date;
import java.util.List;

public class Account implements java.io.Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private float account_balance;
	private final Date account_opened_date;
	
	public Account(float initialBalance,Date accountOpenedDate){
		this.account_balance = initialBalance;
		this.account_opened_date = accountOpenedDate;
	}	
	
	public float getAccount_balance(){ return account_balance; }
	
	public void setAccountBalnce(float newBalance){ this.account_balance = newBalance; }
	
	public Date getAccount_opened_date() { return account_opened_date; }
	
	public synchronized boolean DepositFunds(float depositAmount){
		if(depositAmount <= 0){
			return false;
		}
		else{
			this.setAccountBalnce(this.getAccount_balance()+depositAmount);
			this.SaveAccountState();
			return true;
			}
		}
	
	public synchronized boolean WithdrawFunds(float withdrawAmount){
		if(withdrawAmount <= 0){
			return false;
		}
		else if(this.getAccount_balance() - withdrawAmount < 0){
			return false;
			}
		else{
			this.setAccountBalnce(this.getAccount_balance()-withdrawAmount);
			this.SaveAccountState();
			return true;
			}
		}
	
	public synchronized boolean TransferFunds(Account receivingAccount, float transferAmount){
		if(transferAmount <= 0){
			return false;
		}
		else if(this.getAccount_balance() - transferAmount < 0){
			return false;
		}
		else{
			this.setAccountBalnce(this.getAccount_balance()-transferAmount);
			receivingAccount.setAccountBalnce(receivingAccount.getAccount_balance()+ transferAmount);
			this.SaveAccountState();
			receivingAccount.SaveAccountState();
			return true;
			}
		}
	
	private synchronized void SaveAccountState(){
		try {
			ObjectOutputStream out = new ObjectOutputStream(new FileOutputStream(""));
			out.writeObject(this);
			out.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}

class SingleAccount extends Account{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private final String account_holder;
	
	public String getAccount_holder(){ return account_holder; }
	
	public SingleAccount(String accountHolder,float initialBalance,Date accountOpenedDate){
		super(initialBalance,accountOpenedDate);
		this.account_holder = accountHolder;
	}	
}

class JointAccount extends Account{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private final List<String> account_holders;
	
	public JointAccount(List<String> accountHolders,float initialBalance,Date accountOpenedDate){
		super(initialBalance,accountOpenedDate);
		this.account_holders = accountHolders;
	}
	
	public List<String> getAccount_holders() { return account_holders; }
}
