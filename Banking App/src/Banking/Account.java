package Banking;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.Date;
import java.util.List;

public class Account implements java.io.Serializable {

	private static final long serialVersionUID = 1L;
	private final List<String> account_holders;
	private float account_balance;
	private final Date account_opened_date;
	
	public Account(List<String> account_holders,float initialBalance,Date accountOpenedDate){
		this.account_holders = account_holders;
		this.account_balance = initialBalance;
		this.account_opened_date = accountOpenedDate;
	}	
	
	public float getAccount_balance(){ return account_balance; }
	
	public Date getAccount_opened_date() { return account_opened_date; }
	
	public List<String> getAccount_holders() { return account_holders; }
	
	public void setAccountBalnce(float newBalance) { this.account_balance = newBalance; }
	
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
			StringBuilder sb = new StringBuilder();
			for(String x : this.account_holders){
				sb.append(x);
			}
			String path = sb.toString().concat(".ser");
			ObjectOutputStream out = new ObjectOutputStream(new FileOutputStream(path));
			out.writeObject(this);
			out.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
