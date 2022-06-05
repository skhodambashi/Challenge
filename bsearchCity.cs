using System;
using System.Collections.Generic;
using System.Linq;
using CitySearch;

namespace CitySearch 
{ 
    using System.Collections.Generic; 
 
    public interface ICityResult 
    { 
        ICollection<string> NextLetters { get; set; } 
 
        ICollection<string> NextCities { get; set; } 
    } 
} 
 
namespace CitySearch 
{ 
    public interface ICityFinder 
    { 
        ICityResult Search(string searchString); 
    } 
} 

namespace CitySearch 
{	
	class  myResult : ICityResult
	{
		public ICollection<string> NextLetters { get; set; } 
        public ICollection<string> NextCities { get; set; }
	}
	
	class myFinder : ICityFinder 
	{
		List<string> allCities = new List<string>();
		public myFinder() // constructor fiils up the city list which can be unsorted
		{			
            allCities.Add("BANDUNG");
			allCities.Add("BANGUI");
			allCities.Add("BANGALORE");
			allCities.Add("BANGKOK");             
            allCities.Add("LA PAZ");
			allCities.Add("LA PLATA");
			allCities.Add("LAGOS");
			allCities.Add("LEEDS"); 
			allCities.Add("ZARIA");
			allCities.Add("ZHUGHAI");
			allCities.Add("ZIBO");
			allCities.Sort();			//sort the list for once
		}
		
		public ICityResult Search(string searchString)	//method for search similar cities
		{	
			myResult result = new myResult();
			int index = allCities.BinarySearch(searchString);	//binary search is faster than linear search
					
			ICollection<string> letter = new List<string>();	//suggested next letters
			ICollection<string> name = new List<string>();		//suggested cities 
			if(index<0) index =~index;			//if not found, gives the start index of very similar cities

			for( int i=index; i < allCities.Count(); i++) //linear search for other cities starting with the search string
			{
				if(allCities[i].StartsWith(searchString))	//add similar cities to result, if exist 
				{
					name.Add(allCities[i]);					//add city name to result
					if(allCities[i].Length > searchString.Length)	//if has next letter
						letter.Add(allCities[i][searchString.Length].ToString());	//add next letter
				} else										//there are not more similar cities in the list
					break;
			}	

			result.NextCities = (ICollection<string>)name;
			result.NextLetters = (ICollection<string>)letter.Distinct().ToList();	//removes duplicate letters
			return result;
		}
	}
}

public class Program {
public static void Main(string[] args)
        {
			myFinder testCity = new myFinder();
			string inputName = "LA";
			ICityResult result = testCity.Search(inputName);	//input search
			Console.WriteLine("Search for: "+inputName);
			Console.WriteLine("Suggested City names:");
			foreach(string s in result.NextCities) 
			{
			Console.WriteLine(s);
			}
			Console.WriteLine("\nSuggested letters:");
			foreach(string s in result.NextLetters)
			{
			Console.WriteLine(s);
			}
            Console.ReadLine();
        }
}