using System.Net.NetworkInformation;

public class NetworkUtility {
                                // Returns the 1st valid Mac Address
	public static string GetMacAddress(){
	
	    string macAdress = "";
	
	    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
	
	    foreach (NetworkInterface adapter in nics){
	
	        PhysicalAddress address = adapter.GetPhysicalAddress();
	
	        if(address.ToString() != ""){
	
	            macAdress = address.ToString();
	
	            return macAdress;
	
	        }
	
	    }
	
	    return "0";
    }
	
}
