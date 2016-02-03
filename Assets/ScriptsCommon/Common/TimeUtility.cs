using UnityEngine;
using System.Collections;
using System;

public class TimeUtility {

	public static string GetTimestamp(){
		return DateTime.Now.ToString("yyyyMMddHHmmssffff");
	}
	
	public static bool IsDifferentDay(DateTime datetime1, DateTime datetime2) {
		//Debuger.Log("IsDifferentDay --- " + datetime1.ToString() + " +++ " + datetime2.ToString());
		int yearDiff = Mathf.Abs(datetime2.Year - datetime1.Year);
		//Debuger.Log("yearDiff " + yearDiff);
		if (yearDiff > 0) {
			return true;
		}
		else {
			int monthDiff = Mathf.Abs(datetime2.Month - datetime1.Month);
			//Debuger.Log("monthDiff " + monthDiff);
			if (monthDiff > 0) {
				return true;
			}
			else {
				int dayDiff = Mathf.Abs(datetime2.Day - datetime1.Day);
				//Debuger.Log("dayDiff " + dayDiff);
				if (dayDiff > 0) {
					return true;
				}
				else {
					return false;
				}
			}
		}
	}
	
	public static bool IsDayDifferenceEqualsOne(DateTime datetime1, DateTime datetime2) {
		//Debuger.Log("IsDayDifferenceLessThanOne --- " + datetime1.ToString() + " +++ " + datetime2.ToString());
		int yearDiff = Mathf.Abs(datetime2.Year - datetime1.Year);
		//Debuger.Log("yearDiff " + yearDiff);
		if (yearDiff > 0) {
			return false;
		}
		else {
			int monthDiff = Mathf.Abs(datetime2.Month - datetime1.Month);
			//Debuger.Log("monthDiff " + monthDiff);
			if (monthDiff > 0) {
				return false;
			}
			else {
				int dayDiff = Mathf.Abs(datetime2.Day - datetime1.Day);
				//Debuger.Log("dayDiff " + dayDiff);
				if (dayDiff == 1) {
					return true;
				}
				else {
					return false;
				}
			}
		}
	}
	
	public static double GetHourDiff(DateTime datetime1, DateTime datetime2) {
		return (datetime2 - datetime1).TotalHours;
	}
	
	public static double GetMinuteDiff(DateTime datetime1, DateTime datetime2) {
		return (datetime2 - datetime1).TotalMinutes;
	}
}
